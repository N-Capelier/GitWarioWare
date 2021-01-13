using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace ChasseAuTresor
    {
        /// <summary>
        /// Noé Blanc
        /// </summary>

        public class InputsChoiceScript : TimedBehaviour
        {
            
            private int possibilityNumber;

            private Vector3[] inputLocationList = { new Vector3(-3.7f, 0, 0), new Vector3(0, 0, 0), new Vector3(3.7f, 0, 0), new Vector3(-5, 0, 0), new Vector3(-2.5f, 0, 0), new Vector3(2.5f, 0, 0), new Vector3(5, 0, 0), new Vector3(-7.4f, 0, 0), new Vector3(-3.7f, 0, 0), new Vector3(0, 0, 0), new Vector3(3.7f, 0, 0), new Vector3(7.4f, 0, 0) };           

            private List<GameObject> inputInstantiated = new List<GameObject>();

            public GameObject upArrow;
            public GameObject downArrow;
            public GameObject leftArrow;
            public GameObject rightArrow;

            public GameObject player;
            public GameObject treasure;
            public GameObject mouetteGauche;
            public GameObject mouetteDroite;

            private int inputNumber;

            public float distance;            

            public override void Start()
            {
                base.Start(); //Do not erase this line!              



                distance = player.GetComponent<PlayerTimedController>().distance;

                player.SetActive(false);

                mouetteGauche.SetActive(false);
                mouetteDroite.SetActive(false);

                treasure.transform.position = new Vector3(0, 0, 0);

                if(currentDifficulty == Difficulty.EASY)
                {
                    inputNumber = 3;
                }
                else if(currentDifficulty == Difficulty.MEDIUM)
                {
                    inputNumber = 4;
                }
                else if(currentDifficulty == Difficulty.HARD)
                {
                    inputNumber = 5;
                }
                
                for (int i = 0; i < inputNumber; i++)
                {
                    possibilityNumber = Random.Range(1, 5);

                    switch (possibilityNumber)
                    {
                        case 1:
                            if (i == 0)
                            {
                                if(currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input1 = Instantiate(upArrow, inputLocationList[0], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input1 = Instantiate(upArrow, inputLocationList[3], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input1 = Instantiate(upArrow, inputLocationList[7], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }

                            }
                            if (i == 1)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input2 = Instantiate(upArrow, inputLocationList[1], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input2 = Instantiate(upArrow, inputLocationList[4], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input2 = Instantiate(upArrow, inputLocationList[8], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                            }
                            if (i == 2)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input3 = Instantiate(upArrow, inputLocationList[2], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input3 = Instantiate(upArrow, inputLocationList[5], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input3 = Instantiate(upArrow, inputLocationList[9], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                            }
                            if (i == 3)
                            {                                    
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input4 = Instantiate(upArrow, inputLocationList[6], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input4 = Instantiate(upArrow, inputLocationList[10], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                            }
                            if (i == 4)
                            {                       
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input5 = Instantiate(upArrow, inputLocationList[11], Quaternion.identity);
                                    inputInstantiated.Add(input5);
                                }
                            }
                            
                            treasure.transform.position += new Vector3(0, distance, 0);
                            break;
                        case 2:
                            if (i == 0)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input1 = Instantiate(downArrow, inputLocationList[0], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input1 = Instantiate(downArrow, inputLocationList[3], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input1 = Instantiate(downArrow, inputLocationList[7], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                            }
                            if (i == 1)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input2 = Instantiate(downArrow, inputLocationList[1], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input2 = Instantiate(downArrow, inputLocationList[4], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input2 = Instantiate(downArrow, inputLocationList[8], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                            }
                            if (i == 2)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input3 = Instantiate(downArrow, inputLocationList[2], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input3 = Instantiate(downArrow, inputLocationList[5], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input3 = Instantiate(downArrow, inputLocationList[9], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                            }
                            if (i == 3)
                            {                                    
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input4 = Instantiate(downArrow, inputLocationList[6], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input4 = Instantiate(downArrow, inputLocationList[10], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                            }
                            if (i == 4)
                            {
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input5 = Instantiate(downArrow, inputLocationList[11], Quaternion.identity);
                                    inputInstantiated.Add(input5);
                                }
                            }
                            
                            treasure.transform.position += new Vector3(0, -distance, 0);
                            break;
                        case 3:
                            if (i == 0)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input1 = Instantiate(leftArrow, inputLocationList[0], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input1 = Instantiate(leftArrow, inputLocationList[3], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input1 = Instantiate(leftArrow, inputLocationList[7], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                            }
                            if (i == 1)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input2 = Instantiate(leftArrow, inputLocationList[1], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input2 = Instantiate(leftArrow, inputLocationList[4], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input2 = Instantiate(leftArrow, inputLocationList[8], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                            }
                            if (i == 2)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input3 = Instantiate(leftArrow, inputLocationList[2], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input3 = Instantiate(leftArrow, inputLocationList[5], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input3 = Instantiate(leftArrow, inputLocationList[9], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                            }
                            if (i == 3)
                            {
                                if (currentDifficulty ==Difficulty.MEDIUM)
                                {
                                    GameObject input4 = Instantiate(leftArrow, inputLocationList[6], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input4 = Instantiate(leftArrow, inputLocationList[10], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                            }
                            if (i == 4)
                            {
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input5 = Instantiate(leftArrow, inputLocationList[11], Quaternion.identity);
                                    inputInstantiated.Add(input5);
                                }
                            }
                            
                            treasure.transform.position += new Vector3(-distance, 0, 0);
                            break;
                        case 4:
                            if (i == 0)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input1 = Instantiate(rightArrow, inputLocationList[0], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input1 = Instantiate(rightArrow, inputLocationList[3], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input1 = Instantiate(rightArrow, inputLocationList[7], Quaternion.identity);
                                    inputInstantiated.Add(input1);
                                }
                            }
                            if (i == 1)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input2 = Instantiate(rightArrow, inputLocationList[1], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input2 = Instantiate(rightArrow, inputLocationList[4], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input2 = Instantiate(rightArrow, inputLocationList[8], Quaternion.identity);
                                    inputInstantiated.Add(input2);
                                }
                            }
                            if (i == 2)
                            {
                                if (currentDifficulty == Difficulty.EASY)
                                {
                                    GameObject input3 = Instantiate(rightArrow, inputLocationList[2], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input3 = Instantiate(rightArrow, inputLocationList[5], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input3 = Instantiate(rightArrow, inputLocationList[9], Quaternion.identity);
                                    inputInstantiated.Add(input3);
                                }
                            }
                            if (i == 3)
                            {                                    
                                if (currentDifficulty == Difficulty.MEDIUM)
                                {
                                    GameObject input4 = Instantiate(rightArrow, inputLocationList[6], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input4 = Instantiate(rightArrow, inputLocationList[10], Quaternion.identity);
                                    inputInstantiated.Add(input4);
                                }
                            }
                            if (i == 4)
                            {
                                if (currentDifficulty == Difficulty.HARD)
                                {
                                    GameObject input5 = Instantiate(rightArrow, inputLocationList[11], Quaternion.identity);
                                    inputInstantiated.Add(input5);
                                }
                            }
                            
                            treasure.transform.position += new Vector3(distance, 0, 0);
                            break;
                    }
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
                if(Tick == 3)
                {
                    for(int i = 0; i< inputInstantiated.Count; i++)
                    {
                        Destroy(inputInstantiated[i]);
                        
                    }
                    player.SetActive(true);
                    mouetteGauche.SetActive(true);
                    mouetteDroite.SetActive(true);
                }
            }
            
        }
    }
}