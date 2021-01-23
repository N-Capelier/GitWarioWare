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
        public class ArrowInputBehaviour : TimedBehaviour
        {
            public SpriteRenderer sprite;
            [HideInInspector] public bool activated;
            private bool activationTriggered;
            public GameObject activationFx;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                activated = false;
                activationTriggered = false;

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (activated == true && activationTriggered == false)
                {
                    Instantiate(activationFx, transform);
                    sprite.color = new Color(255, 255, 255, 255);
                    activationTriggered = true;
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
        }
    }
}