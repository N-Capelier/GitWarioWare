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

            [Header("Win condition")]
            public int shootNecessaryEasy;
            public int shootNecessaryMedium;
            public int shootNecessaryHard;
            private int ShootCounter;

            private bool cantPress;
            private bool noInput;

            /*[Header("Life")]
            public GameObject heart1;
            public GameObject heart2;
            public Sprite heartFull;
            public Sprite heartEmpty;*/

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

                /*heart1.GetComponent<SpriteRenderer>().sprite = heartFull;
                heart2.GetComponent<SpriteRenderer>().sprite = heartFull;

                if (eazy)
                {
                    heart1.SetActive(false);
                    heart2.SetActive(true);
                }
                else if (medium)
                {
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                }
                else if (hard)
                {
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                }*/
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
                            //heart2.GetComponent<Animation>().Play();
                            //heart2.GetComponent<SpriteRenderer>().sprite = heartEmpty;

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
                                //heart2.GetComponent<Animation>().Play();
                                //heart2.GetComponent<SpriteRenderer>().sprite = heartEmpty;
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
                            //heart1.GetComponent<Animation>().Play();
                            //heart1.GetComponent<SpriteRenderer>().sprite = heartEmpty;
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
                                //heart2.GetComponent<Animation>().Play();
                                //heart2.GetComponent<SpriteRenderer>().sprite = heartEmpty;
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
                            //heart1.GetComponent<Animation>().Play();
                            //heart1.GetComponent<SpriteRenderer>().sprite = heartEmpty;
                            explosion.SetBool("lauch", true);
                            buttonPress.SetBool("PossibleSuccess", false);

                            youWinner = true;
                            endGame = true;

                            gameObject.GetComponent<BarMovement>().barSpeed = 0;
                        }
                    }
                }
                
                

                if (Tick == 8 && youWinner == false)
                {
                    endGame = true;
                    buttonPress.SetBool("PossibleSuccess", false);
                    gameObject.GetComponent<BarMovement>().barSpeed = 0;
                    Manager.Instance.Result(false);

                }
                else if (Tick == 8 && youWinner == true)
                {
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