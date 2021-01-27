using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RadioRTL
{
    namespace EatingContest
    {
        public class EatAnimation : MonoBehaviour
        {
            public Sprite pirateBase;
            public Sprite pirateEat;
            public bool isEating;
            public PlayerController playerController;
            int foodType;
            public float speed;

            public GameObject burger;
            public GameObject carot;
            public GameObject pizza;
            public Transform mouth;
            Vector3 baseBurgerPos;
            Vector3 basePizzaPos;
            Vector3 baseCarotPos;
            Vector3 burgerToMouth;
            Vector3 pizzaToMouth;
            Vector3 carotToMouth;
            // Start is called before the first frame update
            void Start()
            {
                baseBurgerPos = burger.transform.position;
                basePizzaPos = pizza.transform.position;
                baseCarotPos = carot.transform.position;
                burgerToMouth = mouth.transform.position - baseBurgerPos;
                pizzaToMouth = mouth.transform.position - basePizzaPos;
                carotToMouth = mouth.transform.position - basePizzaPos;
            }

            // Update is called once per frame
            void Update()
            {
                foodType = playerController.foodtype;
                
                isEating = playerController.isEating;
                if (isEating)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = pirateEat;
                    switch (foodType)
                    {
                        case 1:
                            if (burger.transform.position != mouth.transform.position)
                            {
                                burger.GetComponent<SpriteRenderer>().enabled = true;
                                burger.transform.position += burgerToMouth * speed * Time.deltaTime;
                            }
                            else
                            {
                                burger.GetComponent<SpriteRenderer>().enabled = false;
                                burger.transform.position = baseBurgerPos;
                            }
                            break;
                        case 2:
                            if (pizza.transform.position != mouth.transform.position)
                            {
                                pizza.GetComponent<SpriteRenderer>().enabled = true;
                                pizza.transform.position += pizzaToMouth * speed * Time.deltaTime;
                            }
                            else
                            {
                                pizza.GetComponent<SpriteRenderer>().enabled = false;
                                pizza.transform.position = basePizzaPos;
                            }
                            break;
                        case 3:
                            if (carot.transform.position != mouth.transform.position)
                            {
                                carot.GetComponent<SpriteRenderer>().enabled = true;
                                carot.transform.position += carotToMouth * speed * Time.deltaTime;
                            }
                            else
                            {
                                carot.GetComponent<SpriteRenderer>().enabled = false;
                                carot.transform.position = baseCarotPos;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = pirateBase;
                    carot.GetComponent<SpriteRenderer>().enabled = false;
                    pizza.GetComponent<SpriteRenderer>().enabled = false;
                    burger.GetComponent<SpriteRenderer>().enabled = false;
                    burger.transform.position = baseBurgerPos;
                    pizza.transform.position = basePizzaPos;
                    carot.transform.position = baseCarotPos;
                }

                
            }
        }
    }
}

