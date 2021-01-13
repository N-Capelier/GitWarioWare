using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace Les4Pieces
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>
        public class GameManager : TimedBehaviour
        {
            public bool enableInput;

            private CoinManager coinManager;
            private CarteManager carteManager;
            public CoinController[] coinController;

            public GameObject fourthSpot;

            public GameObject winScreen;
            public GameObject lostScreen;

            bool playerLost;
            bool playerWon;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                coinManager = FindObjectOfType<CoinManager>();
                carteManager = FindObjectOfType<CarteManager>();

                #region Spawn Coins
                if(currentDifficulty == Difficulty.EASY)
                {
                    coinManager.SpawnCoinsEasy();
                    coinController = FindObjectsOfType<CoinController>();
                }
                if (currentDifficulty == Difficulty.MEDIUM)
                {
                    coinManager.SpawnCoinsMedium();
                    coinController = FindObjectsOfType<CoinController>();
                }
                if (currentDifficulty == Difficulty.HARD)
                {
                    coinManager.SpawnCoinsHard();
                    coinController = FindObjectsOfType<CoinController>();
                }
                #endregion

                if (currentDifficulty != Difficulty.EASY)
                {
                    fourthSpot.SetActive(true);
                }
            }



            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                #region EasyMode

                if(currentDifficulty == Difficulty.EASY)
                {

                    if(Tick == 5)
                    {
                        enableInput = true;
                        for (int i = 0; i < coinController.Length; i++)
                        {
                            coinController[i].hideCoins = true;
                        }
                        carteManager.SpawnRandomCarteEasy();
                    }

                    if(Tick == 7)
                    {
                        //destroy hiddencoins
                    }

                    if(Tick == 8)
                    {
                        //le joueur n'a pas trouvé la bonne pièce à temps, il loose
                        if (playerWon)
                        {
                            Caps.Manager.Instance.Result(true);
                        }
                        else
                        {
                            Caps.Manager.Instance.Result(false);
                        }
                    }
                }
                #endregion

                #region MediumMode

                if(currentDifficulty == Difficulty.MEDIUM)
                {

                    if (Tick == 5)
                    {
                        enableInput = true;
                        for (int i = 0; i < coinController.Length; i++)
                        {
                            coinController[i].hideCoins = true;
                        }
                        carteManager.SpawnRandomCarteMedium();
                    }

                    if (Tick == 7)
                    {
                        //destroy hiddencoins
                    }

                    if (Tick == 8)
                    {
                        //le joueur n'a pas trouvé la bonne pièce à temps, il loose
                        if (playerWon)
                        {
                            Caps.Manager.Instance.Result(true);
                        }
                        else
                        {
                            Caps.Manager.Instance.Result(false);
                        }

                    }
                }

                #endregion

                #region HardMode

                if (currentDifficulty == Difficulty.HARD)
                {
                    if (Tick == 0)
                    {
                        coinManager.SpawnCoinsHard();
                        coinController = FindObjectsOfType<CoinController>();
                    }

                    if (Tick == 5)
                    {
                        enableInput = true;
                        for (int i = 0; i < coinController.Length; i++)
                        {
                            coinController[i].hideCoins = true;
                        }
                        carteManager.SpawnRandomCarteHard();
                    }

                    if (Tick == 7)
                    {
                        //destroy hiddencoins
                    }

                    if (Tick == 8)
                    {
                        //le joueur n'a pas trouvé la bonne pièce à temps, il loose
                        if (playerWon)
                        {
                            Caps.Manager.Instance.Result(true);
                        }
                        else
                        {
                            Caps.Manager.Instance.Result(false);
                        }

                        if (playerLost)
                        {
                            //Caps.Manager.Instance.Result(false);
                        }
                    }
                }

                #endregion
            }

            public void YouWIn()
            {
                //ptite bool pour signifier la victoire
                playerWon = true;
                //boom winScreen
                winScreen.SetActive(true);
            }

            public void YouLost()
            {
                playerLost = false;
                //boom looseScreen
                lostScreen.SetActive(true);
            }
        }
    }
}