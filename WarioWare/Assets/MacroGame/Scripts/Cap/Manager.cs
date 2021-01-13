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

namespace Caps
{
    public class Manager : Singleton<Manager>
    {       
        private void Awake()
        {
            CreateSingleton();
            ResetIDCards();
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
        //barrel
        [Range(1, 90)]
        public int barrelProbability;
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
        private Island currentIsland;
        private int miniGamePassedNumber;
        private AsyncOperation currentAsyncScene;
       [HideInInspector] public int macroSceneIndex;
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
        [Header("Debug")]
        [SerializeField] bool isDebug = false;


        //events
        //public delegate void MapUIHandler();
        //public event MapUIHandler ResetFocus;
        #endregion

        #region Methods

        private void Start()
        {
            initalCamTransform = VcamTarget.transform;
            //set up value from debug
            numberBeforSpeedUp = DebugToolManager.Instance.ChangeVariableValue("numberBeforSpeedUp");
            idWeightToAdd = DebugToolManager.Instance.ChangeVariableValue("idWeightToAdd");
            idInitialWeight = DebugToolManager.Instance.ChangeVariableValue("idInitialWeight");
            damagesOnMiniGameLose = DebugToolManager.Instance.ChangeVariableValue("damagesOnMiniGameLose");
            barrelProbability = DebugToolManager.Instance.ChangeVariableValue("barrelProbability");
            maxBarrelRessources = DebugToolManager.Instance.ChangeVariableValue("maxBarrelRessources");
            minBarrelRessources = DebugToolManager.Instance.ChangeVariableValue("minBarrelRessources");
            lifeWeight = DebugToolManager.Instance.ChangeVariableValue("lifeWeight");
            goldWeight = DebugToolManager.Instance.ChangeVariableValue("goldWeight");
            foodWeight = DebugToolManager.Instance.ChangeVariableValue("foodWeight");
            cantDoTransition = true;
        }

       

        /// <summary>
        /// lunch a cap, if call within a cap, lunch the next mini game. If cap is already done, lunch CapEnd.
        /// </summary>
        /// <param name="_currentCap"></param>
        /// <returns></returns>
        public IEnumerator StartMiniGame(Cap _currentCap, Island _currentIsland, Malediction malediction = null)
        {
            cantDoTransition = false;
            currentCap = _currentCap;
            currentIsland = _currentIsland;

            if (currentAsyncScene == null)
            {
                BossLifeManager.Instance.bossUI.gameObject.SetActive(false);
                if(currentIsland.type == IslandType.Boss)
                {
                    StartCoroutine(BossManager.Instance.StartBoss());
                    yield break;
                }

                shipOpening.gameObject.SetActive(true);
                
                StartCoroutine(ZoomCam(shipOpening.openingTime));
                transition.DisplayBarrel(_currentCap);

                //if zoom is bugging, look at here
                yield return new WaitForSeconds(shipOpening.openingTime*2);
                if (currentCap.isDone)
                {
                if(isLureActive)
                {
                    isLure = true;
                }
                    StartCoroutine(CapEnd());
                    PlayerMovement.Instance.playerAvatar.transform.position = _currentIsland.transform.position;
                    initalCamTransform = PlayerMovement.Instance.playerAvatar.transform;
                    yield break;
                }
            }

            /* StartCoroutine(FadeManager.Instance.FadeIn(0.15f * 60 / (float)bpm));
             yield return new WaitForSeconds(0.5f * 60 / (float)bpm);*/
            
            StartCoroutine(PlayMiniGame(transitionCam, malediction));
        }
        public IEnumerator StartMiniGame(Cap _currentCap)
        {
            cantDoTransition = false;
            currentCap = _currentCap;

            if (currentAsyncScene == null)
            {
                shipOpening.gameObject.SetActive(true);

                StartCoroutine(ZoomCam(shipOpening.openingTime*2));
                transition.DisplayBarrel(_currentCap);

                //if zoom is bugging, look at here
                yield return new WaitForSeconds(shipOpening.openingTime * 2);
                
            }

            /* StartCoroutine(FadeManager.Instance.FadeIn(0.15f * 60 / (float)bpm));
             yield return new WaitForSeconds(0.5f * 60 / (float)bpm);*/
            StartCoroutine(PlayMiniGame(transitionCam));
        }


        public IEnumerator PlayMiniGame(Camera _transitionCam, Malediction malediction = null)
        {
            
            sceneCam.SetActive(true);
            _transitionCam.enabled = false;
            capUI.SetActive(true);
            macroUI.SetActive(false);
            panel.SetActive(false);
            verbePanel.SetActive(true);
            FadeManager.Instance.NoPanel();
            SoundManager.Instance.ApplyAudioClip("verbeJingle", transitionMusic, bpm);
            transitionMusic.PlaySecured();
            if (currentAsyncScene == null)
            {
                currentDifficulty = currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;
                isLoaded = false;
                currentAsyncScene = SceneManager.LoadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
                currentAsyncScene.allowSceneActivation = false;
                macroSceneIndex = SceneManager.GetActiveScene().buildIndex;

            }

            verbeText.text = currentCap.chosenMiniGames[currentMiniGame].verbe;
            inputImage.sprite = currentCap.chosenMiniGames[currentMiniGame].inputs;
            idName.text = currentCap.chosenMiniGames[currentMiniGame].name;

            //yield return new WaitForSeconds((verbTime-0.25f) * 60 / (float)bpm);
            yield return new WaitForSeconds(transitionMusic.clip.length);


            if(malediction != null)
            {
                
                verbeText.text = "Malediction";
                yield return new WaitForSeconds(malediction.timer * 10 / (float)bpm);
                verbeText.text = "Malediction " + "     " + malediction.maledictionName;
                malediction.StartMalediction();
                yield return new WaitForSeconds(malediction.timer * 50 / (float)bpm);
            }
            currentAsyncScene.allowSceneActivation = true;

            yield return new WaitUntil(() => currentAsyncScene.isDone);
            sceneCam.SetActive(false);

            verbePanel.SetActive(false);
            isLoaded = true;
            Scene scene = SceneManager.GetSceneByBuildIndex(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            SceneManager.SetActiveScene(scene);
            clock.SetActive(true);
            clock.GetComponent<UI.Clock>().timer = 0;
            clock.GetComponent<UI.Clock>().bpm = (float)bpm;
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

        [HideInInspector] public bool isLureActive = false;
        [HideInInspector] public bool isLure = false;
        private IEnumerator GlobalTransitionStart(bool win)
        {
            clock.SetActive(false);
            
            //little fade
            StartCoroutine(FadeManager.Instance.FadeIn(0.15f * 60 / (float)bpm));
            yield return new WaitForSeconds(0.25f * 60 / (float)bpm);
            //panel.SetActive(true);
            sceneCam.SetActive(true);
            SceneManager.UnloadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex);
            if (currentCap.chosenMiniGames[currentMiniGame].currentDifficulty != Difficulty.HARD)
                currentCap.chosenMiniGames[currentMiniGame].currentDifficulty++;
            currentMiniGame++;

            isLoaded = false;
           
            if (currentIsland!=null  &&currentIsland.type == IslandType.Boss) 
            {
                StartCoroutine(BossManager.Instance.TransitionBoss(win));
            }
            else
            {
                StartCoroutine(Transition(win));
            }
        }

        /// <summary>
        /// make the transition within a cap
        /// </summary>
        /// <returns></returns>
        private IEnumerator Transition(bool win)
        {
            transition.MoveShip(currentCap, miniGamePassedNumber, transitionMusic.clip.length*3/4);
            transitionCam.enabled = true;

            StartCoroutine(FadeManager.Instance.FadeOut(0.15f * 60 / (float)bpm));
            yield return new WaitForSeconds(0.25f * 60 / (float)bpm);

            #region resultConsequences
            //transitionMusic.PlayDelayed((transitionTime - 0.5f) * 60 / (float)bpm);
            
            if (win)
            {
                transition.PlayAnimation((float)bpm, win);
                SoundManager.Instance.ApplyAudioClip("victoryJingle", transitionMusic, bpm);
                resultText.text = "You Won!";
                if (currentCap.hasBarrel[miniGamePassedNumber])
                {
                    BarrelRessourcesContent();
                }
            }
            else
            {
                transition.PlayAnimation((float)bpm, win);

                if(isLure)
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
                    resultText.text = "Vous êtes mort !";
                SoundManager.Instance.ApplyAudioClip("loseJingle", transitionMusic, bpm);
            }

            //check if not dead and proceed
            if (PlayerManager.Instance.playerHp > 0) 
            {
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
                    resultText.text = "Speed Up!";

                    yield return new WaitForSeconds(transitionMusic.clip.length);
                    bpm = bpm.Next();
                }
                if ((currentCap.length == miniGamePassedNumber) || (isDebug && Input.GetKey(KeyCode.RightArrow)))
                {
                    StartCoroutine( CapEnd());
                    yield break;
                }
                GlobalTransitionEnd();
            }         

            
        }
       
        public void GlobalTransitionEnd(Malediction malediction = null)
        {
            if (currentMiniGame == currentCap.chosenMiniGames.Count)
                currentMiniGame = 0;
            currentDifficulty = currentCap.chosenMiniGames[currentMiniGame].currentDifficulty;

            currentAsyncScene = SceneManager.LoadSceneAsync(currentCap.chosenMiniGames[currentMiniGame].microGameScene.BuildIndex, LoadSceneMode.Additive);
            currentAsyncScene.allowSceneActivation = false;
            if(currentIsland != null)
                StartCoroutine(StartMiniGame(currentCap, currentIsland, malediction));
            else
                StartCoroutine(StartMiniGame(currentCap));

        }

        /// <summary>
        /// reset values
        /// </summary>
        private IEnumerator CapEnd()
        {
            #region resetValue;
            bool _giveReward = true;
            if (currentCap.isDone)
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
                if(_islandToGo.accessibleNeighbours[i] == _island)
                {
                    _islandToGo.capList[i].isDone = true;
                }
            }

            currentAsyncScene = null;

            currentCap = null;
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
                if(PlayerMovement.Instance.playerIsland.type == IslandType.Shop)
                {
                    capUI.SetActive(false);
                    macroUI.SetActive(true);
                    BossLifeManager.Instance.bossUI.gameObject.SetActive(true);
                    PlayerMovement.Instance.ResetFocus();


                    transitionCam.enabled = false;

                    sceneCam.SetActive(true);
                    StartCoroutine(UnzoomCam());
                    yield return new WaitForSeconds(shipOpening.openingTime * 2);

                    ShopManager.Instance.Show();
                }
                else
                {
                    StartCoroutine(RewardUI());
                    yield return new WaitForSeconds(3f);


                    transitionCam.enabled = false;

                    sceneCam.SetActive(true);
                }
            }
            else
            {
                StartCoroutine(UnzoomCam());
            }



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
                    idCard.idWeight = idInitialWeight;
                }
            
        }

        /// <summary>
        /// create a cap for each island depending on cap number
        /// </summary>
        public void CapAttribution()
        {
            if(zoneNumber <=2)
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
                    island.capList[i].capWeight = idWeightToAdd;
                    Island _IslandTarget = island.accessibleNeighbours[i];
                    if(_IslandTarget.type == IslandType.Boss)
                    {
                        island.capList[i].length = BossManager.Instance.differentMiniGameNumber*2;
                    }
                    else
                    {
                        if ((int)_IslandTarget.difficulty > 2)
                            island.capList[i].length = 6 + zoneNumber;
                        else
                            island.capList[i].length = (int)_IslandTarget.difficulty + 4 + zoneNumber;
                    }
                    island.capList[i].ChoseMiniGames(barrelProbability, sorter);
                }
                
            }
            transition.DisplayBarrel(allIslands[0].capList[0]);
            transition.MoveShip(allIslands[0].capList[0], 3,0);

        }

        [HideInInspector] public int bonusBarrels = 0;

        private void BarrelRessourcesContent()
        {
            var _size = Random.Range(minBarrelRessources + bonusBarrels, maxBarrelRessources + bonusBarrels);
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


        private IEnumerator RewardUI()
        {
            PlayerInventory.Instance.rewardImage.sprite = PlayerMovement.Instance.playerIsland.reward.sprite;
            PlayerInventory.Instance.rewardCanvas.SetActive(true);

            yield return new WaitForSeconds(3);

            //apply object effect if ressource
            PlayerInventory.Instance.rewardCanvas.SetActive(false);

            if (PlayerMovement.Instance.playerIsland.reward.type != RewardType.Resource)
            {
                PlayerInventory.Instance.SetItemToAdd(PlayerMovement.Instance.playerIsland.reward, true);
            }
            else
            {
                PlayerMovement.Instance.playerIsland.reward.ApplyPassiveEffect();
                macroUI.SetActive(true);
                BossLifeManager.Instance.bossUI.gameObject.SetActive(true);
                capUI.SetActive(false);
                PlayerMovement.Instance.ResetFocus();
                StartCoroutine(UnzoomCam());
            }
        }
        #region Cameras
        public IEnumerator ZoomCam(float zoomTime)
        {
            var _position = shipOpening.transform.position + Vector3.left * 13;
            VcamTarget.transform.DOMove(_position, shipOpening.openingTime/2).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(zoomTime / 2);
            for (float i = 0; i < zoomTime/2; i+= 0.01f)
            {
                cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(400, 72, i*2/zoomTime);
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
            var _system = EventSystem.current;
            _system.enabled = false;
            VcamTarget.transform.position =PlayerMovement.Instance.playerAvatar.transform.position;
            VcamTarget.transform.DOMove(initalCamTransform.position, shipOpening.openingTime * 2).SetEase(Ease.InOutCubic);
            StartCoroutine(ZoomCam(shipOpening.openingTime, "dezoom"));
            yield return new WaitForSeconds(shipOpening.openingTime * 2);
            _system.enabled = true;
            cantDoTransition = true;
            shipOpening.Close();
        }
        #endregion

        #endregion
    }

}
