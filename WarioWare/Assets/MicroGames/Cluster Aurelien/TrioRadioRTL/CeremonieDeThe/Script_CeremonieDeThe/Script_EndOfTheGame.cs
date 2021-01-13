using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace RadioRTL
{
    namespace CeremonieDeThe
    {

        /// <summary>
        /// Riwan HERVOUET
        /// </summary>

        public class Script_EndOfTheGame : TimedBehaviour
        {

            //1- Variables
            //1.1- Game Object & Collider
            public Script_PouredTea teaCup;
            public Collider2D teaPotCollider;


            //1.2- Booléennes & int
            bool cupIsFull;
            int cupQuantity;
			public int waterToVictory;

            //2- Récupération des donné
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                switch (currentDifficulty)
                {

                    case (Difficulty.EASY):

                        waterToVictory -= 0;

                        break;

                    case (Difficulty.MEDIUM):

                        waterToVictory -= 6;

                        break;

                    case (Difficulty.HARD):

                        waterToVictory -= 9;

                        break;

                }//60 90 120 140

                switch (bpm)
                {

                    case (60):
                        waterToVictory = 111;
                        break;

                    case (90):
                        waterToVictory = 93;
                        break;

                    case (120):
                        waterToVictory = 72;
                        break;

                    case (140):
                        waterToVictory = 51;
                        break;

                }

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //3- TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

                //Debug.Log(Tick);

                //3.1- A la fin de la 8 tick
                if (Tick == 8)
                {
                    Debug.LogError("end");

                    cupQuantity = teaCup.pouredTea;

                    Debug.Log(cupQuantity);

                    //3.1.2- Verfication des conditions
                    if ( cupQuantity >= waterToVictory)
                    {

                        Manager.Instance.Result(true);
                        Debug.LogError("victoire");

                    }else
                    {

                        Manager.Instance.Result(false);
                        Debug.LogError("defaite");
                    }

                }

            }
        }
    }
}