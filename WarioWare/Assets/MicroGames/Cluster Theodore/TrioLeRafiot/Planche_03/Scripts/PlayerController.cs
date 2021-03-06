﻿using System.Collections;
using UnityEngine;
using Caps;

namespace LeRafiot
{
    namespace Planche
    {
        /// <summary>
        /// Guillaume Rogé
        /// This script control the player
        /// </summary>

        public class PlayerController : TimedBehaviour
        {
            public static PlayerController Instance;

            #region Variable

            [Header ("Rotation point")]
            public Sprite[] sprites;

            [Header ("Player value")]
            public int playerSprite;
            public bool canMove;

            public GameObject[] deathSprite;

            [HideInInspector] public bool playerDrowned;
            [HideInInspector] public bool playerTouched;

            private bool animLock;
            #endregion 

            public override void Start()
            {
                base.Start();

                ManagerInit();

                canMove = true;
                playerSprite = 2;
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

            public override void TimedUpdate()
            {
                if (Tick == 8 && !Manager.Instance.panel.activeSelf && playerDrowned)
                {
                    Manager.Instance.Result(false);
                }
                else if (Tick == 8 && !Manager.Instance.panel.activeSelf && playerTouched)
                {
                    Manager.Instance.Result(false);
                }
            }

            void Update()
            {
                PlayerMouvement();

                TriggerBoxPosition();

                if (playerDrowned == true || playerTouched ==  true)
                {
                    if (animLock == false)
                    {
                        StartCoroutine(DeathAnimation());
                    }
                }
            }


            void ManagerInit()
            {
                if (Instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            void PlayerMouvement()
            {
                if (canMove)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("Right_Bumper"))
                    {

                        if (playerSprite < 4)
                        {
                            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites[playerSprite + 1];
                            playerSprite++;
                        }

                        else
                        {
                            if(!Manager.Instance.panel.activeSelf)
                            {
                                playerDrowned = true;
                                RandomEnemySpawn.Instance.spawnDisabled = true;
                                canMove = false;

                                SoundManagerPlanche.Instance.sfxSound[1].Play();
                                SoundManagerPlanche.Instance.sfxSound[4].Play();
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown("Left_Bumper"))
                    {

                        if (playerSprite > 0)
                        {
                            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites[playerSprite - 1];
                            playerSprite--;
                        }
                        else
                        {
                            if (!Manager.Instance.panel.activeSelf)
                            {
                                playerDrowned = true;
                                RandomEnemySpawn.Instance.spawnDisabled = true;
                                canMove = false;
                                //Manager.Instance.Result(false);
                                SoundManagerPlanche.Instance.sfxSound[1].Play();
                                SoundManagerPlanche.Instance.sfxSound[4].Play();
                            }
                        }
                    }
                }
            }

            void TriggerBoxPosition()
            {
                if (playerSprite == 0) //left 2
                {
                    gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-8.56f, 3.65f);
                }
                else if (playerSprite == 1) //left 1
                {
                    gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-6.53f, 8.22f);
                }
                else if (playerSprite == 2) //mid
                {
                    gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.5198772f, 11.45f);
                }
                else if (playerSprite == 3) //right 1
                {
                    gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(7.17f, 8.22f);
                }
                else if (playerSprite == 4) //right 2
                {
                    gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(9.48f, 3.65f);
                }
            }

            IEnumerator DeathAnimation()
            {
                animLock = true;

                if (playerSprite >= 2) // Gestion droite
                {
                    
                    deathSprite[0].gameObject.SetActive(false);
                    deathSprite[1].gameObject.SetActive(true);

                    yield return new WaitForSeconds(0.5f);

                    deathSprite[1].gameObject.SetActive(false);
                    deathSprite[2].gameObject.SetActive(true);

                    yield return new WaitForSeconds(0.5f);

                    deathSprite[2].gameObject.SetActive(false);

                }

                if (playerSprite < 2) //Gestion gauche
                {
                    deathSprite[0].gameObject.SetActive(false);
                    deathSprite[3].gameObject.SetActive(true);

                    yield return new WaitForSeconds(0.5f);

                    deathSprite[3].gameObject.SetActive(false);
                    deathSprite[4].gameObject.SetActive(true);

                    yield return new WaitForSeconds(0.5f);

                    deathSprite[4].gameObject.SetActive(false);
                }

            }
        }
    }
}
