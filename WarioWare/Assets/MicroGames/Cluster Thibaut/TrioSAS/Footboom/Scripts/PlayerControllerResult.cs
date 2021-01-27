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
            public bool eazy;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                switch(currentDifficulty)
                {
                    case Difficulty.EASY:
                        difficulty.SizeEasy();
                        eazy = true;
                        break;
                    case Difficulty.MEDIUM:
                        difficulty.SizeMedium();
                        break;
                    case Difficulty.HARD:
                        difficulty.SizeHard();
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
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (Input.GetButton("A_Button") && endGame == false)
                {
                    gameObject.GetComponent<BarMovement>().barSpeed = 0;
                    if (winningCondition == true)
                    {
                        anim.ActivateAnimation();
                        explosion.SetBool("setActive", true);
                        kick.SetBool("kick", true);
                        youWinner = true;
                    }
                    else
                    {
                        kick.SetBool("kick", true);
                        failure.ActivateFail();
                        kick.SetBool("breakFail", true);
                        youWinner = false;

                    }
                    endGame = true;
                    buttonPress.SetBool("PossibleSuccess", false);
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
                if (eazy == true)
                {
                    buttonPress.SetBool("PossibleSuccess", true);
                }
                else
                {
                    buttonPress.SetBool("PossibleSuccess", false);
                }
                
            }

            void OnTriggerExit2D(Collider2D col)
            {
                winningCondition = false;
                buttonPress.SetBool("PossibleSuccess", false);
            }

        }
    }
}