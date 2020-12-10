using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SD_UsualAction;
using Islands;

namespace Caps
{
    public class Manager : Singleton<Manager>
    {       
        private void Awake()
        {
            CreateSingleton(true);
        }

        #region Variables
        [Header("Playtest variable")]
        public float transitionTime;
        public float verbTime;
        public int numberBeforeSpeedUp;
        //int that will be added on the id to make it appear more offen, they all start with a value of 10
        public int capWeightToAdd;
        public int listWeightToAdd;

        [Header ("Parameters")]
        public CapsSorter sorter;
        public Island[] islandList;
        public ChallengeHaptique challengeHaptique;
        public ChallengeInput challengeInput;
        public int zoneNumber;
       
        private int currentMiniGame;
        private int miniGamePassedNumber;
        private AsyncOperation currentAsyncScene;
        private Cap currentCap;

        public BPM bpm = BPM.Slow;


        public Difficulty currentDifficulty;

        [Header("UI Management")]
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public GameObject verbePanel;
        public TextMeshProUGUI verbeText;
        public GameObject sceneCam;
        #endregion

        #region Methods



        /// <summary>
        /// lunch a cap, if call within a cap, lunch the next mini game. If cap is already done, lunch CapEnd.
        /// </summary>
        /// <param name="_currentCap"></param>
        /// <returns></returns>
        public IEnumerator StartCap(Cap _currentCap)
        {
            currentCap = _currentCap;
            if (currentCap.isDone)
            {
                Debug.Log("already done");
                CapEnd();
                yield break;
            }
            panel.SetActive(false);
            verbePanel.SetActive(true);
            if(currentAsyncScene == null)
            {
                Debug.Log(_currentCap.chosenMiniGames.Count);
                currentDifficulty = _currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;
                currentAsyncScene = SceneManager.LoadSceneAsync(_currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
                currentAsyncScene.allowSceneActivation = false;

            }
            verbeText.text = _currentCap.chosenMiniGames[currentMiniGame].verbe;
            yield return new WaitForSeconds(verbTime);
            sceneCam.SetActive(false);
            verbePanel.SetActive(false);
            currentAsyncScene.allowSceneActivation = true;
        }

        /// <summary>
        /// Debug the result of the game on a panel
        /// </summary>
        /// <param name="win"> if true the game is won , if false the game is lost</param>
        public void Result(bool win)
        {
            if (win)
                resultText.text = "You Won!";

            else
                resultText.text = "You Lost!";

            panel.SetActive(true);
            StartCoroutine(Transition());


        }

        /// <summary>
        /// make the transition within a cap
        /// </summary>
        /// <returns></returns>
        private IEnumerator Transition()
        {
            sceneCam.SetActive(true);
            SceneManager.UnloadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            if (currentCap.chosenMiniGames[currentMiniGame].currentDifficulty != Difficulty.HARD)
                currentCap.chosenMiniGames[currentMiniGame].currentDifficulty++;


            currentMiniGame++;
            if (currentMiniGame == currentCap.chosenMiniGames.Count-1)
                currentMiniGame = 0;


            miniGamePassedNumber++;

            if(miniGamePassedNumber%numberBeforeSpeedUp == 0)
            {
               bpm = bpm.Next(); 
            }

            currentDifficulty = currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;
            currentAsyncScene = SceneManager.LoadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
            currentAsyncScene.allowSceneActivation = false;

            yield return new WaitForSeconds(transitionTime);
            if(currentCap.length == miniGamePassedNumber)
            {
                CapEnd();
                yield break;
            }
            StartCoroutine(StartCap(currentCap));
        }
       

        /// <summary>
        /// reset values
        /// </summary>
        private void CapEnd()
        {
            currentCap.isDone = true;

            var _island = new Island();
            foreach (var island in islandList)
            {
                if (island.capList.Contains(currentCap))
                {
                    _island = island;
                    break;
                }
            }

            var _index = 1000;
            for (int i = 0; i < _island.capList.Count; i++)
            {
                if (_island.capList[i] == currentCap)
                    _index = i;
            }

            var _islandToGo = _island.accessibleNeighbours[_index];
            for (int i = 0; i < _islandToGo.accessibleNeighbours.Length; i++)
            {
                if(_islandToGo.accessibleNeighbours[i] == _island)
                {
                    _islandToGo.capList[i].isDone = true;
                }
            }
            currentCap = null;
            resultText.text = "GG";
            bpm = BPM.Slow;
            miniGamePassedNumber = 0;
            currentMiniGame = 0;

            //REACTIVER LES INPUTS MACRO
        }

        /// <summary>
        /// reset the difficulty and apparitionPurcentage of all games
        /// </summary>
        public void ResetIDCards()
        {
            foreach (IDCardList list in sorter.sortedIdCards)
            {
                foreach (IDCard idCard in list.IDCards)
                {
                    idCard.currentDifficulty = 0;
                    idCard.idWeight = 10;
                }
            }
        }

        /// <summary>
        /// create a cap for each island depending on cap number
        /// </summary>
        public void CapAttribution()
        {
            foreach (Island island in islandList)
            {
                for (int i = 0; i < island.accessibleNeighbours.Length; i++)
                {
                    island.capList.Add(new Cap());
                    island.capList[i].capWeight = capWeightToAdd;
                    island.capList[i].listWeight = listWeightToAdd;
                    if((int)island.difficulty > 2)
                        island.capList[i].length = 6 + zoneNumber;
                    else
                        island.capList[i].length = (int)island.difficulty + 4 + zoneNumber;
                    island.capList[i].ChoseIdList(sorter);
                    island.capList[i].ChoseMiniGames();
                }
                
            }
        }
        #endregion
    }

    #region enum
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }
    public enum BPM
    {
        Slow = 60,
        Medium = 90,
        Fast = 120,
        SuperFast = 140
    }
    #endregion
}