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
        public class InputsManager : MonoBehaviour
        {
            private CarteManager carteManager;
            private GameManager gameManager;
            //private CarteController carteController;

            [SerializeField] bool isSpot1;
            [SerializeField] bool isSpot2;
            [SerializeField] bool isSpot3;
            [SerializeField] bool isSpot4;


            #region Which coin is where (bools)
            [SerializeField] bool coin1OnSpot1;
            [SerializeField] bool coin1OnSpot2;
            [SerializeField] bool coin1OnSpot3;
            [SerializeField] bool coin1OnSpot4;

            [SerializeField] bool coin2OnSpot1;
            [SerializeField] bool coin2OnSpot2;
            [SerializeField] bool coin2OnSpot3;
            [SerializeField] bool coin2OnSpot4;

            [SerializeField] bool coin3OnSpot1;
            [SerializeField] bool coin3OnSpot2;
            [SerializeField] bool coin3OnSpot3;
            [SerializeField] bool coin3OnSpot4;

            [SerializeField] bool coin4OnSpot1;
            [SerializeField] bool coin4OnSpot2;
            [SerializeField] bool coin4OnSpot3;
            [SerializeField] bool coin4OnSpot4;
            #endregion

            private void Start()
            {
                carteManager = FindObjectOfType<CarteManager>();
                gameManager = FindObjectOfType<GameManager>();
                //carteController = FindObjectOfType<CarteController>();
            }

            private void Update()
            {
                if (gameManager.enableInput)
                {

                    if (isSpot1)
                    {
                        //Y button
                        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
                        {
                            Debug.Log("Y frer");
                            if (coin1OnSpot1)
                            {
                                if (carteManager.weWantCoin1 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }

                            }

                            if (coin2OnSpot1)
                            {
                                if (carteManager.weWantCoin2 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin3OnSpot1)
                            {
                                if (carteManager.weWantCoin3 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin4OnSpot1)
                            {
                                if (carteManager.weWantCoin4 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }
                        }
                    }

                    if (isSpot2)
                    {
                        //B button
                        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                        {
                            Debug.Log("B frer");
                            if (coin1OnSpot2)
                            {
                                if (carteManager.weWantCoin1 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin2OnSpot2)
                            {
                                if (carteManager.weWantCoin2 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin3OnSpot2)
                            {
                                if (carteManager.weWantCoin3 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin4OnSpot2)
                            {
                                if (carteManager.weWantCoin4 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }
                        }
                    }

                    if (isSpot3)
                    {
                        //X button
                        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                        {
                            Debug.Log("A frer");
                            if (coin1OnSpot3)
                            {
                                if (carteManager.weWantCoin1 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin2OnSpot3)
                            {
                                if (carteManager.weWantCoin2 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin3OnSpot3)
                            {
                                if (carteManager.weWantCoin3 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin4OnSpot3)
                            {
                                if (carteManager.weWantCoin4 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }
                        }
                    }

                    if (isSpot4)
                    {
                        //A button
                        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                        {
                            Debug.Log("X frer");
                            if (coin1OnSpot4)
                            {
                                if (carteManager.weWantCoin1 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin2OnSpot4)
                            {
                                if (carteManager.weWantCoin2 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin3OnSpot4)
                            {
                                if (carteManager.weWantCoin3 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }

                            if (coin4OnSpot4)
                            {
                                if (carteManager.weWantCoin4 == true)
                                {
                                    Debug.Log("epic win bro");
                                    //destroy hiddencoins
                                    gameManager.YouWIn();
                                }
                                else
                                {
                                    Debug.Log("Fuck u");
                                    //destroy hiddencoins
                                    gameManager.YouLost();
                                }
                            }
                        }
                    }
                }


            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                #region Where are those coins

                #region Check where is Coin1
                if ((collision.name == "Coin_1(Clone)") && (isSpot1))
                {
                    coin1OnSpot1 = true;
                }

                if ((collision.name == "Coin_1(Clone)") && (isSpot2))
                {
                    coin1OnSpot2 = true;
                }

                if ((collision.name == "Coin_1(Clone)") && (isSpot3))
                {
                    coin1OnSpot3 = true;
                }

                if ((collision.name == "Coin_1(Clone)") && (isSpot4))
                {
                    coin1OnSpot4 = true;
                }
                #endregion

                #region Check where is Coin2
                if ((collision.name == "Coin_2(Clone)") && (isSpot1))
                {
                    coin2OnSpot1 = true;

                }

                if ((collision.name == "Coin_2(Clone)") && (isSpot2))
                {
                    coin2OnSpot2 = true;
                }

                if ((collision.name == "Coin_2(Clone)") && (isSpot3))
                {
                    coin2OnSpot3 = true;

                }

                if ((collision.name == "Coin_2(Clone)") && (isSpot4))
                {
                    coin2OnSpot4 = true;

                }
                #endregion

                #region Check where is Coin3
                if ((collision.name == "Coin_3(Clone)") && (isSpot1))
                {
                    coin3OnSpot1 = true;

                }

                if ((collision.name == "Coin_3(Clone)") && (isSpot2))
                {
                    coin3OnSpot2 = true;
                }

                if ((collision.name == "Coin_3(Clone)") && (isSpot3))
                {
                    coin3OnSpot3 = true;

                }

                if ((collision.name == "Coin_3(Clone)") && (isSpot4))
                {
                    coin3OnSpot4 = true;

                }
                #endregion

                #region Check where is Coin4
                if ((collision.name == "Coin_4(Clone)") && (isSpot1))
                {
                    coin4OnSpot1 = true;

                }

                if ((collision.name == "Coin_4(Clone)") && (isSpot2))
                {
                    coin4OnSpot2 = true;
                }

                if ((collision.name == "Coin_4(Clone)") && (isSpot3))
                {
                    coin4OnSpot3 = true;

                }

                if ((collision.name == "Coin_4(Clone)") && (isSpot4))
                {
                    coin4OnSpot4 = true;

                }
                #endregion

                #region Check where is CoinHard1
                if ((collision.name == "CoinHard_1(Clone)") && (isSpot1))
                {
                    coin1OnSpot1 = true;
                    Debug.Log("coin_1 is on spot 1");
                }

                if ((collision.name == "CoinHard_1(Clone)") && (isSpot2))
                {
                    coin1OnSpot2 = true;
                    Debug.Log("coin_1 is on spot 2");
                }

                if ((collision.name == "CoinHard_1(Clone)") && (isSpot3))
                {
                    coin1OnSpot3 = true;
                    Debug.Log("coin_1 is on spot 3");
                }

                if ((collision.name == "CoinHard_1(Clone)") && (isSpot4))
                {
                    coin1OnSpot4 = true;
                    Debug.Log("coin_1 is on spot 4");
                }

                #endregion

                #region Check where is CoinHard2
                if ((collision.name == "CoinHard_2(Clone)") && (isSpot1))
                {
                    coin2OnSpot1 = true;
                    Debug.Log("coin_2 is on spot 1");
                }

                if ((collision.name == "CoinHard_2(Clone)") && (isSpot2))
                {
                    coin2OnSpot2 = true;
                    Debug.Log("coin_2 is on spot 2");
                }

                if ((collision.name == "CoinHard_2(Clone)") && (isSpot3))
                {
                    coin2OnSpot3 = true;
                    Debug.Log("coin_2 is on spot 3");
                }

                if ((collision.name == "CoinHard_2(Clone)") && (isSpot4))
                {
                    coin2OnSpot4 = true;
                    Debug.Log("coin_2 is on spot 4");
                }
                #endregion

                #region Check where is CoinHard3
                if ((collision.name == "CoinHard_3(Clone)") && (isSpot1))
                {
                    coin3OnSpot1 = true;
                    Debug.Log("coin_3 is on spot 1");
                }

                if ((collision.name == "CoinHard_3(Clone)") && (isSpot2))
                {
                    coin3OnSpot2 = true;
                    Debug.Log("coin_3 is on spot 2");
                }

                if ((collision.name == "CoinHard_3(Clone)") && (isSpot3))
                {
                    coin3OnSpot3 = true;
                    Debug.Log("coin_3 is on spot 3");
                }

                if ((collision.name == "CoinHard_3(Clone)") && (isSpot4))
                {
                    coin3OnSpot4 = true;
                    Debug.Log("coin_3 is on spot 4");
                }
                #endregion

                #region Check where is CoinHard4
                if ((collision.name == "CoinHard_4(Clone)") && (isSpot1))
                {
                    coin4OnSpot1 = true;
                    Debug.Log("coin_4 is on spot 1");
                }

                if ((collision.name == "CoinHard_4(Clone)") && (isSpot2))
                {
                    coin4OnSpot2 = true;
                    Debug.Log("coin_4 is on spot 2");
                }

                if ((collision.name == "CoinHard_4(Clone)") && (isSpot3))
                {
                    coin4OnSpot3 = true;
                    Debug.Log("coin_4 is on spot 3");
                }

                if ((collision.name == "CoinHard_4(Clone)") && (isSpot4))
                {
                    coin4OnSpot4 = true;
                    Debug.Log("coin_4 is on spot 4");
                }
                #endregion
                #endregion
            }
        }

    
    }
}
