using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadioRTL
{
    namespace CeremonieDeThe
    {

        /// <summary>
        /// Riwan HERVOUET
        /// </summary>

        public class Script_PouredTea : TimedBehaviour
        {

            //1- Variables
            //1.1- Collider
            public Collider2D cupCollider;
            public Collider2D waterCollider;

            //1.2- Int
            public int pouredTea;
            int quantityToVictory;
            float quantityLevel;

            //1.3- Bool
            private bool isTouchingCup = false;

			//1.4- Les Sprites d'eau
			public SpriteRenderer firstTeaLevel;
			public SpriteRenderer secondTeaLevel;
			public SpriteRenderer thirdTeaLevel;

			//1.5- Le script pour gagner
			public GameObject endOfGameObject;

            //2- Récupération du component et positionement du Tea Cup
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                gameObject.transform.Translate(0.0f, 0.0f, 0.0f);

				//2.1- Desibling des sprites d'eau pour eviter tout bugs chelou
				this.firstTeaLevel.enabled = false;
				this.secondTeaLevel.enabled = false;
				this.thirdTeaLevel.enabled = false;

                //2.2- Récupération de la quantité d'eau necessaire pour gagné et ainsi pouvoir gérer les paliers
                Script_EndOfTheGame endOfGameScript = endOfGameObject.GetComponent<Script_EndOfTheGame>();
                quantityToVictory = endOfGameScript.waterToVictory;

                quantityLevel = quantityToVictory / 3;

            }

            //3- Verification des colliders
            //3.1- Verification on Enter
            private void OnTriggerEnter2D(Collider2D waterCollider)
            {

                isTouchingCup = true;
                pouredTea += 1;
                Destroy(waterCollider.gameObject);

            }

            //3.2- Verification on Enter
            /*private void OnTriggerExit2D(Collider2D waterCollider)
            {

                isTouchingCup = false;
                //Debug.Log(isTouchingCup);

            }*/

            //4- FixedUpdate is called on a fixed time
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                //4.1- Incrementation
                if (isTouchingCup == true)
                {

                    //pouredTea = pouredTea + 1;
                    //Debug.Log(pouredTea);

                    //4.1.1- Activation des niveaux d'eau
                    if (pouredTea > quantityLevel && pouredTea <= (quantityLevel * 2))
                    {

                        firstTeaLevel.enabled = true;

                    }
                    else if ( pouredTea > (quantityLevel*2) && pouredTea < quantityToVictory)
                    {

                        secondTeaLevel.enabled = true;

                    }
                    else if (quantityToVictory <= pouredTea)
                    {

                        thirdTeaLevel.enabled = true;

                    }

                }
                else
                {

                    pouredTea = pouredTea + 0;

                }
            }
        }
    }
}