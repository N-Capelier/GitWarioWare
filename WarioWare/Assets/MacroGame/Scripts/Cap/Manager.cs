using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SD_UsualAction;
using Islands;
using Player;
using UI;
using Sound;
namespace Caps
{
    public class Manager : Singleton<Manager>
    {       
        private void Awake()
        {
            CreateSingleton(true);
            ResetIDCards();
        }

        #region Variables
        [Header("Playtest variable")]
        public float transitionTime;
        public float verbTime;
        public int numberBeforeSpeedUp;
        //int that will be added on the id to make it appear more offen, they all start with a value of 10
        public int capWeightToAdd;
        public int listWeightToAdd;

        //barrel
        [Range(1, 90)]
        public int barrelProbality;
        public int maxBarrelRessources;
        public int minBarrelRessources;
        public int lifeWeight;
        public int goldWeight;
        public int foodWeight;


        [Header ("Parameters")]
        public CapsSorter sorter;
        [HideInInspector] public Island[] allIslands;
        public int zoneNumber;
       
        private int currentMiniGame;
        private int miniGamePassedNumber;
        private AsyncOperation currentAsyncScene;
        private int macroSceneIndex;
        private Cap currentCap;
        [HideInInspector] public bool isLoaded;
        public BPM bpm = BPM.Slow;

        public Difficulty currentDifficulty;

        [Header("UI Management")]
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public GameObject verbePanel;
        public TextMeshProUGUI verbeText;
        public Image inputImage;
        public GameObject sceneCam;
        public GameObject capUI;
        public GameObject macroUI;
        public TextMeshProUGUI idName;

        [Header("Transition")]
        public TransitionAnimations animations;
        public Camera transitionCam;
        public AudioSource transitionMusic;
        //events
        //public delegate void MapUIHandler();
        //public event MapUIHandler ResetFocus;
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
                CapEnd();
                yield break;
            }
            //little fade  
            StartCoroutine(FadeManager.Instance.FadeInAndOut(0.5f * 60 / (float)bpm));
            yield return new WaitForSeconds(0.25f * 60 / (float)bpm);
            capUI.SetActive(true);
            macroUI.SetActive(false);
            panel.SetActive(false);
            verbePanel.SetActive(true);

            if (currentAsyncScene == null)
            {
                currentDifficulty = _currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;
                isLoaded = false;
                currentAsyncScene = SceneManager.LoadSceneAsync(_currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
                currentAsyncScene.allowSceneActivation = false;
                macroSceneIndex = SceneManager.GetActiveScene().buildIndex;

                SoundManager.Instance.ApplyAudioClip("verbeJingle", transitionMusic, bpm);
                transitionMusic.PlaySecured();
            }

            verbeText.text = _currentCap.chosenMiniGames[currentMiniGame].verbe;
            inputImage.sprite = _currentCap.chosenMiniGames[currentMiniGame].inputs;
            idName.text = _currentCap.chosenMiniGames[currentMiniGame].name;

            //yield return new WaitForSeconds((verbTime-0.25f) * 60 / (float)bpm);
            yield return new WaitForSeconds(transitionMusic.clip.length);

            sceneCam.SetActive(false);
            verbePanel.SetActive(false);
            currentAsyncScene.allowSceneActivation = true;

            yield return new WaitUntil(() => currentAsyncScene.isDone);
            isLoaded = true;
            Scene scene = SceneManager.GetSceneByBuildIndex(_currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            SceneManager.SetActiveScene(scene);
        }

        /// <summary>
        /// Debug the result of the game on a panel
        /// </summary>
        /// <param name="win"> if true the game is won , if false the game is lost</param>
        public void Result(bool win)
        {
            
            StartCoroutine(Transition(win));
        }

        /// <summary>
        /// make the transition within a cap
        /// </summary>
        /// <returns></returns>
        private IEnumerator Transition(bool win)
        {
            panel.SetActive(true);
            SceneManager.UnloadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            if (currentCap.chosenMiniGames[currentMiniGame].currentDifficulty != Difficulty.HARD)
                currentCap.chosenMiniGames[currentMiniGame].currentDifficulty++;

            isLoaded = false;
            sceneCam.SetActive(true);

            currentMiniGame++;

            //little fade
            StartCoroutine(FadeManager.Instance.FadeInAndOut(0.5f * 60 / (float)bpm));
            yield return new WaitForSeconds(0.25f * 60 / (float)bpm);

            #region resultConsequences
            transitionCam.enabled = true;
            //transitionMusic.PlayDelayed((transitionTime - 0.5f) * 60 / (float)bpm);
            
            if (win)
            {
                SoundManager.Instance.ApplyAudioClip("victoryJingle", transitionMusic, bpm);
                resultText.text = "You Won!";
                if (currentCap.hasBarrel[miniGamePassedNumber])
                {
                    BarrelressourcesContente();
                }
            }
            else
            {
                animations.PlayAnimation((float)bpm, false);
                resultText.text = "You Lost!";
                PlayerManager.Instance.TakeDamage(1);
                SoundManager.Instance.ApplyAudioClip("loseJingle", transitionMusic, bpm);
            }

            //play victory/lose jingle and wait for jingle to finish
            transitionMusic.PlaySecured();
            yield return new WaitForSeconds(transitionMusic.clip.length);
            #endregion

            if (currentMiniGame == currentCap.chosenMiniGames.Count)
                currentMiniGame = 0;


            miniGamePassedNumber++;

            if(miniGamePassedNumber%numberBeforeSpeedUp == 0 && currentCap.length != miniGamePassedNumber)
            {
                //play speed up jingle and wait for jingle to finish
                SoundManager.Instance.ApplyAudioClip("speedUpJingle", transitionMusic, bpm);
                transitionMusic.PlaySecured();
                resultText.text = "Speed Up!";

                yield return new WaitForSeconds(transitionMusic.clip.length);
                bpm = bpm.Next(); 
            }

            currentDifficulty = currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;
           
            currentAsyncScene = SceneManager.LoadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
            currentAsyncScene.allowSceneActivation = false;

            if (currentCap.length != miniGamePassedNumber)
            {
                SoundManager.Instance.ApplyAudioClip("verbeJingle", transitionMusic, bpm);
                transitionMusic.PlaySecured();
            }

            transitionCam.enabled = false;
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
            SceneManager.UnloadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);

            Island _island = null;
            foreach (var island in allIslands)
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
            macroUI.SetActive(true);
            capUI.SetActive(false);

            PlayerMovement.Instance.ResetFocus();
            PlayerInventory.Instance.SetItemToAdd(PlayerMovement.Instance.playerIsland.reward);

            Scene scene = SceneManager.GetSceneByBuildIndex(macroSceneIndex = SceneManager.GetActiveScene().buildIndex);
            SceneManager.SetActiveScene(scene);

            //REACTIVER LES INPUTS MACRO
        }

        /// <summary>
        /// reset the difficulty and apparitionPurcentage of all games
        /// </summary>
        public void ResetIDCards()
        {
            foreach (IDCard idCard in sorter.idCards)
                {
                    idCard.currentDifficulty = 0;
                    idCard.idWeight = 10;
                }
            
        }

        /// <summary>
        /// create a cap for each island depending on cap number
        /// </summary>
        public void CapAttribution()
        {
            foreach (Island island in allIslands)
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
                    island.capList[i].ChoseMiniGames(barrelProbality, sorter);
                }
                
            }
        }

        private void BarrelressourcesContente()
        {
            var _size = Random.Range(minBarrelRessources, maxBarrelRessources);
            int _goldAmount = 0;
            int _lifeAmount = 0;
            int _foodAmount = 0;
            for (int i = 0; i < _size; i++)
            {
                int _weight = goldWeight + lifeWeight + foodWeight;
                var _random = Random.Range(0, _weight);
                if (_random < goldWeight)
                    _goldAmount++;
                else if (_random < goldWeight + lifeWeight)
                    _lifeAmount++;
                else if (_random < goldWeight + lifeWeight + foodWeight)
                    _foodAmount++;
            }
            PlayerManager.Instance.GainCoins(_goldAmount);
            PlayerManager.Instance.GainFood(_foodAmount);
            PlayerManager.Instance.Heal(_lifeAmount);
        }
        #endregion
    }

}
