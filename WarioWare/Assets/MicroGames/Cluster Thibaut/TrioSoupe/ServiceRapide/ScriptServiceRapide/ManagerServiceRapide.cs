using System.Collections;
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
            AudioSource source;
            public AudioClip pickedGreatIngredient;
            public AudioClip pickedWrongIngredient;

            int chefMeatInt;
            int chefBreadInt;
            int chefVegetableInt;

            public GameObject memoryBackground;
            public GameObject selectionBackgroundD1;
            public GameObject selectionBackgroundD0;
            public GameObject selectionBackgroundEasy;
            GameObject selectionBackground;
            GameObject currentSelectionBackGround;
            public GameObject victoryScreen;
            public GameObject looseScreen;

            public GameObject sadFace;
            public GameObject HappyFace;
            public GameObject sadFaceD0;
            public GameObject HappyFaceD0;

            [Range(1,2)] public int moreIngredientsDifficultyIncrease = 1;
            [Range(2, 3)] public int baseIngredientNumber = 2;
            [Range(1, 2)] public int moreChoiceDifficultyIncrease = 2;
            [Range(2, 3)] public int baseChoiceNumber = 3;
            GameObject chefBread;
            GameObject chefMeat;
            GameObject chefVegetable;
            GameObject chefBreadBase;
            public GameObject[] meat;
            public GameObject[] bread;
            public GameObject[] breadBase;
            public GameObject[] vegetable;
            public Vector3[] ingredientScreenPositions;
            List<int> ingredientChosedPerPos;

            public Vector3 chefBreadPos;
            public Vector3 EasyChefBreadPos;
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
                animatorBoutons.SetBool("EasyMode", baseChoiceNumber != 3);
                if (difficulty < moreChoiceDifficultyIncrease)
                {
                    chefMeatInt = Random.Range(0, baseChoiceNumber);
                    chefBreadInt = Random.Range(0, baseChoiceNumber);
                    chefVegetableInt = Random.Range(0, baseChoiceNumber);
                    if(baseChoiceNumber == 2)
                    {
                        selectionBackground = selectionBackgroundEasy;
                    }
                    else
                    {
                        selectionBackground = selectionBackgroundD0;
                    }
                    animatorBoutons.SetInteger("Difficulty", 0);
                }
                else
                {
                    chefMeatInt = Random.Range(0, baseChoiceNumber + 1);
                    chefBreadInt = Random.Range(0, baseChoiceNumber + 1);
                    chefVegetableInt = Random.Range(0, baseChoiceNumber + 1);
                    if (baseChoiceNumber == 2)
                    {
                        selectionBackground = selectionBackgroundD0;
                    }
                    else
                    {
                        selectionBackground = selectionBackgroundD1;
                    }
                    animatorBoutons.SetInteger("Difficulty", 1);
                }

                InstantiateChefIngredients();
                
                source = GetComponent<AudioSource>();

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
                        if (difficulty >= moreIngredientsDifficultyIncrease)
                        {
                            RandomPlaceIngredient(vegetable);
                            PlayerChose(chefVegetableInt, ref playerChosedVegetable);
                        }
                        else
                        {
                            playerChosedVegetable = true;
                        }

                    }
                    if (playerChosedMeat == false && playerChosedVegetable == true)
                    {

                            RandomPlaceIngredient(meat);
                        
                        PlayerChose(chefMeatInt, ref playerChosedMeat);

                        if(playerChosedMeat == true)
                        {
                            animatorBoutons.SetBool("EndGame", true);
                            victoryScreen.SetActive(true);
                            InstantiateChefIngredients();
                            if(!(difficulty >= moreIngredientsDifficultyIncrease))
                            {
                                HappyFaceD0.SetActive(true);
                            }
                            else
                            {
                                HappyFace.SetActive(true);
                            }
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
                if (difficulty >= moreIngredientsDifficultyIncrease)
                {
                    Destroy(chefVegetable);
                }
                Destroy(chefBreadBase);
                RandomPlaceIngredient(bread);
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
                    if(difficulty < moreChoiceDifficultyIncrease)
                    {
                        maxi = baseChoiceNumber;
                    }
                    else
                    {
                        maxi = baseChoiceNumber + 1;
                    }
                   
                    for (int i = 0; i < maxi; i++)
                    {
                        bool isIngredientAlreadyIn;
                        int randomIngredient;
                        if (i == 0)
                        {
                            if(difficulty < moreChoiceDifficultyIncrease)
                            {
                                randomIngredient = Random.Range(0, baseChoiceNumber);
                                
                            }
                            else
                            {
                                randomIngredient = Random.Range(0, baseChoiceNumber + 1);
                            }
                            ingredientChosingPhase[i] = Instantiate(ingredient[randomIngredient], ingredientScreenPositions[i], Quaternion.identity);
                            ingredientChosedPerPos.Add(randomIngredient);
                        }
                        else
                        {
                            do
                            {
                                isIngredientAlreadyIn = false;
                                if (difficulty < moreChoiceDifficultyIncrease)
                                {
                                    randomIngredient = Random.Range(0, baseChoiceNumber);
                                }
                                else
                                {
                                    randomIngredient = Random.Range(0, baseChoiceNumber + 1);
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
                    if (Input.GetButtonDown("B_Button"))
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
                    if (Input.GetButtonDown("Y_Button"))
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
                source.PlayOneShot(pickedWrongIngredient);
                DestroyChosedIngredients();
                animatorBoutons.SetBool("EndGame", true);
                loose = true;
                Destroy(currentSelectionBackGround);
                InstantiateChefIngredients();
                if(difficulty == 0)
                {
                    sadFaceD0.SetActive(true);
                }
                else
                {
                    sadFace.SetActive(true);
                }
                looseScreen.SetActive(true);
            }
            void PlayerPickedCorrect(ref bool checkedIngredient)
            {
                checkedIngredient = true;
                ingredientChosedPerPos.Clear();
                source.PlayOneShot(pickedGreatIngredient);
                DestroyChosedIngredients();
                launchPlaceRandomIngredient = true;
            }
            void InstantiateChefIngredients()
            {
                if(difficulty == 0)
                {
                    chefBread = Instantiate(bread[chefBreadInt], EasyChefBreadPos, Quaternion.identity);
                }
                else
                {
                    chefBread = Instantiate(bread[chefBreadInt], chefBreadPos, Quaternion.identity);
                    chefVegetable = Instantiate(vegetable[chefVegetableInt], chefVegetablePos, Quaternion.identity);
                }
                    chefBreadBase = Instantiate(breadBase[chefBreadInt], chefBreadBasePos, Quaternion.identity);
                    chefMeat = Instantiate(meat[chefMeatInt], chefMeatPos, Quaternion.identity);
                }
            }
        }

    }



