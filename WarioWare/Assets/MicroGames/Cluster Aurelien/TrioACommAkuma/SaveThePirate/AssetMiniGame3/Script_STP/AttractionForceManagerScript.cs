using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace SaveThePirate
    {
        /// <summary>
        /// PARET Paul
        /// </summary>
        public class AttractionForceManagerScript : TimedBehaviour
        {
            private PointEffector2D pointEffector;
            public float attractionForceBase;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                pointEffector = GetComponent<PointEffector2D>();


                switch (bpm)
                {
                    case 60:
                        pointEffector.forceMagnitude = attractionForceBase;
                        break;
                    case 90:
                        pointEffector.forceMagnitude = attractionForceBase * 1.4f;
                        break;
                    case 120:
                        pointEffector.forceMagnitude = attractionForceBase * 1.8f;
                        break;
                    case 140:
                        pointEffector.forceMagnitude = attractionForceBase * 2.3f;
                        break;
                }

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
        }
    }
}