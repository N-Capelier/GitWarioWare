using System.Collections;
using System.Collections.Generic;
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

            [Header("Arms")]
            public GameObject armDown;
            public GameObject armUp;

            [HideInInspector] public bool canCatch = true;
            [HideInInspector] public bool catchedGoodDrink;
            #endregion

            // Start is called before the first frame update
            public override void Start()
			{
                base.Start();
                colorBase = GetComponent<SpriteRenderer>().color;
				drinkInZone = false;

                armDown.SetActive(true);
                armUp.SetActive(false);
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
            }

            // Update is called once per frame
            void Update()
			{
                if (canCatch == true)
                {

                    if (drinkInZone)
                    {
					    GetComponent<SpriteRenderer>().color = colorTriggered;
					
					    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_Button") && !Manager.Instance.panel.activeSelf)
                        {
                            armDown.SetActive(false);
                            armUp.SetActive(true);

                            if (goodDrink)
                            {
                                catchedGoodDrink = true;
                                canCatch = false;
                                DrinkManager.Instance.canSpawn = false;
                                Destroy(drinkTriggered);
                                //Manager.Instance.Result(true);
                                SoundManagerChoppe.Instance.sfxSound[4].Play();
                                SoundManagerChoppe.Instance.sfxSound[0].Play();
                            }
                            else
                            {
                                catchedGoodDrink = false;
                                canCatch = false;
                                DrinkManager.Instance.canSpawn = false;
                                Destroy(drinkTriggered);
                                //Manager.Instance.Result(false);
                                SoundManagerChoppe.Instance.sfxSound[5].Play();
                                SoundManagerChoppe.Instance.sfxSound[1].Play();
                            }
					    }
				    }

                    else
                    {
					    GetComponent<SpriteRenderer>().color = colorBase;
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
                }
            }
        }
	}
}