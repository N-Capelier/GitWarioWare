using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using UnityEditor.Build.Reporting;
using TrioSAS.Cinquante;
using UnityEditor;

namespace SAS
{
    namespace Cinquante
    {
        public class GM_Script : TimedBehaviour
        {
            private bool lt;
            private bool rt;
            private bool win;
            private bool lose;
            private bool twoTrigger;
            private bool coroutinePlay;

            public float stepDistance = 1.5f;

            public GameObject piedG;
            public GameObject piedD;

            public Controller controllerScript;
            public AudioManagerScript Audio;
            public HeadBobbing Moves;

            [Header("Input")]
            public GameObject inputLeft;
            public GameObject inputRight;
            public Sprite triggerLeft;
            public Sprite triggerRight;
            public Sprite triggerLeftActive;
            public Sprite triggerRightActive;

            [Header("Win COndition")]
            public GameObject winGraph;
            public GameObject loseGraph;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                loseGraph.SetActive(false);
                winGraph.SetActive(false);

                lt = false;
                rt = false;
                win = false;
                lose = false;
                twoTrigger = false;


                inputLeft.SetActive(false);
                inputRight.SetActive(true);
                inputLeft.GetComponent<SpriteRenderer>().sprite = triggerLeft;
                inputRight.GetComponent<SpriteRenderer>().sprite = triggerRight;

                GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);

                Audio.StartScene();
            }

            //FixedUpdate is called on a fixed time.
            public void Update()
            {
                if (Input.GetAxis("Left_Trigger") == 1f && Input.GetAxis("Right_Trigger") == 1f && win == false && lose == false)
                {
                    //inputLeft.SetActive(true);
                    //inputRight.SetActive(true);
                    inputLeft.GetComponent<SpriteRenderer>().sprite = triggerLeft;
                    inputRight.GetComponent<SpriteRenderer>().sprite = triggerRight;
              
                    twoTrigger = true;

                    controllerScript.PlayerStop();

                    //Audio.StopRunning();

                    if (!coroutinePlay)
                    {
                        coroutinePlay = true;

                    }
                }
                else
                {
                    coroutinePlay = false;
                    twoTrigger = false;
                }

                //A = Gachette gauche
                if (Input.GetAxis("Left_Trigger") == 1f && lt == false && twoTrigger == false)
                {
                    inputLeft.SetActive(false);
                    inputRight.SetActive(true);
                    inputLeft.GetComponent<SpriteRenderer>().sprite = triggerLeftActive;
                    inputRight.GetComponent<SpriteRenderer>().sprite = triggerRight;

                    controllerScript.PlayerInput();
                    lt = true;
                    rt = false;
                    piedG.SetActive(true);
                    piedD.SetActive(false);
                    Moves.bobOn();
                    Audio.Running();
                }

                //Z = Gachette droite
                if (Input.GetAxis("Right_Trigger") == 1f && rt == false&& twoTrigger == false)
                {
                    inputLeft.SetActive(true);
                    inputRight.SetActive(false);
                    inputLeft.GetComponent<SpriteRenderer>().sprite = triggerLeft;
                    inputRight.GetComponent<SpriteRenderer>().sprite = triggerRightActive;

                    controllerScript.PlayerInput();
                    rt = true;
                    lt = false;
                    piedG.SetActive(false);
                    piedD.SetActive(true);
                    Moves.bobOn();
                    
                    Audio.Running();
                }

                FootManagement();
            }

            private void FootManagement()
            {
                
            }

            #region Winning Condition
            //Win Condition
            private void OnTriggerEnter(Collider col)
            {
                if (col.gameObject.name == "Arrival" && lose == false)
                {
                    win = true;
                    winGraph.SetActive(true);
                    controllerScript.PlayerEnd();
                    lt = true;
                    rt = true;
                }
            }

            //TimedUpdate is called once every tick.

            //Lose and End Win Condition
            public override void TimedUpdate()
            {
                base.TimedUpdate();

                if (Tick == 8 && win == true)
                {
                    Manager.Instance.Result(true);
                }

                if (Tick == 8 && win == false)
                {
                    lose = true;
                    loseGraph.SetActive(true);
                    Audio.EndScene();
                    Manager.Instance.Result(false);
                    controllerScript.PlayerEnd();
                    lt = true;
                    rt = true;
                }
            }
            #endregion 

           
        }
    }
}