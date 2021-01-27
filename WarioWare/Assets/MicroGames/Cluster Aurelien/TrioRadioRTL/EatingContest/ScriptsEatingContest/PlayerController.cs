using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using System.Linq;


/// <summary>
/// DEUTSCHMANN Lucas
/// </summary>

namespace RadioRTL
{
    namespace EatingContest
    {
        public class PlayerController : MonoBehaviour
        {

            [Header ("Parameters")]
            public int chomp; //the number of chomps the player has currently had
            public int numberOfChomps; //the number of chomps required to finish a plate
            public int numberOfPlates; //the number of plates the player need to eat to finish the mini game
            public int totalPlates;
            int basePlates;
            public int numberOfRottenPlates; //the number of rotten plates the player will need to avoid

            public bool rottenPlate = false; //the state of the current plate
            [Header("Difficulty")]
            public int rottenPlatesEasy;
            public int rottenPlatesMedium;
            public int rottenPlatesHard;
            public int numberOfPlatesEasy;
            public int numberOfPlatesMedium;
            public int numberOfPlatesHard;

            [Header("Sprites")]
            SpriteRenderer platesFullStackDisplay;
            public Sprite platesFull5;
            public Sprite platesFull4;
            public Sprite platesFull3;
            public Sprite platesFull2;
            public Sprite platesFull1;
            public Sprite platesFull5Dirty;
            public Sprite platesFull4Dirty;
            public Sprite platesFull3Dirty;
            public Sprite platesFull2Dirty;
            public Sprite platesFull1Dirty;

            public GameObject sparkParticles;

            public SpriteRenderer platesEmptyStackDisplay;

            public Sprite platesEmpty5;
            public Sprite platesEmpty4;
            public Sprite platesEmpty3;
            public Sprite platesEmpty2;
            public Sprite platesEmpty1;

            public List<bool> platesQueue = new List<bool>();

            public Transform target;
            Vector3 basePosition;
            Vector3 moveVector;
            [HideInInspector]
            public bool win;
            public float speed;
            public bool movePlate = true;
            public GameObject platesManager;
            int i = 0;
            bool hasLost;

            public GameObject fireworks;
            public GameObject tears;
            bool hasEnded;
            public bool isEating;
            public int foodtype;
            // Start is called before the first frame update
            void Start()
            {
                
                platesFullStackDisplay = gameObject.GetComponent<SpriteRenderer>();

                basePosition = transform.position;

                platesManager.transform.position = basePosition;
                moveVector = (target.position - transform.position).normalized;

                if (Manager.Instance.currentDifficulty == Difficulty.EASY)
                {
                    numberOfPlates = numberOfPlatesEasy;
                    numberOfRottenPlates = rottenPlatesEasy;
                }
                else if (Manager.Instance.currentDifficulty == Difficulty.MEDIUM)
                {
                    numberOfPlates = numberOfPlatesMedium;
                    numberOfRottenPlates = rottenPlatesMedium;
                }
                else if (Manager.Instance.currentDifficulty == Difficulty.HARD)
                {
                    numberOfPlates = numberOfPlatesHard;
                    numberOfRottenPlates = rottenPlatesHard;
                }
                basePlates = numberOfRottenPlates + numberOfPlates;
                PopulateQueue();
                NextPlate();
            }

            // Update is called once per frame
            void Update()
            {
                totalPlates = numberOfPlates + numberOfRottenPlates;

                if (!hasEnded)
                {
                    if (totalPlates > 0)
                    {
                        if (movePlate)
                        {

                            platesManager.transform.position += Vector3.right * speed * Time.deltaTime;

                            if (platesManager.transform.position.x >= 0)
                            {
                                if (rottenPlate)
                                {
                                    sparkParticles.SetActive(true);
                                }
                                
                                
                                movePlate = false;
                            }
                            else
                            {
                                sparkParticles.SetActive(false);
                            }
                        }
                        
                        if (!rottenPlate) //plate isn't rotten
                        {
                            if (Input.GetButtonDown("A_Button"))
                            {
                                chomp += 1;
                                FindObjectOfType<AudioManagerLucas>().Play("Eat", 0);
                                StartCoroutine(EatAnim());
                            }
                           
                            if (chomp == numberOfChomps && numberOfPlates != 0) //if the player eats everything in the plate
                            {
                                chomp = 0;
                                numberOfPlates -= 1;
                                FindObjectOfType<AudioManagerLucas>().Play("Burp", 0);
                                NextPlate(); //Changing Plates
                            }
                            if (Input.GetButtonDown("B_Button"))
                            {

                                chomp = 0;
                                platesQueue.Insert(0, true);

                                NextPlate();
                                hasLost = true;
                                EndMinigame();
                            }
                        }
                        else if (rottenPlate)//plate is rotten
                        {
                            if (Input.GetButtonDown("B_Button"))
                            {
                                FindObjectOfType<AudioManagerLucas>().Play("Yarr", 0);
                                numberOfRottenPlates -= 1;
                                NextPlate();

                            }
                            if (Input.GetButtonDown("A_Button"))
                            {
                                
                                win = false;
                                hasLost = true;
                                EndMinigame();

                            }
                        }
                    }
                }
                
                

                if (totalPlates == 0) //if there are no more plates the player wins
                {
                    platesManager.GetComponent<SpriteRenderer>().enabled = false;
                    
                    EndMinigame();
                }
                if (platesQueue.Count>0)
                {
                    if (platesQueue.First())
                    {
                        switch (totalPlates - 1)
                        {
                            case 0:
                                platesFullStackDisplay.sprite = null;
                                break;
                            case 1:
                                platesFullStackDisplay.sprite = platesFull1;
                                break;
                            case 2:
                                platesFullStackDisplay.sprite = platesFull2;
                                break;
                            case 3:
                                platesFullStackDisplay.sprite = platesFull3;
                                break;
                            case 4:
                                platesFullStackDisplay.sprite = platesFull4;
                                break;
                            case 5:
                                platesFullStackDisplay.sprite = platesFull5;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (!platesQueue.First())
                    {
                        switch (totalPlates - 1)
                        {
                            case 0:
                                platesFullStackDisplay.sprite = null;
                                break;
                            case 1:
                                platesFullStackDisplay.sprite = platesFull1Dirty;
                                break;
                            case 2:
                                platesFullStackDisplay.sprite = platesFull2Dirty;
                                break;
                            case 3:
                                platesFullStackDisplay.sprite = platesFull3Dirty;
                                break;
                            case 4:
                                platesFullStackDisplay.sprite = platesFull4Dirty;
                                break;
                            case 5:
                                platesFullStackDisplay.sprite = platesFull5Dirty;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    platesFullStackDisplay.sprite = null;
                }

                switch (basePlates-(totalPlates))
                {
                    case 0:
                        platesEmptyStackDisplay.sprite = null;
                        break;
                    case 1:
                        platesEmptyStackDisplay.sprite = platesEmpty1;
                        break;                                
                    case 2:
                        platesEmptyStackDisplay.sprite = platesEmpty2;
                        break;                                
                    case 3:
                        platesEmptyStackDisplay.sprite = platesEmpty3;
                        break;                                
                    case 4:
                        platesEmptyStackDisplay.sprite = platesEmpty4;
                        break;                               
                    case 5:
                        platesEmptyStackDisplay.sprite = platesEmpty5;
                        break;
                    default:
                        break;
                }
            }

            IEnumerator EatAnim()
            {
                foodtype += 1;
                if (foodtype > 3)
                {
                    foodtype = foodtype - 3;
                }
                isEating = true;
                yield return new WaitForSeconds(.2f);
                isEating = false;
            }
            void PrintQueue()
            {
                string result = "queue : ";
                foreach (var item in platesQueue)
                {
                    result += item + " |";
                }
                
            }
            void NextPlate()//changing the plate once its empty
            {
                //PrintQueue();
                FindObjectOfType<AudioManagerLucas>().Play("Swap", 1);
                platesManager.transform.position = basePosition;


                movePlate = true;
                if (platesQueue.Count == 0)
                    return;


                rottenPlate = !platesQueue.First();

                //numberOfRottenPlates -= (rottenPlate == true ? 1 : 0);
                platesQueue.RemoveAt(0);

                //if (Random.Range(0,numberOfRottenPlates) != 0)
                //{
                //    rottenPlate = true;
                //    numberOfRottenPlates -= 1;
                //}
                //else
                //{
                //    rottenPlate = false;
                //}
                
            }

            void PopulateQueue()
            {
                System.Random rnd = new System.Random();

                for (int i = 0; i < numberOfRottenPlates; i++)
                     platesQueue.Add(false);
                for (int i = 0; i < numberOfPlates; i++)
                    platesQueue.Add(true);

                platesQueue = platesQueue.OrderBy<bool, int>((plate) => rnd.Next()).ToList();

            }




            void EndMinigame()//ending the mini game
            {
                hasEnded = true;
                if (hasLost)
                {
                    win = false;
                    FindObjectOfType<AudioManagerLucas>().Play("Lose", 1);
                    tears.SetActive(true);
                }
                else
                {
                    if (i == 0)
                    {
                        fireworks.SetActive(true);
                        FindObjectOfType<AudioManagerLucas>().Play("Win", 0);
                        i += 1;
                    }

                    win = true;
                }
                
                
            }
        }
    }
}
