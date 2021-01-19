using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioLLL
{
    namespace Pintade
    {
        /// <summary>
        /// VIDAL Luc
        /// </summary>
        public class PintadeGlobalManager : TimedBehaviour
        {
            private bool inputPressed;
            private bool leftInputActivated;
            private bool rightInputActivated;

            private int leftChoice;
            private int rightChoice;
            public int bigChoice;

            public GameObject leftGrass;
            public GameObject rightGrass;
            public GameObject rightAnimalParent;
            public GameObject leftAnimalParent;
            public GameObject leafParticles;

            public GameObject leftArrow;
            public GameObject rightArrow;

            public GameObject leftParticleSpawner;
            public GameObject rightParticleSpawner;

            public GameObject servalManager;

            public GameObject angrySymbol;

            public GameObject soundManager;

            [HideInInspector] public bool frogEaten;
            [HideInInspector] public bool pintadeEaten;
            [HideInInspector] public bool nothingEaten;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                bigChoice = Random.Range(0,2);
                Debug.Log(bigChoice);
                inputPressed = false;
                pintadeEaten = false;
                frogEaten = false;
                nothingEaten = false;
                leftInputActivated = false;
                leftChoice = 10;
                rightChoice = 10;
                angrySymbol.SetActive(false);
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (inputPressed == false && leftInputActivated == true || rightInputActivated == true && inputPressed == false)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetButton("X_Button"))
                    {
                        if (leftGrass.GetComponent<TouffeManager>().pintadeON == true)
                        {
                            pintadeEaten = true;
                            servalManager.GetComponent<ServalManager>().leftActivated = true;
                            servalManager.GetComponent<ServalManager>().happyServal = true;
                        }
                        else if (leftGrass.GetComponent<TouffeManager>().pintadeON == false)
                        {
                            if (leftGrass.GetComponent<TouffeManager>().frogON == true)
                            {
                                frogEaten = true;
                                servalManager.GetComponent<ServalManager>().leftActivated = true;
                                servalManager.GetComponent<ServalManager>().sickServal = true;
                            }
                            else if (leftGrass.GetComponent<TouffeManager>().frogON == false)
                            {
                                nothingEaten = true;
                            }
                        }

                        inputPressed = true;
                    }

                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetButton("B_Button"))
                    {
                        if (rightGrass.GetComponent<TouffeManager>().pintadeON == true)
                        {
                            pintadeEaten = true;
                            servalManager.GetComponent<ServalManager>().rightActivated = true;
                            servalManager.GetComponent<ServalManager>().happyServal = true;
                        }
                        else if (rightGrass.GetComponent<TouffeManager>().pintadeON == false)
                        {
                            if (rightGrass.GetComponent<TouffeManager>().frogON == true)
                            {
                                frogEaten = true;
                                servalManager.GetComponent<ServalManager>().rightActivated = true;
                                servalManager.GetComponent<ServalManager>().sickServal = true;
                            }
                            else if (rightGrass.GetComponent<TouffeManager>().frogON == false)
                            {
                                nothingEaten = true;
                            }
                        }

                        inputPressed = true;
                    }
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                Debug.Log(Tick);

                if (Tick == 2)
                {
                    switch (currentDifficulty)
                    {
                        case Difficulty.EASY:
                            EasyTouffeChoice();
                            break;

                        case Difficulty.MEDIUM:
                            MediumAndHardTouffeChoice();
                            break;

                        case Difficulty.HARD:
                            MediumAndHardTouffeChoice();
                            break;
                    }
                }

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        if (Tick == 4)
                        {
                            leftArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                            rightArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                            leftInputActivated = true;
                            rightInputActivated = true;
                        }
                        break;

                    case Difficulty.MEDIUM:
                        if (Tick == 4)
                        {
                            leftArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                            rightArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                            leftInputActivated = true;
                            rightInputActivated = true;
                        }
                        break;

                    case Difficulty.HARD:
                        if (Tick == 3)
                        {
                            if (bigChoice == 1)
                            {
                                if (rightChoice == 1 && leftChoice == 2)
                                {
                                    leftArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                    leftInputActivated = true;
                                }
                                else if (leftChoice == 1 && rightChoice == 2)
                                {
                                    rightArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                    rightInputActivated = true;
                                }
                                else if (leftChoice == 2 && rightChoice == 2)
                                {
                                    rightArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                    rightInputActivated = true;
                                }
                            }
                            else if (bigChoice == 0)
                            {
                                if (leftChoice == 1 && rightChoice == 2)
                                {
                                    rightArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                    rightInputActivated = true;
                                }
                                else if (rightChoice == 1 && leftChoice == 2)
                                {
                                    leftArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                    leftInputActivated = true;
                                }
                                else if (leftChoice == 2 && rightChoice == 2)
                                {
                                    leftArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                    leftInputActivated = true;
                                }
                            }
                        }

                        if (Tick == 5)
                        {
                            if (bigChoice == 1)
                            {
                                leftArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                leftInputActivated = true;
                            }
                            else if (bigChoice == 0)
                            {
                                rightArrow.GetComponent<ArrowInputBehaviour>().activated = true;
                                rightInputActivated = true;
                            }
                        }
                        break;
                }

                if (Tick == 7 && inputPressed == false)
                {
                    inputPressed = true;

                    if (rightGrass.GetComponent<TouffeManager>().pintadeON == true || leftGrass.GetComponent<TouffeManager>().pintadeON == true)
                    {
                        angrySymbol.SetActive(true);

                        if (leftGrass.GetComponent<TouffeManager>().pintadeON == true)
                        {
                            Instantiate(leafParticles, leftParticleSpawner.transform);
                            soundManager.GetComponent<PintadeSoundManager>().pintade = true;
                        }
                        if (leftGrass.GetComponent<TouffeManager>().frogON == true)
                        {
                            Instantiate(leafParticles, leftParticleSpawner.transform);
                            soundManager.GetComponent<PintadeSoundManager>().frog1 = true;
                        }
                        if (rightGrass.GetComponent<TouffeManager>().pintadeON == true)
                        {
                            Instantiate(leafParticles, rightParticleSpawner.transform);
                            soundManager.GetComponent<PintadeSoundManager>().pintade = true;
                        }
                        if (rightGrass.GetComponent<TouffeManager>().frogON == true)
                        {
                            Instantiate(leafParticles, rightParticleSpawner.transform);
                            soundManager.GetComponent<PintadeSoundManager>().frog2 = true;
                        }

                        leftAnimalParent.SetActive(false);
                        rightAnimalParent.SetActive(false);
                    }

                }

                if (Tick == 8)
                {
                    if (pintadeEaten == false && frogEaten == false && nothingEaten == false)
                    {
                        nothingEaten = true;
                    }

                    if (pintadeEaten == true)
                    {
                        Manager.Instance.Result(true);
                    }
                    else if (frogEaten == true)
                    {
                        Manager.Instance.Result(false);
                    }
                    else if (nothingEaten == true)
                    {
                        if (leftGrass.GetComponent<TouffeManager>().pintadeON == true || rightGrass.GetComponent<TouffeManager>().pintadeON == true)
                        {
                            Manager.Instance.Result(false);
                        }
                        else
                        {
                            Manager.Instance.Result(true);
                        }
                    }
                }
            }

            private void EasyTouffeChoice()
            {
                if (bigChoice == 0)
                {
                    leftChoice = leftGrass.GetComponent<TouffeManager>().choice;
                }

                else if (bigChoice == 1)
                {
                    rightChoice = rightGrass.GetComponent<TouffeManager>().choice;
                }

                if (rightChoice == 1)
                {
                    rightGrass.GetComponent<TouffeManager>().pintadeON = true;

                }
                else if (rightChoice == 2)
                {
                    rightGrass.GetComponent<TouffeManager>().frogON = true;
                }

                if (leftChoice == 1)
                {
                    leftGrass.GetComponent<TouffeManager>().pintadeON = true;
                }
                else if (leftChoice == 2)
                {
                    leftGrass.GetComponent<TouffeManager>().frogON = true;
                }
            }

            private void MediumAndHardTouffeChoice()
            {
                if (bigChoice == 0)
                {
                    leftChoice = leftGrass.GetComponent<TouffeManager>().choice;

                    if (leftChoice == 1)
                    {
                        rightChoice = 2;
                        bigChoice = 1;
                    }
                    else if (leftChoice == 2)
                    {
                        rightChoice = rightGrass.GetComponent<TouffeManager>().choice;
                        bigChoice = 0;
                    }
                }
                else if (bigChoice == 1)
                {
                    rightChoice = rightGrass.GetComponent<TouffeManager>().choice;

                    if (rightChoice == 1)
                    {
                        leftChoice = 2;
                        bigChoice = 0;
                    }
                    else if (rightChoice == 2)
                    {
                        leftChoice = leftGrass.GetComponent<TouffeManager>().choice;
                        bigChoice = 1;
                    }
                }

                if (rightChoice == 1)
                {
                    rightGrass.GetComponent<TouffeManager>().pintadeON = true;

                }
                else if (rightChoice == 2)
                {
                    rightGrass.GetComponent<TouffeManager>().frogON = true;
                }

                if (leftChoice == 1)
                {
                    leftGrass.GetComponent<TouffeManager>().pintadeON = true;
                }
                else if (leftChoice == 2)
                {
                    leftGrass.GetComponent<TouffeManager>().frogON = true;
                }
            }
        }
    }
}