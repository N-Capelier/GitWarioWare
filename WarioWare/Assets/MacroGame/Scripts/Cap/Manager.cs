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
using Rewards;
using DG.Tweening;
using Cinemachine;
using Shop;
using Boss;
using UnityEngine.EventSystems;
using System.Net;
using UnityEngine.U2D;

namespace Caps
{
    public class Manager : Singleton<Manager>
    {
        private void Awake()
        {
            CreateSingleton();
            ResetIDCards();
            eventSystem = EventSystem.current;
        }

        #region Variables
        [Header("Playtest variable")]
        public float transitionTime;
        //  public float verbTime;
        public int numberBeforSpeedUp;
        //int that will be added on the id to make it appear more offen, they all start with a value of 10
        public int idWeightToAdd;
        public int idInitialWeight;
        [SerializeField] int damagesOnMiniGameLose = 10;
        [SerializeField] public int moralCost = 10;
        [HideInInspector]public int _moralCost = 10;
        [Header("BronzeChest")]
        [SerializeField] int monnaieBronze = 3;
        [Header("SilverChest")]
        [SerializeField] int monnaieSilver = 6;
        [Header("GoldChest")]
        [SerializeField] int monnaieGold = 9;
        private int currentCompletionChest;
        /*   //barrel
           [Range(1, 90)]
           public int barrelProbability;
           public int maxBarrelRessources;
           public int minBarrelRessources;
           public int lifeWeight;
           public int goldWeight;
           public int foodWeight;
        */

        [Header("Parameters")]
        public CapsSorter sorter;
        [HideInInspector] public Island[] allIslands;
        public int zoneNumber;

        private int currentMiniGame;
        private Island currentIsland;
        private int miniGamePassedNumber;
        private AsyncOperation currentAsyncScene;
        [HideInInspector] public int macroSceneIndex;
        private Cap currentCap;
        [HideInInspector] public bool isLoaded;
        public BPM bpm = BPM.Slow;

        public Difficulty currentDifficulty;
        public bool isNormalMode;
        private float miniGameWon;

        [Header("UI Management")]
        public GameObject panel;
        public TextMeshProUGUI resultText;
        public GameObject verbePanel;
        public TextMeshProUGUI verbeText;
        public Image inputImage;
        public Image secondInputImage;
        public Image firstInputImage;
        public GameObject sceneCam;
        public GameObject capUI;
        public GameObject macroUI;
        public GameObject ressourcesUI;
        public GameObject playerHealthUI;
        public TextMeshProUGUI idName;
        public GameObject clock;


        [Header("Transition")]
        public TransitionAnimations transition;
        public Camera transitionCam;
        public AudioSource transitionMusic;
        [HideInInspector] public bool cantDoTransition = true;
        private Transform initalCamTransform;
        public Visual_IslandDescriptionOpening shipOpening;
        public GameObject VcamTarget;
        public CinemachineVirtualCamera cinemachine;
        [HideInInspector] public bool cantDisplayVerbe;
        public GameObject speedUp;
        public GameObject victory;
        [HideInInspector] public bool zoomed;
        private bool isFirstMiniGame = true;
        [Header("Debug")]
        [SerializeField] bool isDebug = false;
        [HideInInspector] public EventSystem eventSystem;
        public int miniGameNumberPerCap = 4;
        public int winingStreakNumber = 2;
        public int losingStreakNumber = 2;
        //events
        //public delegate void MapUIHandler();
        //public event MapUIHandler ResetFocus;
        #endregion

        #region Methods

        private void Start()
        {
            initalCamTransform = VcamTarget.transform;
            //set up value from debug
            numberBeforSpeedUp = (int)DebugToolManager.Instance.ChangeVariableValue("numberBeforSpeedUp");
            idWeightToAdd = (int)DebugToolManager.Instance.ChangeVariableValue("idWeightToAdd");
            idInitialWeight = (int)DebugToolManager.Instance.ChangeVariableValue("idInitialWeight");
            damagesOnMiniGameLose = (int)DebugToolManager.Instance.ChangeVariableValue("damagesOnMiniGameLose");
            winingStreakNumber = (int)DebugToolManager.Instance.ChangeVariableValue("winingStreakNumber");
            losingStreakNumber = (int)DebugToolManager.Instance.ChangeVariableValue("losingStreakNumber");
            miniGameNumberPerCap = (int)DebugToolManager.Instance.ChangeVariableValue("miniGameNumberPerCap");
            cantDoTransition = true;
        }



        /// <summary>
        /// lunch a cap, if call within a cap, lunch the next mini game. If cap is already done, lunch CapEnd.
        /// </summary>
        /// <param name="_currentCap"></param>
        /// <returns></returns>
        public IEnumerator StartMiniGame(Cap _currentCap, Island _currentIsland ,Camera _transitionCam  = null, bool isBoss = false)
        {
            UI.UICameraController.canSelect = false;
            if (_transitionCam == null)
                _transitionCam = transitionCam;

            int _keyStoneImpact = Mathf.RoundToInt(KeystoneReward.keystoneCount / 2f);
            numberBeforSpeedUp = 1 + Mathf.RoundToInt(((float)_currentIsland.difficulty + 1f) / 5f) + _keyStoneImpact + Mathf.RoundToInt(1f / (1f + _keyStoneImpact));
            cantDoTransition = false;
            currentCap = _currentCap;
            currentIsland = _currentIsland;

            if (currentAsyncScene == null)
            {
                
                BossLifeManager.Instance.bossUI.gameObject.SetActive(false);
                if (currentIsland.type == IslandType.Boss || currentIsland.type == IslandType.Keystone)
                {
                    StartCoroutine(BossManager.Instance.StartBoss(sorter, currentCap, currentIsland.type));
                    yield break;
                }
                else
                {
                    _currentCap.ChoseMiniGames(sorter);

                }

                shipOpening.gameObject.SetActive(true);

                StartCoroutine(ZoomCam(shipOpening.openingTime));
                transition.DisplayBarrel(_currentCap);

                //if zoom is bugging, look at here
                yield return new WaitForSeconds(shipOpening.openingTime * 2);
                if (currentCap.isDone)
                {
                    if (isLureActive > 0)
                    {
                        isLure = true;
                    }
                    PlayerMovement.Instance.playerAvatar.transform.position = _currentIsland.transform.position;
                    initalCamTransform = PlayerMovement.Instance.playerAvatar.transform;
                    StartCoroutine(CapEnd());
                    yield break;
                }
            }

            StartCoroutine(PlayMiniGame(_transitionCam, isBoss));
        }
        public IEnumerator StartMiniGame(Cap _currentCap)
        {
            //_currentCap.ChoseMiniGames(sorter);

            cantDoTransition = false;
            currentCap = _currentCap;

            if (currentAsyncScene == null)
            {
                shipOpening.gameObject.SetActive(true);

                StartCoroutine(ZoomCam(shipOpening.openingTime * 2));
                //transition.DisplayBarrel(_currentCap);

                //if zoom is bugging, look at here
                yield return new WaitForSeconds(shipOpening.openingTime * 2);

            }

            if (isLureActive > 0)
            {
                isLure = true;
            }
            StartCoroutine(PlayMiniGame(transitionCam));
        }


        public IEnumerator PlayMiniGame(Camera _transitionCam, bool isBoss = false)
        {
            _transitionCam.enabled = true;
            sceneCam.SetActive(true);
            if (speedUp.activeSelf)
                speedUp.SetActive(false);
            capUI.SetActive(true);
            macroUI.SetActive(false);
            ressourcesUI.SetActive(false);
            playerHealthUI.SetActive(false);


            FadeManager.Instance.NoPanel();
            if (!isBoss)
                SoundManager.Instance.ApplyAudioClip("verbeJingle", transitionMusic, bpm);
            else
            {
                if(currentIsland.type == IslandType.Boss)
                    SoundManager.Instance.ApplyAudioClip("verbeJingleBoss", transitionMusic, bpm);
                else
                    SoundManager.Instance.ApplyAudioClip("verbeJingleMiniBoss", transitionMusic, bpm);

            }
            transitionMusic.PlaySecured();

            verbePanel.SetActive(true);

            if (currentAsyncScene == null)
            {
                currentDifficulty = currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;
                isLoaded = false;
                currentAsyncScene = SceneManager.LoadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
                currentAsyncScene.allowSceneActivation = false;
                macroSceneIndex = SceneManager.GetActiveScene().buildIndex;

            }
            if (cantDisplayVerbe)
            {

                verbeText.text = "********";
            }
            else
            {
                if (currentCap.chosenMiniGames[currentMiniGame].asSecondSprite)
                {
                    secondInputImage.enabled = true;
                    firstInputImage.enabled = true;
                    inputImage.enabled = false;

                    secondInputImage.sprite = currentCap.chosenMiniGames[currentMiniGame].secondSprite;
                    firstInputImage.sprite = currentCap.chosenMiniGames[currentMiniGame].inputs;

                }
                else
                {
                    secondInputImage.enabled = false;
                    firstInputImage.enabled = false;
                    inputImage.enabled = true;

                    inputImage.sprite = currentCap.chosenMiniGames[currentMiniGame].inputs;

                }
                verbeText.text = currentCap.chosenMiniGames[currentMiniGame].verbe;
            }

           // idName.text = currentCap.chosenMiniGames[currentMiniGame].name;

            //yield return new WaitForSeconds((verbTime-0.25f) * 60 / (float)bpm);
            yield return new WaitForSeconds(transitionMusic.clip.length);


            currentAsyncScene.allowSceneActivation = true;
            yield return new WaitUntil(() => currentAsyncScene.isDone);
            sceneCam.SetActive(false);
            panel.SetActive(false);
            _transitionCam.enabled = false;

            verbePanel.SetActive(false);
            isLoaded = true;
            Scene scene = SceneManager.GetSceneByBuildIndex(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            SceneManager.SetActiveScene(scene);
            clock.SetActive(true);
            clock.GetComponent<UI.Clock>().Reset();
        }

        /// <summary>
        /// Debug the result of the game on a panel
        /// </summary>
        /// <param name="win"> if true the game is won , if false the game is lost</param>
        public void Result(bool win)
        {
            if (!cantDoTransition)
            {
                StartCoroutine(GlobalTransitionStart(win));
            }
        }

        private IEnumerator GlobalTransitionStart(bool win)
        {
            clock.SetActive(false);

            //little fade
            StartCoroutine(FadeManager.Instance.FadeIn(0.15f * 60 / (float)bpm));
            yield return new WaitForSeconds(0.25f * 60 / (float)bpm);
            //panel.SetActive(true);
            sceneCam.SetActive(true);
            SceneManager.UnloadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            playerHealthUI.SetActive(true);

            // difficutly chnage ever wining or losing streak
            if (win)
            {
                currentCap.chosenMiniGames[currentMiniGame].winningStreak++;
                currentCap.chosenMiniGames[currentMiniGame].losingStreak = 0;
                if (currentCap.chosenMiniGames[currentMiniGame].winningStreak == winingStreakNumber && currentCap.chosenMiniGames[currentMiniGame].currentDifficulty != Difficulty.HARD)
                {
                    currentCap.chosenMiniGames[currentMiniGame].currentDifficulty++;
                    currentCap.chosenMiniGames[currentMiniGame].winningStreak = 0;
                }
            }
            else
            {
                currentCap.chosenMiniGames[currentMiniGame].winningStreak = 0;
                currentCap.chosenMiniGames[currentMiniGame].losingStreak++;
                if (currentCap.chosenMiniGames[currentMiniGame].losingStreak == losingStreakNumber && currentCap.chosenMiniGames[currentMiniGame].currentDifficulty != Difficulty.EASY)
                {
                    currentCap.chosenMiniGames[currentMiniGame].currentDifficulty--;
                    currentCap.chosenMiniGames[currentMiniGame].losingStreak = 0;
                }
            }


            currentMiniGame++;

            isLoaded = false;

            if (currentIsland != null && currentIsland.type == IslandType.Boss || currentIsland != null && currentIsland.type == IslandType.Keystone)
            {
                StartCoroutine(BossManager.Instance.TransitionBoss(win));
            }
            else
            {
                StartCoroutine(Transition(win));
            }
        }

        [HideInInspector] public int isLureActive = 0;
        [HideInInspector] public bool isLure = false;

        /// <summary>
        /// make the transition within a cap
        /// </summary>
        /// <returns></returns>
        private IEnumerator Transition(bool win)
        {
            if (isNormalMode)
            {
                transition.MoveShip(currentCap, miniGamePassedNumber, transitionMusic.clip.length * 3 / 4, win);
            }
            transitionCam.enabled = true;

            StartCoroutine(FadeManager.Instance.FadeOut(0.15f * 60 / (float)bpm));

            #region resultConsequences
            //transitionMusic.PlayDelayed((transitionTime - 0.5f) * 60 / (float)bpm);

            if (win)
            {
                miniGameWon++;
                if (isNormalMode)
                {
                    transition.CompletionBar(miniGameWon / currentCap.length, 60f / (float)bpm);
                }
                transition.PlayAnimation((float)bpm, win);
                SoundManager.Instance.ApplyAudioClip("victoryJingle", transitionMusic, bpm);
                resultText.text = "You Won!";
                /* if (currentCap.hasBarrel[miniGamePassedNumber] && isNormalMode)
                 {
                     BarrelRessourcesContent();
                 }*/
            }
            else
            {
                transition.PlayAnimation((float)bpm, win);
                if (isNormalMode)
                {
                    if (isLure)
                    {
                        isLure = false;
                    }
                    else
                    {
                        PlayerManager.Instance.TakeDamage(damagesOnMiniGameLose);
                    }

                    if (PlayerManager.Instance.playerHp > 0)
                        resultText.text = "Vous avez perdu !";
                    else
                    {
                        resultText.text = "Vous êtes mort !";
                        yield break;
                    }
                }

                SoundManager.Instance.ApplyAudioClip("loseJingle", transitionMusic, bpm);
            }
            if (isNormalMode)
            {
                //check if not dead and proceed
                if (PlayerManager.Instance.playerHp <= 0)
                {
                    yield break;
                }
            }
            //play victory/lose jingle and wait for jingle to finish
            transitionMusic.PlaySecured();
            yield return new WaitForSeconds(transitionMusic.clip.length);
            #endregion

            miniGamePassedNumber++;

            

            if (miniGamePassedNumber % numberBeforSpeedUp == 0 && currentCap.length != miniGamePassedNumber)
            {
                //play speed up jingle and wait for jingle to finish
                SoundManager.Instance.ApplyAudioClip("speedUpJingle", transitionMusic, bpm);
                transitionMusic.PlaySecured();
                speedUp.SetActive(true);
                transition.SpeedUp((float)bpm);
                yield return new WaitForSeconds(transitionMusic.clip.length);
                bpm = bpm.Next();
            }
            if ((currentCap.length == miniGamePassedNumber) || (isDebug && Input.GetKey(KeyCode.RightArrow)))
            {
                StartCoroutine(CapEnd());
                yield break;
            }
            GlobalTransitionEnd(transitionCam);

        }

        public void GlobalTransitionEnd(Camera _transtionCam ,bool isBoss = false)
        {
            if (currentMiniGame == currentCap.chosenMiniGames.Count)
            {
                for (int i = 0; i < currentCap.chosenMiniGames.Count - 1; i++)
                {
                    int j = Random.Range(i, currentCap.chosenMiniGames.Count);
                    var temp = currentCap.chosenMiniGames[i];
                    currentCap.chosenMiniGames[i] = currentCap.chosenMiniGames[j];
                    currentCap.chosenMiniGames[j] = temp;
                }
                currentMiniGame = 0;
            }
            currentDifficulty = currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;

            currentAsyncScene = SceneManager.LoadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
            currentAsyncScene.allowSceneActivation = false;
            if (currentIsland != null)
                StartCoroutine(StartMiniGame(currentCap, currentIsland,_transtionCam, isBoss));
            else
                StartCoroutine(StartMiniGame(currentCap));

        }

        /// <summary>
        /// reset values
        /// </summary>
        public IEnumerator CapEnd(bool isOver = false)
        {
            #region resetValue;

            bool _giveReward = true;
            if (currentCap.isDone || PlayerMovement.Instance.playerIsland.isDone)
                _giveReward = false;

            currentCap.isDone = true;

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
                if (_islandToGo.accessibleNeighbours[i] == _island)
                {
                    _islandToGo.capList[i].isDone = true;
                }
            }

            //new isDone trails
            SpriteShapeRenderer[] _islandTrails = _island.traiList.ToArray();
            SpriteShapeRenderer[] _islandToGoTrails = _islandToGo.traiList.ToArray();

            for (int i = 0; i < _islandTrails.Length; i++)
            {
                for (int j = 0; j < _islandToGoTrails.Length; j++)
                {
                    if(_islandTrails[i] == _islandToGoTrails[j])
                    {
                        _islandTrails[i].materials[1].SetInt("bool_IsDone", 1);
                    }
                }
            }

            currentAsyncScene = null;

            resultText.text = "GG";
            bpm = BPM.Slow;
            miniGamePassedNumber = 0;
            currentMiniGame = 0;
            capUI.SetActive(false);


            Scene scene = SceneManager.GetSceneByBuildIndex(macroSceneIndex = SceneManager.GetActiveScene().buildIndex);
            SceneManager.SetActiveScene(scene);
            #endregion



            //reward attribution
            if (_giveReward)
            {
                PlayerMovement.Instance.playerIsland.isDone = true;
                capUI.SetActive(false);
                macroUI.SetActive(true);
                BossLifeManager.Instance.bossUI.gameObject.SetActive(true);
                PlayerMovement.Instance.ResetFocus();
                eventSystem.enabled = false;


                transitionCam.enabled = false;

                sceneCam.SetActive(true);
                StartCoroutine(UnzoomCam());
                yield return new WaitForSeconds(shipOpening.openingTime * 2);

                StartCoroutine(RewardUI(isOver));

            }
            else
            {
                StartCoroutine(UnzoomCam());
                currentCap = null;
                UI.UICameraController.canSelect = true;

            }


            //REACTIVER LES INPUTS MACRO
        }

        /// <summary>
        /// reset the difficulty and apparitionPurcentage of all games
        /// </summary>
        public void ResetIDCards()
        {
            sorter.iDCardsPlayed = new List<IDCard>();
            foreach (IDCard idCard in sorter.idCards)
            {
                idCard.currentDifficulty = 0;
                idCard.idWeight = idInitialWeight;
                idCard.winningStreak = 0;
                idCard.losingStreak = 0;
            }

        }

        /// <summary>
        /// create a cap for each island depending on cap number
        /// </summary>
        public void CapAttribution()
        {
            if (zoneNumber <= 2)
            {

                sorter.idCardsNotPlayed = sorter.idCards;

            }
            else
            {
                sorter.idCardsNotPlayed = new List<IDCard>();
                foreach (IDCard id in sorter.idCards)
                {
                    if (!sorter.iDCardsPlayed.Contains(id))
                        sorter.idCardsNotPlayed.Add(id);
                }
            }
            foreach (Island island in allIslands)
            {

                for (int i = 0; i < island.accessibleNeighbours.Length; i++)
                {

                    island.capList.Add(new Cap());
                    Island _IslandTarget = island.accessibleNeighbours[i];
                    if (_IslandTarget.type == IslandType.Boss)
                    {
                        island.capList[i].length = BossManager.Instance.differentMiniGameNumber * 2;
                        sorter.bossList = new List<IDCard>();
                        for (int x = 0; x < 12; x++)
                        {
                            var random = Random.Range(0, sorter.idCards.Count);
                            if (sorter.bossList.Contains(sorter.idCards[random]))
                                x--;
                            else
                                sorter.bossList.Add(sorter.idCards[random]);
                        }
                    }
                    else
                    {
                        int keyStoneImpact = KeystoneReward.keystoneCount / 2;
                        island.capList[i].length = 3 * ((int)_IslandTarget.difficulty + 1) + miniGameNumberPerCap + KeystoneReward.keystoneCount - 2 * keyStoneImpact;
                    }

                    island.capList[i].capWeight = idWeightToAdd;

                }

            }
            transition.MoveShip(allIslands[0].capList[0], 3, 0);
        }

        [HideInInspector] public int bonusBarrels = 0;


        public void KeyStoneReset()
        {
            foreach (Island island in allIslands)
            {

                for (int i = 0; i < island.accessibleNeighbours.Length; i++)
                {
                    Island _IslandTarget = island.accessibleNeighbours[i];
                    if (_IslandTarget.type != IslandType.Boss)
                    {
                        int keyStoneImpact = KeystoneReward.keystoneCount / 2;
                        island.capList[i].length = 3 * ((int)_IslandTarget.difficulty + 1) + miniGameNumberPerCap + KeystoneReward.keystoneCount - 2 * keyStoneImpact;

                    }
                }
            }
            List<IDCard> _strongerId = new List<IDCard>();


            for (int i = 0; i < 3; i++)
            {
                _strongerId.Add(new IDCard());
            }

            for (int i = 0; i < sorter.iDCardsPlayed.Count; i++)
            {
                var _idcard = sorter.iDCardsPlayed[i];
                for (int x = 0; x < _strongerId.Count; x++)
                {
                    if (_idcard.idWeight == _strongerId[x].idWeight)
                    {
                        int random = Random.Range(0, 2);
                        if (random == 0)
                            _idcard = _strongerId[x];
                        else
                            break;
                    }
                }

                if (sorter.iDCardsPlayed[i].idWeight > _strongerId[2].idWeight)
                {
                    _strongerId[0] = _strongerId[1];
                    _strongerId[1] = _strongerId[2];
                    _strongerId[2] = sorter.iDCardsPlayed[i];
                }
                else if (sorter.iDCardsPlayed[i].idWeight < _strongerId[2].idWeight && sorter.iDCardsPlayed[i].idWeight > _strongerId[1].idWeight)
                {
                    _strongerId[0] = _strongerId[1];
                    _strongerId[1] = sorter.iDCardsPlayed[i];
                }
                else if (sorter.iDCardsPlayed[i].idWeight < _strongerId[1].idWeight && sorter.iDCardsPlayed[i].idWeight > _strongerId[0].idWeight)
                {
                    _strongerId[0] = sorter.iDCardsPlayed[i];
                }

            }


            foreach (var id in _strongerId)
            {
                for (int i = 0; i < sorter.bossList.Count; i++)
                {
                    if (id.idWeight > sorter.bossList[i].idWeight)
                    {
                        if (i == sorter.bossList.Count - 1)
                        {
                            for (int x = 0; x < sorter.bossList.Count - 1; x++)
                            {
                                sorter.bossList[x] = sorter.bossList[x + 1];
                            }
                            sorter.bossList[sorter.bossList.Count - 1] = id;
                            break;
                        }
                        else if (id.idWeight < sorter.bossList[i + 1].idWeight)
                        {
                            for (int x = 0; x < i; x++)
                            {
                                sorter.bossList[x] = sorter.bossList[x + 1];
                            }
                            sorter.bossList[i] = id;
                            break;
                        }
                    }
                    else if (id.idWeight == sorter.bossList[i].idWeight)
                    {
                        int random = Random.Range(0, 2);
                        if (random == 0)
                            sorter.bossList[i] = id;
                        else
                            break;
                    }

                }
            }

            sorter.iDCardsPlayed = new List<IDCard>();
        }
        private bool isTutoDialogDone;
        private IEnumerator RewardUI(bool isOver =false)
        {
            // eventSystem = EventSystem.current;
            eventSystem.enabled = false;
            if (PlayerMovement.Instance.playerIsland.type != IslandType.Shop)
            {
                PlayerInventory.Instance.rewardImage.sprite = PlayerMovement.Instance.playerIsland.reward.sprite;
            }
            else
            {
                PlayerInventory.Instance.rewardImage.enabled = false;
            }
            PlayerInventory.Instance.rewardCanvas.SetActive(true);
                CompletionAttribution(isOver);
            yield return null;
        }
        public void CloseReward(bool isOver = false)
        {
            //apply object effect if ressource
            PlayerInventory.Instance.rewardCanvas.SetActive(false);
            if (PlayerMovement.Instance.playerIsland.type == IslandType.Shop)
            {
                eventSystem.enabled = true;
                ShopManager.Instance.Show(PlayerMovement.Instance.playerIsland);
                PlayerInventory.Instance.rewardImage.enabled = true;
            }
            else
            {
                if (PlayerMovement.Instance.playerIsland.reward.type != RewardType.Resource)
                {
                    PlayerInventory.Instance.SetItemToAdd(PlayerMovement.Instance.playerIsland.reward, true);
                }
                else
                {
                    if (PlayerMovement.Instance.playerIsland.reward.name != "TreasureChest")
                    {

                    }
                    else
                    {
                        eventSystem.enabled = true;
                    }

                    PlayerMovement.Instance.playerIsland.reward.ApplyPassiveEffect();
                }
                if (isFirstMiniGame)
                {
                    DialogueManager.Instance.PlayDialogue(4, 6);
                    isFirstMiniGame = false;
                }
                else if(KeystoneReward.tutorialCompleted && KeystoneReward.keystoneCount == 0 && !isTutoDialogDone)
                {
                    isTutoDialogDone = true;
                    DialogueManager.Instance.PlayDialogue(10, 6, VcamTarget, allIslands[16].transform,5);
                }
                else if (PlayerMovement.Instance.playerIsland.type == IslandType.Keystone)
                {
                    PlayerInventory.Instance.GetKeyStone(PlayerMovement.Instance.playerIsland.keyStoneIslandReward.rewardName);
                    switch (PlayerMovement.Instance.playerIsland.keyStoneIslandReward.rewardName)
                    {
                        case "Les voiles du Queen Anne’s Revenge":
                            DialogueManager.Instance.PlayDialogue(16,1);
                            break;
                        case "Les canons de l’Adventure Galley":
                            DialogueManager.Instance.PlayDialogue(17, 1);
                            break;
                        case "Figure de proue du Sloop William":
                            DialogueManager.Instance.PlayDialogue(18, 1);
                            break;
                        case "La barre du The William":
                            DialogueManager.Instance.PlayDialogue(19, 1);
                            break;
                        default:
                            break;
                    }
                }
                else if (isOver)
                {
                    DialogueManager.Instance.PlayDialogue(20, 2);
                }
                else
                    eventSystem.enabled = true;
            }


        }

        private void CompletionAttribution( bool isOver =false)
        {
            float pourcentageCompleted = miniGameWon / currentCap.length;
            int currentMonnaie;
            int goldToAdd = 0;
            int additionalGold = 0;
            int woodToAdd = 0;
            int _moralCost = 0;

            if (pourcentageCompleted >= 1f)
            {
                currentMonnaie = monnaieGold;
                while (currentMonnaie > 0)
                {
                    int random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            goldToAdd += 5;
                            currentMonnaie--;
                            break;
                        case 1:
                            if (currentMonnaie >= 2)
                            {
                                currentMonnaie -= 2;
                                _moralCost += 5;
                            }
                            else
                            {
                                while (currentMonnaie > 0)
                                {
                                    goldToAdd += 5;
                                    currentMonnaie--;
                                }
                            }
                            break;
                        case 2:
                            if (currentMonnaie >= 6)
                            {
                                currentMonnaie -= 6;
                                woodToAdd += 10;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            else if (pourcentageCompleted >= 0.5f)
            {
                if (pourcentageCompleted >= 0.75f)
                    currentMonnaie = monnaieSilver;
                else
                    currentMonnaie = monnaieBronze;
                while (currentMonnaie > 0)
                {
                    int random = Random.Range(0, 2);
                    switch (random)
                    {
                        case 0:
                            goldToAdd += 5;
                            currentMonnaie--;
                            break;
                        case 1:
                            if (currentMonnaie >= 2)
                            {
                                currentMonnaie -= 2;
                                _moralCost += 5;
                            }
                            else
                            {
                                while (currentMonnaie > 0)
                                {
                                    goldToAdd += 5;
                                    currentMonnaie--;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            //add moral based gold
            if (PlayerManager.Instance.moral > 0 && PlayerManager.Instance.moral < 25)
            {
                additionalGold += 0;
            }
            else if (PlayerManager.Instance.moral >= 25 && PlayerManager.Instance.moral < 50)
            {
                additionalGold += 5;

            }
            else if (PlayerManager.Instance.moral >= 50 && PlayerManager.Instance.moral < 75)
            {
                additionalGold += 10;
            }
            else if (PlayerManager.Instance.moral >= 75 && PlayerManager.Instance.moral < 100)
            {
                additionalGold += 15;
            }

            PlayerManager.Instance.completionUI.StartCompletion(pourcentageCompleted,isOver);
         
            PlayerManager.Instance.GainCoins(goldToAdd+additionalGold);
            PlayerManager.Instance.Heal(woodToAdd);
            PlayerManager.Instance.GainMoral(moralCost + _moralCost);
            miniGameWon = 0;
            transition.completionBar.fillAmount = 0;

            if (goldToAdd > 0)
                PlayerInventory.Instance.goldCompletion.text = "beatcoin + " + goldToAdd;
            else
                PlayerInventory.Instance.goldCompletion.text = System.String.Empty;

            if (woodToAdd > 0)
                PlayerInventory.Instance.woodCompletion.text = "planches + " + woodToAdd;
            else
                PlayerInventory.Instance.woodCompletion.text = System.String.Empty;

            if (_moralCost > 0)
                PlayerInventory.Instance.moralCompletion.text = "moral + " + _moralCost;
            else
                PlayerInventory.Instance.moralCompletion.text = System.String.Empty;

            currentCap = null;
        }

        #region Cameras
        public IEnumerator ZoomCam(float zoomTime)
        {
            zoomed = true;
            var _position = shipOpening.transform.position + Vector3.left * 13;
            VcamTarget.transform.DOMove(_position, shipOpening.openingTime / 2).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(zoomTime / 2);
            for (float i = 0; i < zoomTime / 2; i += 0.01f)
            {
                cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(400, 72, i * 2 / zoomTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        private IEnumerator ZoomCam(float zoomTime, string dezoom)
        {
            for (float i = 0; i < zoomTime; i += 0.01f)
            {
                cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(72, 400, i / zoomTime);
                yield return new WaitForSeconds(0.01f);
            }
        }

        public IEnumerator UnzoomCam()
        {
            ressourcesUI.SetActive(true);
            eventSystem.enabled = false;
            VcamTarget.transform.position = PlayerMovement.Instance.playerAvatar.transform.position;
            VcamTarget.transform.DOMove(initalCamTransform.position, shipOpening.openingTime * 2).SetEase(Ease.InOutCubic);
            StartCoroutine(ZoomCam(shipOpening.openingTime, "dezoom"));
            yield return new WaitForSeconds(shipOpening.openingTime * 2);
            eventSystem.enabled = true;
            cantDoTransition = true;
            shipOpening.Close();
            zoomed = false;
        }
        #endregion

        #endregion


    }

}
