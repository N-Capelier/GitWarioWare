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
        public class BoatMovementPintade : TimedBehaviour
        {
            public Rigidbody rb;
            private float newX;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                rb = GetComponent<Rigidbody>();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                newX = (-0.5f) * Time.deltaTime * bpm;
                rb.velocity = new Vector3(newX, 0, 0);
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
        }
    }
}