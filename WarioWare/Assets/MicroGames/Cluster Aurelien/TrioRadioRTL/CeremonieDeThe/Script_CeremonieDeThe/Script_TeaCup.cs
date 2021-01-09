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

        public class Script_TeaCup : TimedBehaviour
        {

            //1- Création des variables
            private Rigidbody2D teaCupRigidbody;
            public float currentTeaCupSpeed;
            public Vector2 mouvementTeaCup;
            private bool isGoingLeft;
            public int mouvementTeaCupX;
            public float maxTeaCupSpeed;
            public float teaCupSpeedTimer;
            public float teaCupTimeSwing = 1.5f;

            //2- Récupération du component et positionement de la Tea Cup
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                gameObject.transform.Translate(0.0f, 0.0f, 0.0f);
                teaCupRigidbody = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;

                mouvementTeaCup = new Vector2(mouvementTeaCupX, 0);

            }

            //3- FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                //3.1- Un petit delay pour pas que ça commence directe
                if (Tick > 1)
                {

                    //3.1.1- Mouvement vers la gauche
                    if (isGoingLeft == true)
                    {

                        //teaCupRigidbody.velocity = new Vector2(0, 0);

                        teaCupSpeedTimer += Time.fixedDeltaTime *3f;

                        currentTeaCupSpeed = Mathf.Lerp(maxTeaCupSpeed, -maxTeaCupSpeed, teaCupSpeedTimer);
                        
                        if (teaCupSpeedTimer > teaCupTimeSwing)
                        {

                            isGoingLeft = false;

                            teaCupSpeedTimer = 0f;

                        }


                    }
                    //3.1.2- Mouvement vers la droite
                    else
                    {

                        //teaCupRigidbody.velocity = new Vector2(0, 0);


                        //mouvementTeaCup = new Vector2(mouvementTeaCupX, 0);

                        //teaCupRigidbody.AddForce(mouvementTeaCup * teaCupSpeed);

                        teaCupSpeedTimer += Time.fixedDeltaTime *3f;

                                                                                         //0 to 1 
                        currentTeaCupSpeed = Mathf.Lerp(-maxTeaCupSpeed, maxTeaCupSpeed, teaCupSpeedTimer);

                        if (teaCupSpeedTimer > teaCupTimeSwing)
                        {

                            isGoingLeft = true;

                            teaCupSpeedTimer = 0f;

                        }

                    }

                    Vector2 targetVector = mouvementTeaCup * currentTeaCupSpeed;
                    //Debug.Log("targetVector= " + targetVector);
                    //teaCupRigidbody.AddForce(targetVector , ForceMode2D.Force);
                    teaCupRigidbody.velocity = targetVector;

                }

            }

            //TimedUpdate is called once every tick (ici la vitesse varie en fonction des tics)
            public override void TimedUpdate()
            {
                
               

            }
        }
    }
}