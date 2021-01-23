using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioLLL
{
    namespace Pintade
    {
        /// <summary>
        /// VIDAL Luc
        /// </summary>
        public class AnimalBehaviour : TimedBehaviour
        {
            private bool jumped;
            public GameObject leafParticles;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (jumped == false)
                {
                    jumped = true;

                    Instantiate(leafParticles, transform);
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

            public void LeafPop()
            {
                Instantiate(leafParticles, transform);
            }
        }
    }
}