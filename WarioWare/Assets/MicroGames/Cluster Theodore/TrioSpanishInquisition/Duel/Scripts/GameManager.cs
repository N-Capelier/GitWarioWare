using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace SpanishInquisition
{

    namespace Duel
    {

        /// <summary>
        /// Adel Ahmed-Yahia
        /// </summary>

        public class GameManager : TimedBehaviour
        {
            private static GameManager _instance;
            public static GameManager instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<GameManager>();
                    }
                    return _instance;
                }
            }

            //public GameObject[] scoreDisplays;
            public GameObject parryButton1;
            public GameObject parryButtonTransparent1;
            public GameObject parryButton2;
            public GameObject parryButtonTransparent2;
            public GameObject parryButton3;
            public GameObject parryButtonTransparent3;
            public GameObject victoryFeedback;
            public GameObject defeatFeedback;
            public GameObject parentButton;
            public bool[] showButtons; 
           
            public Animator playerAnimator;
            public Animator enemyAnimator;

            public ParticleSystem victoryParticle;
            public ParticleSystem bloodParticle;
            public ParticleSystem parryParticle;

            private int numberOfParries;
            public int parriesNeeded = 2;
            public int currentParryButton;
            public int neutralTime;
            public int tickBeforeNextParry;
            public float tickTimer;
            public float cooldownTime;
            public bool gameIsWon;
            public bool gameIsFinished;
            public bool endFeedbackPlayed;
            public float speed;
            public float cooldown;


            private SoundManager soundManager;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                speed = bpm / 5;
                soundManager = GetComponentInChildren<SoundManager>();
                soundManager.PlayBackgroundNoise();

                switch (bpm)
                {
                    case 60:
                        soundManager.PlayDuelMusicSlow();
                        break;

                    case 80:
                        soundManager.PlayDuelMusicMedium();
                        break;

                    case 100:
                        soundManager.PlayDuelMusicFast();
                        break;

                    case 120:
                        soundManager.PlayDuelMusicSuperFast();
                        break;
                }

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:

                        neutralTime = 2;
                        parriesNeeded = 2;
                        break;

                    case Difficulty.MEDIUM:

                        neutralTime = 1;
                        parriesNeeded = 3;

                        break;

                    case Difficulty.HARD:

                        neutralTime = 0;
                        parriesNeeded = 6;
                        break;
                }


                parentButton.SetActive(showButtons[(int)currentDifficulty]);
                tickBeforeNextParry = neutralTime;
                //DisplayScore();               
            }

            public void Update()
            {

                UpdateParry();

                if (numberOfParries >= parriesNeeded && !gameIsFinished)
                {
                    gameIsFinished = true;
                    gameIsWon = true;
                    if (!endFeedbackPlayed)
                    {
                        EndOfGameFeedback();
                        endFeedbackPlayed = true;
                    }                   
                }
            }

            //TimedUpdate is called once every tick.

            public override void TimedUpdate()
            {
                base.TimedUpdate();

                Debug.Log(Tick);

                if ((Tick < 8 && !gameIsWon) && !gameIsFinished)
                {
                    playerAnimator.SetBool("Parrying", false);
                    enemyAnimator.SetBool("isParried", false);
                    enemyAnimator.SetInteger("Direction", 0);

                    if (currentParryButton != 0)
                    {
                        Fail();
                        currentParryButton = 0;
                    }
                    else
                    {
                        if (tickBeforeNextParry > 0)
                        {
                            tickBeforeNextParry--;
                        }
                        else
                        {
                            ParryRandomizer();
                        }
                    }
                }

                if (Tick == 8)
                {
                    Manager.Instance.Result (gameIsWon);
                }

                /*if ((Tick == 8 && !gameIsWon) || gameIsFinished)
                {
                    
                }*/
            }

            private void UpdateParry()
            {
                switch (currentParryButton)
                {
                    case 1:
                        if (Input.GetButtonDown("X_Button"))
                        {
                            soundManager.PlayAttack();
                            Parry();
                        }

                        if (Input.GetButtonDown("Y_Button") || Input.GetButtonDown("B_Button"))
                        {
                            soundManager.PlayAttack();
                            Fail();
                        }
                        break;

                    case 2:
                        if (Input.GetButtonDown("Y_Button"))
                        {
                            soundManager.PlayAttack();
                            Parry();
                        }

                        if (Input.GetButtonDown("X_Button") || Input.GetButtonDown("B_Button"))
                        {
                            soundManager.PlayAttack();
                            Fail();

                        }
                        break;

                    case 3:
                        if (Input.GetButtonDown("B_Button"))
                        {
                            soundManager.PlayAttack();
                            Parry();
                        }

                        if (Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button"))
                        {
                            soundManager.PlayAttack();
                            Fail();
                        }
                        break;

                    default:
                        break;
                }

                if(currentParryButton >= 1 && currentParryButton <= 3 && Input.GetButtonDown("X_Button") || Input.GetButtonDown("Y_Button") || Input.GetButtonDown("B_Button"))
                {
                    parryButton1.SetActive(false);
                    parryButtonTransparent1.SetActive(true);
                    parryButton2.SetActive(false);
                    parryButtonTransparent2.SetActive(true);
                    parryButton3.SetActive(false);
                    parryButtonTransparent3.SetActive(true);
                    currentParryButton = 0;
                    tickBeforeNextParry = neutralTime;
                }
            }

            private void Parry()
            {
                Debug.Log("Parry !");
                soundManager.PlayParry();
                numberOfParries++;
                playerAnimator.SetBool("Parrying", true);
                enemyAnimator.SetBool("isParried", true);
                parryParticle.Play();
            }

            private void Fail()
            {
                Debug.Log("Fail !");
                gameIsFinished = true;
                EndOfGameFeedback();
            }

            private void ParryRandomizer()
            {
                currentParryButton = Random.Range(1, 4);
                enemyAnimator.SetInteger("Direction", currentParryButton);

                if (currentParryButton == 1)
                {
                    soundManager.PlayPrepareAttack();
                    parryButton1.SetActive(true);
                    parryButtonTransparent1.SetActive(false);
                }

                if (currentParryButton == 2)
                {
                    soundManager.PlayPrepareAttack();
                    parryButton2.SetActive(true);
                    parryButtonTransparent2.SetActive(false);
                }

                if (currentParryButton == 3)
                {
                    soundManager.PlayPrepareAttack();
                    parryButton3.SetActive(true);
                    parryButtonTransparent3.SetActive(false);
                }
            }


            public void EndOfGameFeedback()
            {
                parryButton1.SetActive(false);
                parryButtonTransparent1.SetActive(true);
                parryButton2.SetActive(false);
                parryButtonTransparent2.SetActive(true);
                parryButton3.SetActive(false);
                parryButtonTransparent3.SetActive(true);

                if (gameIsWon)
                {
                    //Win feedback
                    soundManager.PlayVictory();
                    victoryFeedback.SetActive(true);
                    enemyAnimator.SetBool("hasWon", true);
                    victoryParticle.Play();
                }
                else
                {
                    //Loss feedback
                    soundManager.PlayDefeat();
                    defeatFeedback.SetActive(true);
                    playerAnimator.SetBool("hasLost", true);
                    enemyAnimator.SetBool("isParried", true);
                    bloodParticle.Play();
                }
            }

            private IEnumerator StartCooldown()
            {
                yield return new WaitForSeconds(cooldownTime);
            }

            /*public bool HasWon()
            {
                bool allObjectsPassed = true;
                bool allObjectsOutOfZone = true;

                foreach (ObjectMovement objMovement in activeObjects)
                {
                    if (!objMovement.hasBeenInZone)
                    {
                        allObjectsPassed = false;
                    }
                }
                foreach (ObjectMovement objMovement in activeObjects)
                {
                    if (objMovement.InZone())
                    {
                        allObjectsOutOfZone = false;
                    }
                }
                Debug.Log(allObjectsOutOfZone);
                return allObjectsPassed && allObjectsOutOfZone && objectSpawned >= objectsNumber && fruitsRemaining == 0;
            }

            /*public void DisplayScore()
            {
                // From int
                foreach (GameObject display in scoreDisplays)
                {
                    display.SetActive(false);
                }

                scoreDisplays[fruitsRemaining].SetActive(true);
            }

            private IEnumerator StartCooldown()
            {
                canCut = false;
                yield return new WaitForSeconds(cooldownTime);
                canCut = true;
            }*/
        }      
    }
}