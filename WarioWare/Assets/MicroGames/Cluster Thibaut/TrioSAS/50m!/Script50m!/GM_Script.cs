using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using TrioSAS.Cinquante;
using TrioName.MiniGameName;

namespace SAS
{
    namespace Cinquante
    {
        public class GM_Script : TimedBehaviour
        {
            private bool p1;
            private bool p2;
            private bool win;
            private bool lose;
            public float stepDistance = 1.5f;
            private float startPositionZ;
            private int stepCount = 1;
            private bool currentStepRight; 
            
            public GameObject YouLose;
            public GameObject YouWin;

            public GameObject piedG;
            public GameObject piedD;

            public Controller controllerScript;
            public AudioManagerScript Audio;
            public AnimationScript InputAnimation;
            public CameraShakeScript Shake;

            public override void Start()
            {
                base.Start(); //Do not erase this line!


                p1 = false;
                p2 = false;
                win = false;
                lose = false;

                InputAnimation.StartScene();
                Audio.StartScene();
            }

            //FixedUpdate is called on a fixed time.
            public void Update()
            {

                //A = Gachette gauche
                if (Input.GetAxis("Left_Trigger") == 1f && p1 == false)
                {
                    controllerScript.PlayerInput();
                    p1 = true;
                    p2 = false;
                    piedG.SetActive(true);
                    piedD.SetActive(false);
                    Audio.Running();
                    //Debug.Log("Pas gauche");
                }

                //Z = Gachette droite
                if (Input.GetAxis("Right_Trigger") == 1f && p2 == false)
                {
                    controllerScript.PlayerInput();
                    p2 = true;
                    p1 = false;
                    piedG.SetActive(false);
                    piedD.SetActive(true);
                    
                    Audio.Running();
                    //Debug.Log("Pas droit");
                }

                if (Input.GetAxis("Left_Trigger")== 1f && Input.GetAxis("Right_Trigger") == 1f)
                {
                    controllerScript.PlayerStop();
                    Audio.StopRunning();
                    StartCoroutine(Shake.Shake(0.1f, 0.05f));
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
                    //Debug.Log("OnTriggerEnter Tower");
                    win = true;
                    //YouWin.SetActive(true);
                    Manager.Instance.Result(true);
                    controllerScript.PlayerEnd();
                }
            }

            //TimedUpdate is called once every tick.

            //Lose Condition
            public override void TimedUpdate()
            {
                base.TimedUpdate();

                if (Tick == 8 && win == false)
                {
                    lose = true;
                    Audio.EndScene();
                    Manager.Instance.Result(false);
                    controllerScript.PlayerStop();
                }
            }
            #endregion 

           
        }
    }
}