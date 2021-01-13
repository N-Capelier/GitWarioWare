using Caps;
using UnityEngine;
using UnityEngine.UI;

namespace ACommeAkuma
{
    namespace PirateGrog
    {
        /// <summary>
        /// Bastien PRIGENT
        /// </summary>
        public class PirateGrogController : TimedBehaviour
        {
            #region variables
            [Header("Grog Var")]
            [SerializeField]
            private Image grog;
            [SerializeField]
            [Range(0, 100)]
            private float grogAmount;
            [SerializeField]
            private float overFillAmount;
            [SerializeField]
            private float maxGrogAmount;
            [SerializeField]
            private float minGrogAmount;
            private bool overFill = false;

            [Header("Speed Var")]
            private int fillSpeed;
            [SerializeField]
            private int baseFillSpeed;
            [SerializeField]
            private bool canFill = true;

            [Header("Tool for Different Cup")]
            [SerializeField]
            private AnimationCurve fillGrog;
            //[SerializeField]
            //private float[] whenToChangeSpeed;
            //[SerializeField]
            //private int[] changeFillSpeed;

            private float fillTimer;
            private float yJoystickRight;
            [SerializeField]
            private bool inputDetected = false;
            [SerializeField]
            private bool gameplayIsOver = false;

            bool liquidSFX;
            bool beerTapSFX;

            #endregion

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                PlayMusic();
            }

            public void Update()
            {
                PlaySoundSFX();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                FillGrog();
                stopFillGrog();
            }


            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                WinLose();
            }

            void FillGrog()
            {
                yJoystickRight = -Input.GetAxisRaw("Left_Joystick_Y");

                if (gameplayIsOver == true)
                {
                    yJoystickRight = 0f;    // Reset Controller if player already used input
                }

                if (yJoystickRight > 0 && canFill == true)
                {
                    grogAmount += Time.fixedDeltaTime * fillSpeed * yJoystickRight; //Controller
                    fillTimer += Time.fixedDeltaTime;
                }
                else if (inputDetected == true)
                {
                    fillTimer -= Time.fixedDeltaTime;
                    fillTimer = Mathf.Min(fillTimer, 1);
                    grogAmount += Mathf.Max(0f, fillSpeed * fillTimer * Time.fixedDeltaTime);
                    if (fillTimer < 0)
                    {
                        fillTimer = 0;
                        inputDetected = false;
                    }
                }

                if (grogAmount > overFillAmount)
                {
                    overFill = true;
                    canFill = false;
                }

                grog.fillAmount = grogAmount / overFillAmount;
                fillSpeed = (int)(fillGrog.Evaluate(grogAmount / overFillAmount) * baseFillSpeed);
                //Debug.Log("fillSpeed = " + fillSpeed);


                //for (int i = 0; i < whenToChangeSpeed.Length; i++)
                //{
                //    if (grogAmount >= whenToChangeSpeed[i])
                //    {
                //        fillSpeed = changeFillSpeed[i];
                //    }
                //}
            }

            /// <summary>
            /// regarde si l'input à été utilisé
            /// </summary>
            void stopFillGrog()
            {
                if (Input.GetAxisRaw("Left_Joystick_Y") < -0.1f)
                {
                    inputDetected = true;
                }
                //if (Input.GetAxisRaw("Right_Joystick_Y") > -0.01f && inputDetected == true)
                //{
                //    gameplayIsOver = true;
                //}
            }
            void WinLose()
            {
                if (overFill == true)
                {
                    Manager.Instance.Result(false);
                    Debug.Log("Lose");
                }

                if (Tick == 8)
                {
                    canFill = false;
                    Debug.Log(Tick);
                    if (grogAmount >= minGrogAmount && grogAmount <= maxGrogAmount)     //Make the player win or lose at Tick 8
                    {
                        Manager.Instance.Result(true);
                        Debug.Log("Win");
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                        Debug.Log("Lose");
                    }
                }
            }

            private void PlaySoundSFX()
            {
                if (inputDetected)
                {
                    liquidSFX = true;
                    beerTapSFX = true;
                }
                else if (!inputDetected)
                {
                    liquidSFX = false;
                    beerTapSFX = false;
                }

                if (liquidSFX == true && PGAudioSource.Instance.sourceList[0].isPlaying == false)
                {
                    PGAudioSource.Instance.PlayBeerLiquid();
                }
                else if (liquidSFX == false)
                {
                    PGAudioSource.Instance.StopBeerLiquid();
                }

                if (beerTapSFX == true && PGAudioSource.Instance.sourceList[1].isPlaying == false)
                {
                    PGAudioSource.Instance.PlayBeerTapSound();
                }
                else if (beerTapSFX == false)
                {
                    PGAudioSource.Instance.StopBeerTapSound();
                }
            }

            private void PlayMusic()
            {
                switch(Manager.Instance.bpm)
                {
                    case BPM.Slow:
                        PGAudioSource.Instance.PlayMusicSlow();
                        break;

                    case BPM.Medium:
                        PGAudioSource.Instance.PlayMusicMedium();
                        break;

                    case BPM.Fast:
                        PGAudioSource.Instance.PlayMusicFast();
                        break;

                    case BPM.SuperFast:
                        PGAudioSource.Instance.PlayMusicSuperFast();
                        break;
                }
            }


        }
    }
}