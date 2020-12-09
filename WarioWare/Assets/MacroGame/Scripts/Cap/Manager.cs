using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SD_UsualAction;
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
        public float verbeTime;
        public int numberBeforeSpeedUp;
        //int that will be added on the id to make it appear more offen, they all start with a value of 10
        public int capWeightToAdd;
        public int listWeightToAdd;

        [Header ("other stuff")]
        public CapsSorter sorter;
        public List<TemporaryIsland> islandList = new List<TemporaryIsland>();
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
            yield return new WaitForSeconds(verbeTime);
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
            currentCap = null;
            resultText.text = "GG";
            bpm = BPM.Slow;
            miniGamePassedNumber = 0;
            currentMiniGame = 0;
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
        /// this is a temporaly mesur, I'm waiting for the real island to be created
        /// </summary>
        public void IslandCreation()
        {
            for (int i = 0; i < 15; i++)
            {
                islandList.Add(new TemporaryIsland());
                islandList[i].size = Random.Range(0, 3);
                islandList[i].capNumber = Random.Range(1, 3);
            }
        }

        /// <summary>
        /// create a cap for each island depending on cap number
        /// </summary>
        public void CapAttribution()
        {
            foreach (TemporaryIsland island in islandList)
            {
                for (int i = 0; i < island.capNumber; i++)
                {
                    island.cap.Add(new Cap());
                    island.cap[i].capWeight = capWeightToAdd;
                    island.cap[i].listWeight = listWeightToAdd;
                    island.cap[i].length = island.size + 4 + zoneNumber;
                    island.cap[i].ChoseIdList(sorter);
                    island.cap[i].ChoseMiniGames();
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