using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioSAS
{
    namespace Footboom
    {
        /// <summary>
        /// Steve Guitton
        /// </summary>
        
        public class PlayerControllerResult : TimedBehaviour
        {
            public AnimateCannonBall anim;
            public bool winningCondition;
            public SuccessZoneSize difficulty;
            public BarMovement speedBpm;
            private bool endGame;
            public FailAnimate failure;
            public Animator explosion;
            public Animator kick;
            public GetMusic music;
            public bool youWinner;
            public Animator buttonPress;

            private bool eazy;
            private bool medium;
            private bool hard;
            private bool alreadyEnded = false;

            [Header("Win condition")]
            public int shootNecessaryEasy;
            public int shootNecessaryMedium;
            public int shootNecessaryHard;
            private int ShootCounter;

            private bool cantPress;
            private bool noInput;

            [Header("CannonBall")]
            public GameObject cannon1;
            public GameObject cannon2;
            public Sprite cannonFull;
            public Sprite cannonMiss;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                ShootCounter = 0;

                switch(currentDifficulty)
                {
                    case Difficulty.EASY:
                        difficulty.SizeEasy();
                        eazy = true;
                        break;
                    case Difficulty.MEDIUM:
                        difficulty.SizeMedium();
                        medium = true;
                        break;
                    case Difficulty.HARD:
                        difficulty.SizeHard();
                        hard = true;
                        break;
                }

                switch(bpm)
                {
                    case (float)BPM.Slow:
                        music.Maestro60();
                        speedBpm.speedSlow();
                        break;
                    case (float)BPM.Medium:
                        music.Maestro80();
                        speedBpm.speedMedium();
                        break;
                    case (float)BPM.Fast:
                        music.Maestro100();
                        speedBpm.speedFast();
                        break;
                    case (float)BPM.SuperFast:
                        music.Maestro120();
                        speedBpm.speedVeryFast();
                        break;
                }

                cannon1.GetComponent<SpriteRenderer>().sprite = cannonFull;
                cannon2.GetComponent<SpriteRenderer>().sprite = cannonFull;

                if (eazy)
                {
                    cannon1.SetActive(true);
                    cannon2.SetActive(false);
                }
                else if (medium)
                {
                    cannon1.SetActive(true);
                    cannon2.SetActive(true);
                }
                else if (hard)
                {
                    cannon1.SetActive(true);
                    cannon2.SetActive(true);
                }
            }

            private void Update()
            {
                if (Input.GetButtonUp("A_Button"))
                {
                    noInput = false;
                }
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (Input.GetButton("A_Button") && endGame == false && !noInput)
                {
                    noInput = true;

                    if (eazy)
                    {
                        if (winningCondition == true && !cantPress)
                        {
                            cantPress = true;

                            ShootCounter++;
                            
                            cannon1.GetComponent<SpriteRenderer>().sprite = cannonMiss;

                            if (ShootCounter == shootNecessaryEasy)
                            {
                                anim.ActivateWin();
                            }
                            else
                            {
                                anim.ActivateAnimation();
                            }

                            kick.SetTrigger("kick");
                        }
                        else if (winningCondition == false)
                        {
                            failure.ActivateFail();
                            kick.SetBool("breakFail", true);

                            youWinner = false;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }

                        if (ShootCounter == shootNecessaryEasy)
                        {
                            explosion.SetBool("lauch", true);
                            buttonPress.SetBool("PossibleSuccess", false);

                            youWinner = true;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }
                    }

                    else if (medium)
                    {
                        if (winningCondition == true && !cantPress)
                        {
                            cantPress = true;

                            ShootCounter++;                           

                            if (ShootCounter == shootNecessaryMedium)
                            {
                                anim.ActivateWin();
                            }
                            else
                            {
                                anim.ActivateAnimation();

                                cannon1.GetComponent<SpriteRenderer>().sprite = cannonMiss;

                                explosion.SetTrigger("explosion");
                            }

                            kick.SetTrigger("kick");
                        }
                        else if (winningCondition == false)
                        {
                            failure.ActivateFail();
                            kick.SetBool("breakFail", true);

                            youWinner = false;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }

                        if (ShootCounter == shootNecessaryMedium)
                        {

                            cannon2.GetComponent<SpriteRenderer>().sprite = cannonMiss;

                            explosion.SetBool("lauch", true);
                            buttonPress.SetBool("PossibleSuccess", false);

                            youWinner = true;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }
                    }

                    else if (hard)
                    {
                        if (winningCondition == true && !cantPress)
                        {
                            cantPress = true;

                            ShootCounter++;                          

                            if (ShootCounter == shootNecessaryHard)
                            {
                                anim.ActivateWin();
                            }
                            else
                            {
                                anim.ActivateAnimation();

                                cannon1.GetComponent<SpriteRenderer>().sprite = cannonMiss;

                                explosion.SetTrigger("explosion");
                            }

                            kick.SetTrigger("kick");
                        }
                        else if (winningCondition == false)
                        {
                            failure.ActivateFail();
                            kick.SetBool("breakFail", true);

                            youWinner = false;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }

                        if (ShootCounter == shootNecessaryHard)
                        {

                            cannon2.GetComponent<SpriteRenderer>().sprite = cannonMiss;

                            explosion.SetBool("lauch", true);
                            buttonPress.SetBool("PossibleSuccess", false);

                            youWinner = true;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }
                    }
                }
                
                

                if (Tick == 8 && youWinner == false && !alreadyEnded)
                {
                    alreadyEnded = true;
                    endGame = true;
                    buttonPress.SetBool("PossibleSuccess", false);
                    gameObject.GetComponent<BarMovement>().barSpeed = 0;
                    Manager.Instance.Result(false);

                }
                else if (Tick == 8 && youWinner == true && !alreadyEnded)
                {
                    alreadyEnded = true;
                    endGame = true;
                    buttonPress.SetBool("PossibleSuccess", false);
                    Manager.Instance.Result(true);
                }

            }

            void OnTriggerEnter2D(Collider2D col)
            {
                winningCondition = true;
                /*if (eazy == true)
                {
                    buttonPress.SetBool("PossibleSuccess", true);
                }
                else
                {
                    buttonPress.SetBool("PossibleSuccess", false);
                }*/

                buttonPress.SetBool("PossibleSuccess", true);

            }

            void OnTriggerExit2D(Collider2D col)
            {
                winningCondition = false;
                buttonPress.SetBool("PossibleSuccess", false);

                cantPress = false;
            }

        }
    }
}