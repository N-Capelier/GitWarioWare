using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Caps;

namespace LeRafiot
{
    namespace Choppe
    {
        /// <summary>
        /// Antoine LEROUX
        /// This script is use to display the timer at the screen
        /// </summary>

        public class DisplayUI : TimedBehaviour
        {/*
            #region Variables
            private float spawnCooldown;

            [Header("UI")]
            public GameObject panel;
            public TextMeshProUGUI resultText;
            public TextMeshProUGUI bpmText;
            public Slider timerUI;
            public TextMeshProUGUI tickNumber;

            [Header("Script")]
            public CatchSystem catchScript;
            #endregion

            public override void Start()
            {
                base.Start();
                bpmText.text = "bpm: " + bpm.ToString();
                spawnCooldown = 60 / bpm;
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
                if (Tick == 8 && !Manager.Instance.panel.activeSelf && !catchScript.catchedGoodDrink)               
                {
                    //Manager.Instance.Result(false);
                    //SoundManagerChoppe.Instance.sfxSound[1].Play();
                }

                if (Tick <= 8)
                {
                    tickNumber.text = Tick.ToString();
                }*/
            }       
        }
    }