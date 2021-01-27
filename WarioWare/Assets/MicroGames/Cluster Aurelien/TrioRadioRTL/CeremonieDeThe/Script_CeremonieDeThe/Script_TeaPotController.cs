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

        public class Script_TeaPotController : TimedBehaviour
        {

            //1- Initialisation des variables
            private Rigidbody2D teaPotRigidBody;
            public int teaPotSpeed;
            public Vector2 mouvementTeaPot;
            float moveHorizontal;
            float moveVertical;
            float teaPotBoundary;

            //2- Récupération du component et positionement du Tea-Pot
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                teaPotRigidBody = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;

                switch (currentDifficulty)
                {

                    case Difficulty.EASY:

                        teaPotBoundary = 1f;

                        break;

                    case Difficulty.MEDIUM:

                        teaPotBoundary = 1f;

                        break;

                    case Difficulty.HARD:

                        teaPotBoundary = 3f;

                        break;

                }

            }

            //3- Déplacement du Teapot
            private void Update()
            {

                base.FixedUpdate(); //Do not erase this line!
                moveHorizontal = Input.GetAxis("Left_Joystick_X");

                moveVertical = Input.GetAxis("Left_Joystick_Y");

                if (moveHorizontal > 0.15f || moveVertical > 0.15f || moveHorizontal < -0.15f || moveVertical < -0.15f) //Pour éviter les micormouvement parasite
                {

                    mouvementTeaPot = new Vector2(moveHorizontal, moveVertical);
                    mouvementTeaPot.Normalize();

                    teaPotRigidBody.AddForce(mouvementTeaPot * teaPotSpeed * Time.deltaTime);

                }
                else //pour éviter que le Tea-Pot continue de bouger si il y a 0 input
                {
                    mouvementTeaPot = new Vector2(0.0f, 0.0f);

                    teaPotRigidBody.AddForce(mouvementTeaPot * teaPotSpeed * Time.deltaTime);
                }

            }

            //4- FixedUpdate is called on a fixed time & déplacement du Tea-Pot
            public override void FixedUpdate()
            {
                
                if (transform.position.y < teaPotBoundary)
                {

                    transform.position = new Vector2(transform.position.x, teaPotBoundary);

                    teaPotRigidBody.velocity = new Vector2(teaPotRigidBody.velocity.x, 0);

                }

            }
        }
    }
}