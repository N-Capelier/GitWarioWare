﻿using System.Collections.Generic;
using UnityEngine;


namespace TrapioWare
{
    namespace Climb
    {
        public class ClimbGameManager : Singleton<ClimbGameManager>
        {
            [Header("Tableau de Positions")]
            public GameObject ladderParent;
            public List<GameObject> positions = new List<GameObject>();
            public bool needToCheck = false;

            [Header("Variables")]
            public int numberOfLadder = 0;
            public int numberOfLadderNeeded = 0;

            [Header("PlayerReference")]
            public GameObject player;
            public GameObject playerSkin;
            public int playerPosition = 0;

            [Header("InputBools")]
            /*[SerializeField]*/
            private bool canLeft = false;
            /*[SerializeField]*/
            private bool canRight = true;
            public bool finishInstantiate = false;
            public bool win = false;
            public bool lose = false;

            public bool needToStop = false;

            [Header("Sounds")]
            [SerializeField] private GameObject audioSource = null;
            [SerializeField] private AudioClip[] inputSounds = null;
            [SerializeField] private int nextSound = 0;

            [SerializeField] public AudioSource deadSound;

            [Header("Parrot")]
            [SerializeField] private GameObject parrot = null;
            public bool animationDone = false;
            public bool loseDone = false;

            public bool setDifficulty = false;
            public int myDifficulty;
            public float mySpeed = 0;

            private void Awake()
            {
                CreateSingleton();
            }

            private void Update()
            {
                if (lose)
                {
                    playerSkin.GetComponent<Animator>().SetTrigger("Lose");
                    gameObject.GetComponent<AudioSource>().Play();
                }
                

                if (needToStop || win)
                {
                    needToStop = true;
                    canLeft = false;
                    canRight = false;
                }

                if (playerPosition == positions.Count && finishInstantiate && !win)
                {
                    win = true;
                    canLeft = false;
                    canRight = false;
                    parrot.GetComponent<AudioSource>().Play();
                    parrot.GetComponent<Animator>().SetTrigger("Victory");
                

                }

                if (finishInstantiate && !needToStop && (canLeft || canRight))
                {
                    player.transform.position = positions[playerPosition].transform.position;
                }

                if ((Input.GetButtonDown("Left_Bumper") || Input.GetButtonDown("Right_Bumper")) && finishInstantiate)
                {
                    if (playerPosition < positions.Count && !needToStop)
                    {

                        PlayerInput();
                    }

                }
            }

            void PlayerInput()
            {
                if ((Input.GetButtonDown("Left_Bumper") || Input.GetMouseButtonDown(0)) && canLeft)
                {
                    
                    canRight = true;
                    playerPosition += 1;
                    nextSound++;
                    canLeft = false;
                    playerSkin.GetComponent<SpriteRenderer>().flipX = false;

                    if (nextSound == 2)
                    {
                        nextSound = 0;
                        GameObject audiosourceSpawned = Instantiate(audioSource, transform.position, Quaternion.identity);
                        audiosourceSpawned.transform.parent = gameObject.transform;
                        audiosourceSpawned.GetComponent<AudioSource>().clip = inputSounds[playerPosition / 2];
                        audiosourceSpawned.GetComponent<AudioSource>().Play();
                    }
                    return;

                }
                else if ((Input.GetButtonDown("Right_Bumper") || Input.GetMouseButtonDown(1)) && canRight)
                {
                    
                    canLeft = true;
                    canRight = false;
                    nextSound++;
                    playerPosition++;
                    playerSkin.GetComponent<SpriteRenderer>().flipX = true;

                    if (nextSound == 2)
                    {
                        nextSound = 0;
                        GameObject audiosourceSpawned = Instantiate(audioSource, transform.position, Quaternion.identity);
                        audiosourceSpawned.GetComponent<AudioSource>().clip = inputSounds[playerPosition / 2];
                        audiosourceSpawned.GetComponent<AudioSource>().Play();
                    }
                    return;
                }


            }
        }
    }
}
