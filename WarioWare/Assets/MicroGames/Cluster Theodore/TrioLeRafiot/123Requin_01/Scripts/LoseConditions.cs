﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Caps;

namespace LeRafiot
{
    namespace UnDeuxTroisRequin
    {
        /// <summary>
        /// Antoine LEROUX
        /// This script is use to tell the end game conditions
        /// </summary>
        
        public class LoseConditions : TimedBehaviour
        {
            #region Variables
            private float spawnCooldown;

            [Header("UI")]
            public GameObject panel;
            public TextMeshProUGUI resultText;
            public TextMeshProUGUI bpmText;
            public Slider timerUI;
            public TextMeshProUGUI tickNumber;
            public Animator buttonAnimator;

            private bool playerCatchByShark;

            [Header("Script")]
            public RopeController robeControllerScript;
            public Animator loseAnimator;
            public SharkManager sharkManager;
            public SoundManager123Requin sound;
            #endregion

            public override void Start()
            {
                base.Start(); 
                bpmText.text = "bpm: " + bpm.ToString();
                spawnCooldown = 60 / bpm;

                buttonAnimator.gameObject.SetActive(true);
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); 
                timerUI.value = (float)timer / spawnCooldown;
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8 && !robeControllerScript.win && !Manager.Instance.panel.activeSelf && !playerCatchByShark)                //Lose if at the end of the game, the player don't pull the chest to the boat 
                {
                    Manager.Instance.Result(false);
                    sound.sfxSound[1].Play();
                    buttonAnimator.gameObject.SetActive(false);
                }
                if (Tick == 8 && !robeControllerScript.win && playerCatchByShark && !Manager.Instance.panel.activeSelf)
                {
                    Manager.Instance.Result(false);
                }

                if (Tick <= 8)
                {
                    tickNumber.text = Tick.ToString();
                }

                if (Tick < 8 && !sharkManager.sharkIsHere)
                {
                    buttonAnimator.SetTrigger("Press");
                }
            }

            private void Update()
            {              
                if (!Manager.Instance.panel.activeSelf)                             //Lose if the player pulling up the chest when a shark is here
                {
                    if (sharkManager.sharkIsHere && !playerCatchByShark)              
                    {
                        buttonAnimator.SetBool("DontPress", true);

                        if (Input.GetButtonDown("A_Button") || Input.GetKeyDown(KeyCode.Space))
                        {
                            playerCatchByShark = true;
                            loseAnimator.SetBool("Lose", true);
                            //Manager.Instance.Result(false);
                            sound.sfxSound[1].Play();
                            robeControllerScript.controllerDisabled = true;
                            buttonAnimator.gameObject.SetActive(false);                        
                        }
                    }
                    else
                    {
                        buttonAnimator.SetBool("DontPress", false);
                    }
                }
            }
        }
    }
}