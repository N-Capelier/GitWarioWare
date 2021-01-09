using System.Collections;
using System.Collections.Generic;
using Caps;
using UnityEditor;
using UnityEngine;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class MicroManager : TimedBehaviour
        {
            [Range(5, 25)]
            public int _easyGhostSpeed;
            [Range(5, 25)]
            public int _mediumGhostSpeed;
            [Range(5, 25)]
            public int _hardGhostSpeed;

            [Range(1, 8)]
            public int _easyBlackScreenDelay;
            [Range(1, 8)]
            public int _mediumBlackScreenDelay;
            [Range(1, 8)]
            public int _hardBlackScreenDelay;

            public int blackScreenDelay { get; set; }
            public float maxGameTime { get; set; }
            private bool hasInput = false;
            private GameObject ghost { get; set; }
            private GameObject area { get; set; }
            private GhostMovement ghostMovement { get; set; }
            private GameObject blackScreen { get; set; }

            private SoundManager soundManager;


            public override void Start()
            {
                // DO NOT REMOVE -----
                base.Start();
                // -------------------

                ghost = GameObject.Find("Ghost");
                area = GameObject.Find("Middle Collider");
                ghostMovement = ghost.GetComponent<GhostMovement>();
                soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
                blackScreen = GameObject.Find("Black Screen");

                //soundManager.PlayMusic();
                area.SetActive(false);
                blackScreen.SetActive(false);

                switch(currentDifficulty)
                {
                    case Difficulty.EASY:
                        ghostMovement.speed = _easyGhostSpeed;
                        blackScreenDelay = _easyBlackScreenDelay;
                        break;

                    case Difficulty.MEDIUM:
                        ghostMovement.speed = _mediumGhostSpeed;
                        blackScreenDelay = _mediumBlackScreenDelay;
                        break;

                    case Difficulty.HARD:
                        ghostMovement.speed = _hardGhostSpeed;
                        blackScreenDelay = _hardBlackScreenDelay;
                        break;
                }
            }

            public override void FixedUpdate()
            {
                // DO NOT REMOVE -----
                base.FixedUpdate();
                // -------------------
            }

            private void Update()
            {
                if (Input.GetButtonDown("A_Button") && Tick >= blackScreenDelay)
                {
                    OnAButton();
                }
            }

            public override void TimedUpdate()
            {
                // DO NOT REMOVE -----
                base.TimedUpdate();
                // -------------------

                if(Tick == 1)
                {
                    ghost.SetActive(true);
                }
                else if(Tick == blackScreenDelay)
                {
                    blackScreen.SetActive(true);
                    area.SetActive(true);
                }
                else if(Tick == 8)
                {
                    Manager.Instance.Result(ghostMovement.inArea && hasInput);
                    if(ghostMovement.inArea)
                    {
                        soundManager.StopMusic();
                        soundManager.PlayVictory();
                    }
                    else
                    {
                        soundManager.StopMusic();
                        soundManager.PlayDefeat();
                    }
                    blackScreen.SetActive(false);
                }
            }

            private void OnAButton()
            {
                ghost.GetComponent<GhostMovement>().canMove = false;
                hasInput = true;
                blackScreen.SetActive(false);
            }
        }
    }
}
