using UnityEngine;
using Caps;

namespace LeRafiot
{
	namespace Choppe
	{
		/// <summary>
		/// Antoine LEROUX
		/// This script is use to detect drink in catch zone and by pressing A, catch the drink
		/// </summary>

		public class CatchSystem : TimedBehaviour
        {
			#region Variables
			public Color colorTriggered;
			private Color colorBase;

			private bool drinkInZone;
			private GameObject drinkTriggered;
            public bool goodDrink;

            public Transform drinkCatch;
            private GameObject drinkInHand;

            [Header("Arms")]
            public GameObject armDown;
            public GameObject armUp;

            [Header("Win & Lose Sprites")]
            public GameObject winScreen;
            public GameObject loseScreen;
            public GameObject pirate;
            public GameObject pirateWin;
            public GameObject pirateLose;
            public GameObject fondLvl1;
            public GameObject fondLvl2;
            public GameObject fondLvl3;

            [Header("Sous verre")]
            public GameObject sousVerre;
            public Color colorSousVerreTriggered;

            [Header("Button")]


            [HideInInspector] public bool canCatch = true;
            [HideInInspector] public bool catchedGoodDrink;
            [HideInInspector] public bool catchedBadDrink;
            [HideInInspector] public bool drinkExit;

            private int tickCounter;
            private bool cantCount;
            #endregion

            // Start is called before the first frame update
            public override void Start()
			{
                base.Start();
                colorBase = GetComponent<SpriteRenderer>().color;
				drinkInZone = false;

                armDown.SetActive(true);
                armUp.SetActive(false);

                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                pirateWin.SetActive(false);
                pirateLose.SetActive(false);

                sousVerre.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

            public override void TimedUpdate()
            {
                if (Tick == 8 && !Manager.Instance.panel.activeSelf)
                {
                    if (catchedGoodDrink)
                    {
                        Manager.Instance.Result(true);
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                    }
                }

                if (!canCatch && !cantCount)
                {
                    tickCounter++;
                }

                if(tickCounter == 1 && !cantCount)
                {
                    cantCount = true;

                    if (catchedGoodDrink)
                    {
                        winScreen.SetActive(true);
                        pirateWin.SetActive(true);

                        fondLvl1.SetActive(false);
                        fondLvl2.SetActive(false);
                        fondLvl3.SetActive(false);
                        pirate.SetActive(false);
                        armDown.SetActive(false);
                        armUp.SetActive(false);

                        SoundManagerChoppe.Instance.sfxSound[4].Play();
                        SoundManagerChoppe.Instance.sfxSound[0].Play();
                    }
                    else if (catchedBadDrink || drinkExit)
                    {
                        loseScreen.SetActive(true);
                        pirateLose.SetActive(true);

                        fondLvl1.SetActive(false);
                        fondLvl2.SetActive(false);
                        fondLvl3.SetActive(false);
                        pirate.SetActive(false);
                        armDown.SetActive(false);
                        armUp.SetActive(false);

                        SoundManagerChoppe.Instance.sfxSound[5].Play();
                        SoundManagerChoppe.Instance.sfxSound[1].Play();
                    }
                }
            }

            // Update is called once per frame
            void Update()
			{
                if (canCatch == true)
                {
                    if (drinkInZone)
                    {
					    GetComponent<SpriteRenderer>().color = colorTriggered;
                        sousVerre.GetComponent<SpriteRenderer>().color = colorSousVerreTriggered;

                        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button") && !Manager.Instance.panel.activeSelf)
                        {
                            armDown.SetActive(false);
                            armUp.SetActive(true);

                            if (goodDrink)
                            {
                                catchedGoodDrink = true;
                                canCatch = false;
                                DrinkManager.Instance.canSpawn = false;
                                
                                drinkInHand = Instantiate(drinkTriggered, drinkCatch);
                                drinkInHand.GetComponent<BoxCollider2D>().enabled = false;
                                Destroy(drinkTriggered);


                                //Manager.Instance.Result(true);
                                //SoundManagerChoppe.Instance.sfxSound[4].Play();
                                //SoundManagerChoppe.Instance.sfxSound[0].Play();
                            }
                            else
                            {
                                catchedBadDrink = true;
                                catchedGoodDrink = false;
                                canCatch = false;
                                DrinkManager.Instance.canSpawn = false;

                                drinkInHand = Instantiate(drinkTriggered, drinkCatch);
                                drinkInHand.GetComponent<BoxCollider2D>().enabled = false;
                                Destroy(drinkTriggered);


                                //Manager.Instance.Result(false);
                                //SoundManagerChoppe.Instance.sfxSound[5].Play();
                                //SoundManagerChoppe.Instance.sfxSound[1].Play();
                            }
					    }
				    }

                    else
                    {
					    GetComponent<SpriteRenderer>().color = colorBase;
                        sousVerre.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                    }
                }
			}

            private void OnTriggerEnter2D(Collider2D col)
            {
                if (col.CompareTag("Enemy2"))
                {
                    goodDrink = true;
                    drinkTriggered = col.gameObject;
                    drinkInZone = true;
                }

                if (col.CompareTag("Enemy1"))
                {
                    drinkInZone = true;
                    drinkTriggered = col.gameObject;
                }
            }

            private void OnTriggerExit2D(Collider2D col)
            {
                if (col.CompareTag("Enemy1"))
                {
					drinkInZone = false;					
				}

                if (col.CompareTag("Enemy2"))
                {
                    goodDrink = false;
                    drinkInZone = false;
                }
            }
        }
	}
}