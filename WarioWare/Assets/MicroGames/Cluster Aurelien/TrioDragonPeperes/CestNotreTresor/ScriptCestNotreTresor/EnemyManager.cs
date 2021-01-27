using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Dragons_Peperes
{
    namespace CestNotreTresor
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>

        public class EnemyManager : TimedBehaviour
        {
            public GameObject showInput;

            public GameObject enemy;
            public GameObject enemy2;
            public GameObject lostScreen;

            [Space]
            [Header("Lieux de spawns pour les enemies")]
            public GameObject spot1;
            public GameObject spot2;
            public GameObject spot3;

            private EnemyController enemyController;

            TimedBehaviour timedBehaviour;
            AudioManager audioManager;

            public bool playerLost;

            public GameObject particule;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                enemyController = FindObjectOfType<EnemyController>();
                audioManager = FindObjectOfType<AudioManager>();

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
            }

            public void Update()
            {
                if (playerLost)
                {
                    lostScreen.SetActive(true);
                                     
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                //base.TimedUpdate();
                if (Tick == 1)
                {
                    audioManager.PlayMusic();
                }

                #region EasyMode

                if (currentDifficulty == Difficulty.EASY)
                {
                    if (Tick == 1)
                    {
                        Debug.Log("inputshow");
                        showInput.SetActive(true);
                    }

                    if (Tick == 3)
                    {
                        Debug.Log("spawntie 1");
                        Destroy(showInput);
                        Instantiate(enemy, spot2.transform);
                        audioManager.PlayRandomReplique();
                    }                  

                    if (Tick == 5)
                    {
                        Debug.Log("spawntie 2");
                        Destroy(showInput);
                        Instantiate(enemy, spot2.transform);
                    }


                    


                    if (Tick == 8)
                    {
                        particule.SetActive(false);

                        if (playerLost)
                        {
                             Manager.Instance.Result(false);
                        }
                        

                        if (!playerLost)
                        {
                             audioManager.StopMusic();
                             Manager.Instance.Result(true);
                        }
                        
                    }
                }
                    
                #endregion

                #region MediumMode

                if(currentDifficulty == Difficulty.MEDIUM)
                {
                    if (Tick == 1)
                    {
                        showInput.SetActive(true);
                    }
                        

                    if (Tick == 2)
                    {
                        Instantiate(enemy, spot2.transform);
                        audioManager.PlayRandomReplique();
                    }

                    if (Tick == 3)
                    {
                        Destroy(showInput);
                        Instantiate(enemy, spot3.transform);
                    }

                    if (Tick == 5)
                    {
                        Instantiate(enemy, spot1.transform);
                        audioManager.PlayRandomReplique();
                    }

                        

                    if (Tick == 8)
                    {
                        particule.SetActive(false);

                        if (playerLost)
                        {
                            Manager.Instance.Result(false);
                        }

                        if (!playerLost)
                        {
                            audioManager.StopMusic();
                            Manager.Instance.Result(true);
                        }
                    }
                }
                #endregion

                #region HardMode

                if(currentDifficulty == Difficulty.HARD)
                {
                    if (Tick == 1)
                    {
                        showInput.SetActive(true);
                    }

                    if (Tick == 2)
                    {
                        Instantiate(enemy, spot1.transform);
                        audioManager.PlayRandomReplique();
                    }

                    if (Tick == 3)
                    {
                        Instantiate(enemy, spot3.transform);
                    }

                    if (Tick == 5)
                    {
                        Instantiate(enemy, spot2.transform);
                        Instantiate(enemy2, spot2.transform);
                        audioManager.PlayRandomReplique();

                    }

                    if(Tick == 6)
                    {
                        Instantiate(enemy2, spot3.transform);
                    }



                    if (Tick == 8)
                    {
                        particule.SetActive(false);

                        if (playerLost)
                        {
                            Manager.Instance.Result(false);
                        }

                        if (!playerLost)
                        {
                            audioManager.StopMusic();
                            Manager.Instance.Result(true);
                        }
                    }
                }
                #endregion
            }

            
        }
    }
}