﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioSoupe
{
    namespace ServiceRapide
    {
        /// <summary>
        ///  Manon Ghignoni
        /// </summary>
        public class ManagerServiceRapide : TimedBehaviour
        {
            int chefMeatInt;
            int chefBreadInt;
            int chefVegetableInt;

            public GameObject memoryBackground;
            public GameObject selectionBackgroundD1;
            public GameObject selectionBackgroundD0;
            GameObject selectionBackground;
            GameObject currentSelectionBackGround;
            public GameObject victoryScreen;
            public GameObject looseScreen;

            public GameObject sadFace;
            public GameObject neutralFace;
            public GameObject HappyFace;

            GameObject chefBread;
            GameObject chefMeat;
            GameObject chefVegetable;
            GameObject chefBreadBase;

            public GameObject[] meat;
            public GameObject[] bread;
            public GameObject[] breadBase;
            public GameObject[] vegetable;
            public GameObject[] hardMeat;
            public GameObject[] hardBread;
            public GameObject hardBreadBase;
            public GameObject[] hardVegetable;
            public Vector3[] ingredientScreenPositions;
            List<int> ingredientChosedPerPos;

            public Vector3 chefBreadPos;
            public Vector3 chefMeatPos;
            public Vector3 chefVegetablePos;
            public Vector3 chefBreadBasePos;

            public float memorizationTime;

            bool memorizationOver;
            bool hasStartedMemorization;

            bool playerChosedMeat;
            bool playerChosedBread;
            bool playerChosedVegetable;

            int difficulty;
            bool loose;
            bool win;
            bool launchPlaceRandomIngredient;

            public float timeBetweenSteps;

            bool canChoose;

            GameObject[] ingredientChosingPhase;

            public Animator animatorBoutons;

            public override void Start()
            {

                base.Start();
                difficulty = (int)currentDifficulty;
                canChoose = true;
                loose = false;
                ingredientChosedPerPos = new List<int>();
                ingredientChosingPhase = new GameObject[4];
                launchPlaceRandomIngredient = true;
                memorizationOver = false;
                if (difficulty == 0)
                {
                    chefMeatInt = Random.Range(0, 3);
                    chefBreadInt = Random.Range(0, 3);
                    chefVegetableInt = Random.Range(0, 3);
                    selectionBackground = selectionBackgroundD0;
                    animatorBoutons.SetInteger("Difficulty", 0);
                }
                else
                {
                    chefMeatInt = Random.Range(0, 4);
                    chefBreadInt = Random.Range(0, 4);
                    chefVegetableInt = Random.Range(0, 4);
                    selectionBackground = selectionBackgroundD1;
                    animatorBoutons.SetInteger("Difficulty", 1);
                }
                InstantiateChefIngredients();

                
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

            private void Update()
            {
                if (win == false && loose == false && Tick < 8 && Tick > 0 )
                {
                    if (hasStartedMemorization == false)
                    {
                        StartCoroutine(MemorizationPhase());

                    }
                    if (playerChosedBread == false && memorizationOver == true)
                    {
                        PlayerChose(chefBreadInt, ref playerChosedBread);
                    }
                    if (playerChosedVegetable == false && playerChosedBread == true)
                    {
                        if(difficulty == 2)
                        {
                            RandomPlaceIngredient(hardVegetable);
                        }
                        else
                        {
                            RandomPlaceIngredient(vegetable);
                        }
                        PlayerChose(chefVegetableInt, ref playerChosedVegetable);
                    }
                    if (playerChosedMeat == false && playerChosedVegetable == true)
                    {
                        if (difficulty == 2)
                        {
                            RandomPlaceIngredient(hardMeat);
                        }
                        else
                        {
                            RandomPlaceIngredient(meat);
                        }
                        PlayerChose(chefMeatInt, ref playerChosedMeat);

                        if(playerChosedMeat == true)
                        {
                            animatorBoutons.SetBool("EndGame", true);
                            victoryScreen.SetActive(true);
                            InstantiateChefIngredients();
                            HappyFace.SetActive(true);
                            Destroy(currentSelectionBackGround,0f);
                            win = true;
                        }
                    }
                }
                if (Tick == 8)
                {
                    if(win == true)
                    {
                        Manager.Instance.Result(true);
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                    } 
                }
            }
            public override void TimedUpdate()
            {
                if (Tick % 2 == 0)
                {
                    animatorBoutons.SetBool("pressed", true);
                }
                else
                {
                    animatorBoutons.SetBool("pressed", false);
                }

            }
            IEnumerator MemorizationPhase()
            {
                hasStartedMemorization = true;
                yield return new WaitForSeconds(memorizationTime);
                Destroy(chefBread);
                Destroy(chefMeat);
                Destroy(chefVegetable);
                Destroy(chefBreadBase);
                neutralFace.SetActive(false);


                if (difficulty == 2)
                {
                    RandomPlaceIngredient(hardBread);
                }
                else
                {
                    RandomPlaceIngredient(bread);
                }
                memoryBackground.SetActive(false);
                currentSelectionBackGround = Instantiate(selectionBackground, Vector3.zero, Quaternion.identity);
                animatorBoutons.SetBool("memorisationOver", true);
                memorizationOver = true;
            }

            void RandomPlaceIngredient(GameObject[] ingredient)
            {
                if (launchPlaceRandomIngredient == true)
                {
                    int maxi;
                    if(difficulty == 0)
                    {
                        maxi = 3;
                    }
                    else
                    {
                        maxi = 4;
                    }
                   
                    for (int i = 0; i < maxi; i++)
                    {
                        bool isIngredientAlreadyIn;
                        int randomIngredient;
                        if (i == 0)
                        {
                            if(difficulty == 0)
                            {
                                randomIngredient = Random.Range(0, 3);
                                
                            }
                            else
                            {
                                randomIngredient = Random.Range(0, 4);
                            }
                            ingredientChosingPhase[i] = Instantiate(ingredient[randomIngredient], ingredientScreenPositions[i], Quaternion.identity);
                            ingredientChosedPerPos.Add(randomIngredient);
                        }
                        else
                        {
                            do
                            {
                                isIngredientAlreadyIn = false;
                                if (difficulty == 0)
                                {
                                    randomIngredient = Random.Range(0, 3);
                                }
                                else
                                {
                                    randomIngredient = Random.Range(0, 4);
                                }
                                foreach (int ingredientChosed in ingredientChosedPerPos)
                                {
                                    if (randomIngredient == ingredientChosed)
                                    {
                                        isIngredientAlreadyIn = true;
                                    }
                                }

                            } while (isIngredientAlreadyIn == true);

                            ingredientChosingPhase[i] =  Instantiate(ingredient[randomIngredient], ingredientScreenPositions[i], Quaternion.identity);
                            ingredientChosedPerPos.Add(randomIngredient);
                            
                        }
                    }
                }
                launchPlaceRandomIngredient = false;
                
            }

            void PlayerChose(int checkedChefIngredient, ref bool checkedPlayerIngredient)
            {

                if (canChoose == true)
                {
                    if (Input.GetButtonDown("X_Button")) 
                    {
                        StartCoroutine(StartChooseCD());
                        if (ingredientChosedPerPos[0] == checkedChefIngredient)
                        {
                            PlayerPickedCorrect(ref checkedPlayerIngredient);
                        }
                        else
                        {
                            PlayerLoose();
                        }
                        ingredientChosedPerPos.Clear();
                    }
                    if (Input.GetButtonDown("Y_Button"))
                    {
                        StartCoroutine(StartChooseCD());
                        if (ingredientChosedPerPos[1] == checkedChefIngredient)
                        {
                            PlayerPickedCorrect(ref checkedPlayerIngredient);
                        }
                        else
                        {
                            PlayerLoose();
                        }

                    }
                    if (Input.GetButtonDown("B_Button"))
                    {
                        StartCoroutine(StartChooseCD());
                        if (ingredientChosedPerPos[2] == checkedChefIngredient)
                        {
                            PlayerPickedCorrect(ref checkedPlayerIngredient);
                        }
                        else
                        {
                            PlayerLoose();
                        }

                    }
                    if (Input.GetButtonDown("A_Button") && difficulty != 0) 
                    {
                        StartCoroutine(StartChooseCD());
                        if (ingredientChosedPerPos[3] == checkedChefIngredient)
                        {
                            PlayerPickedCorrect(ref checkedPlayerIngredient);
                        }
                        else
                        {
                            PlayerLoose();
                        }

                    }
                }
            }
            void DestroyChosedIngredients()
            {
                foreach (GameObject ingredients in ingredientChosingPhase)
                {
                    Destroy(ingredients);
                }
            }

            IEnumerator StartChooseCD()
            {
                canChoose = false;
                yield return new WaitForSeconds(timeBetweenSteps);
                canChoose = true;
            }

            void PlayerLoose()
            {
                DestroyChosedIngredients();
                animatorBoutons.SetBool("EndGame", true);
                loose = true;
                Destroy(currentSelectionBackGround);
                InstantiateChefIngredients();
                sadFace.SetActive(true);
                looseScreen.SetActive(true);
            }
            void PlayerPickedCorrect(ref bool checkedIngredient)
            {
                checkedIngredient = true;
                ingredientChosedPerPos.Clear();
                DestroyChosedIngredients();
                launchPlaceRandomIngredient = true;
            }
            void InstantiateChefIngredients()
            {
                if (difficulty == 2)
                {
                    chefBread = Instantiate(hardBread[chefBreadInt], chefBreadPos, Quaternion.identity);
                    chefBreadBase = Instantiate(hardBreadBase, chefBreadBasePos, Quaternion.identity);
                    chefMeat = Instantiate(hardMeat[chefMeatInt], chefMeatPos, Quaternion.identity);
                    chefVegetable = Instantiate(hardVegetable[chefVegetableInt], chefVegetablePos, Quaternion.identity);
                }
                else
                {
                    chefBread = Instantiate(bread[chefBreadInt], chefBreadPos, Quaternion.identity);
                    chefBreadBase = Instantiate(breadBase[chefBreadInt], chefBreadBasePos, Quaternion.identity);
                    chefMeat = Instantiate(meat[chefMeatInt], chefMeatPos, Quaternion.identity);
                    chefVegetable = Instantiate(vegetable[chefVegetableInt], chefVegetablePos, Quaternion.identity);
                }
            }
        }

    }
}

