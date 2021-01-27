
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Fleebos
{
    namespace BananaStrip
    {
        public class GameManagerBanana : TimedBehaviour
        {
            public static GameManagerBanana gm;

            float timeCounter;
            public float speed, height, width, triggerValue;
            bool pinched;
            bool isHard = false;
            

            [Header("Hand")]
            public GameObject hand;
            public Rigidbody2D handRb;
            public float rayCastDist;
            public Transform pinchPoint;



            [Header("BananaPeel")]
            public List<GameObject> peels = new List<GameObject>();
            GameObject peel;
            public GameObject box;
            public LineRenderer line;
            public Transform linePoint, linePoint2, resetPoint, resetPoint2;
            public Animator bananaAnimator;
            int peelCounter = 0;
            bool peeled = false;
            public GameObject peelP1, peelP2;

            [Header("VFX")]
            public ParticleSystem arrowUp;
            public ParticleSystem arrowDown;

            [Header("Win Visuals")]
            public float outSpeed;
            public Animator monkey1, monkey2, banana;
            public ParticleSystem star, bananaLauncher1, bananaLauncher2, monkeyDown1, monkeyDown2, monkeySide1, monkeySide2;

            private void Awake()
            {
                gm = this;
            }
            public override void Start()
            {
                base.Start(); //Do not erase this line!



                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        peel = peels[0];
                        break;
                    case Difficulty.MEDIUM:
                        peel = peels[1];
                        peelP1 = peel.transform.GetChild(2).gameObject;
                        peelP2 = peel.transform.GetChild(3).gameObject;
                        break;
                    case Difficulty.HARD:
                        peel = peels[2];
                        peelP1 = peel.transform.GetChild(3).gameObject;
                        peelP2 = peel.transform.GetChild(4).gameObject;
                        peelP1.SetActive(false);
                        peelP2.SetActive(false);
                        isHard = true;
                        break;
                }
                peel.SetActive(true);
                bananaAnimator = peel.GetComponent<Animator>();
                linePoint = peel.transform.GetChild(0);
                linePoint2 = peel.transform.GetChild(1);
                lineStartSetup();

                switch (bpm)
                {
                    case 60:
                        SoundManager.sd.PlaySound(1);
                        speed *= 1;
                        break;
                    case 90:
                        SoundManager.sd.PlaySound(4);
                        speed *= 1.5f;
                        break;
                    case 120:
                        SoundManager.sd.PlaySound(5);
                        speed *= 2f;
                        break;
                    case 140:
                        SoundManager.sd.PlaySound(6);
                        speed *= 2.4f;
                        break;
                }
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                //Check if the Hand is pinching the banana
                if (peeled == false)
                {
                    MouvementAndRotation();
                }
                else if (peeled == true)
                {
                    WinAnimation();
                }

                if (pinched == false)
                {
                    Checking();
                }
                if(pinched == true)
                {
                    Line();
                    MoveCollider();
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8 && peeled == false)
                {
                    Manager.Instance.Result(false);
                }
                else if (Tick == 8 && peeled == true)
                {
                    Manager.Instance.Result(true);
                }
            }

            public void MouvementAndRotation()
            {
                #region Movement
                //Set Trigger value according to input value
                if (Input.GetAxis("Right_Trigger") >= 0.5f && (pinched == false || peelCounter < 2 || isHard == true))
                {
                    triggerValue = -1f;
                }
                else if (Input.GetAxis("Left_Trigger") >= 0.5f && (pinched == false || peelCounter >= 2 || isHard == true))
                {
                    triggerValue = 1f;
                }
                else
                {
                    triggerValue = 0;
                }

                //Set Time counter according to triggerValue
                if (triggerValue == 1)
                {
                    timeCounter += Time.deltaTime * speed;
                }
                else if (triggerValue == -1)
                {
                    timeCounter -= Time.deltaTime * speed;
                }

                //Move the Hand
                float x = Mathf.Cos(timeCounter) * width;
                float y = Mathf.Sin(timeCounter) * height;

                hand.transform.position = new Vector2(x, y);
                #endregion


                #region Rotation
                Vector2 lookDir = transform.position - hand.transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                handRb.rotation = angle;
                #endregion
            }

            public void Checking()
            {
                RaycastHit2D hit = Physics2D.Raycast(hand.transform.position, hand.transform.up, rayCastDist);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy1"))
                    {
                        pinched = true;
                        SoundManager.sd.PlaySound(3);
                        hand.GetComponent<Animator>().SetTrigger("Transition");
                        box = hit.collider.gameObject;
                        if (isHard == true)
                        {
                            bananaAnimator.SetTrigger("Peel");
                            arrowUp.Play();
                            peelP1.SetActive(true);
                        }
                    }
                }
            }

            public void MoveCollider()
            {
                box.transform.position = pinchPoint.transform.position;
            }

            public void lineStartSetup()
            {
                line.SetPosition(0, linePoint2.transform.position);
                line.SetPosition(1, linePoint.transform.position);
            }
            public void Line()
            {
                line.SetPosition(0, pinchPoint.transform.position);
                line.SetPosition(1, linePoint.transform.position);
            }
            public void RestartLine()
            {
                linePoint.transform.position = resetPoint.transform.position;
                linePoint2.transform.position = resetPoint2.transform.position;
                line.SetPosition(0, linePoint2.transform.position);
                line.SetPosition(1, linePoint.transform.position);
            }

            public void PeelEASY()
            {
                peelCounter += 1;
                bananaAnimator.SetTrigger("Peel");
                SoundManager.sd.PlaySound(2);
                if (peelCounter == 2)
                {
                    peeled = true;
                    monkey1.SetTrigger("Transition");
                    monkey2.SetTrigger("Transition");
                    banana.SetTrigger("Transition");
                }
                else if (peelCounter == 4)
                {
                    peeled = true;
                    monkey1.SetTrigger("Transition");
                    monkey2.SetTrigger("Transition");
                    banana.SetTrigger("Transition");
                }
            }
            public void PeelMEDIUM()
            {
                peelCounter += 1;
                bananaAnimator.SetTrigger("Peel");
                SoundManager.sd.PlaySound(2);
                if (peelCounter == 2)
                {
                    pinched = false;
                    hand.GetComponent<Animator>().SetTrigger("Transition");
                    RestartLine();
                    peelP1.SetActive(false);
                    peelP2.SetActive(true);
                    box = null;
                }
                else if (peelCounter >= 4)
                {
                    peeled = true;
                    monkey1.SetTrigger("Transition");
                    monkey2.SetTrigger("Transition");
                    banana.SetTrigger("Transition");
                }
            }
            public void PeelHARD()
            {
                peelCounter += 1;
                switch (peelCounter)
                {
                    case 1:
                        SoundManager.sd.PlaySound(2);
                        arrowDown.Play();
                        peelP1.SetActive(false);
                        peelP2.SetActive(true);
                        break;
                    case 2:
                        SoundManager.sd.PlaySound(3);
                        arrowUp.Play();
                        peelP1.SetActive(true);
                        peelP2.SetActive(false);
                        break;
                    case 3:
                        SoundManager.sd.PlaySound(2);
                        arrowDown.Play();
                        peelP1.SetActive(false);
                        peelP2.SetActive(true);
                        break;
                    case 4:
                        SoundManager.sd.PlaySound(3);
                        peeled = true;
                        monkey1.SetTrigger("Transition");
                        monkey2.SetTrigger("Transition");
                        banana.SetTrigger("Transition");
                        break;
                }
            }

            public void WinAnimation()
            {
                //Banana Peel goes down
                peel.transform.position += Vector3.up * Time.deltaTime * outSpeed;
                //Monkey Hand goes right
                hand.transform.position += Vector3.right * Time.deltaTime * outSpeed;
                //LineRenderer set active false
                if (line.gameObject.activeSelf == true)
                {
                    line.gameObject.SetActive(false);
                }
                //Particule Effect
                if (star.isPlaying == false)
                {
                    star.Play(true);
                    bananaLauncher1.Play(true);
                    bananaLauncher2.Play(true);
                    switch (currentDifficulty)
                    {
                        case Difficulty.EASY:
                            break;
                        case Difficulty.MEDIUM:
                            monkeyDown1.Play();
                            monkeyDown2.Play();
                            break;
                        case Difficulty.HARD:
                            monkeyDown1.Play();
                            monkeyDown2.Play();
                            monkeySide1.Play();
                            monkeySide2.Play();
                            break;
                    }
                }


            }
        }
    }
}