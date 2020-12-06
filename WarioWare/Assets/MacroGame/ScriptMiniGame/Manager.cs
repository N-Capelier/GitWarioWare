using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Caps
{
    public class Manager : Singleton<Manager>
    {       
        private void Awake()
        {
            CreateSingleton(true);
        }

        #region Variables
        [Header ("Playtest variable")]
        [Range(4,10)]
        public int capLength;
        public float transitionTime;
        public float verbeTime;

        [Header ("other stuff")]
        public CapsSorter sorter;

        public ChallengeHaptique challengeHaptique;
        public ChallengeInput challengeInput;

        private List<IDCard> chosenMiniGames = new List<IDCard>();
        private int currentMiniGame;
        private int miniGamePassedNumber;
        private AsyncOperation currentAsyncScene;

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
        public void Start()
        {
            int differentGameNumber = capLength / (int)2;
            if (sorter.chalInput)
            {
                for (int i = 0; i < differentGameNumber; i++)
                {
                    chosenMiniGames.Add(sorter.sortedIdCards[(int)challengeInput+1].IDCards[
                                                                                            Random.Range(0, sorter.sortedIdCards[(int)challengeInput + 1].IDCards.Count)
                                                                                          ]);
                }
            }
            else
            {
                for (int i = 0; i < differentGameNumber; i++)
                {
                    
                    chosenMiniGames.Add(sorter.sortedIdCards[(int)challengeHaptique+1].IDCards[
                                                                                            Random.Range(0, sorter.sortedIdCards[(int)challengeInput+1].IDCards.Count)
                                                                                          ]);
                }
            }
            StartCoroutine(StartCap());
        }

        public IEnumerator StartCap()
        {
            verbePanel.SetActive(true);
            if(currentAsyncScene == null)
            {
                currentDifficulty = chosenMiniGames[currentMiniGame].currentDifficulty;
                currentAsyncScene = SceneManager.LoadSceneAsync(chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
                currentAsyncScene.allowSceneActivation = false;

            }
            verbeText.text = chosenMiniGames[currentMiniGame].verbe;
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

        private IEnumerator Transition()
        {
            sceneCam.SetActive(true);
            SceneManager.UnloadSceneAsync(chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            if (chosenMiniGames[currentMiniGame].currentDifficulty != Difficulty.HARD)
                chosenMiniGames[currentMiniGame].currentDifficulty++;


            currentMiniGame++;
            if (currentMiniGame == capLength / (int)2)
                currentMiniGame = 0;
            miniGamePassedNumber++;
            if(miniGamePassedNumber%4 == 0)
            {
                bpm++;
            }


            currentDifficulty = chosenMiniGames[currentMiniGame].currentDifficulty;
            currentAsyncScene = SceneManager.LoadSceneAsync(chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
            currentAsyncScene.allowSceneActivation = false;

            yield return new WaitForSeconds(transitionTime);
            if(capLength == miniGamePassedNumber)
            {
                resultText.text = "GG";
                bpm = 0;
                miniGamePassedNumber = 0;
                currentMiniGame = 0;
                yield break;
            }
            panel.SetActive(false);
            StartCoroutine(StartCap());
        }
        #endregion


    }
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
}