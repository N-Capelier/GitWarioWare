using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace SaveThePirate
    {
        public class SpeedLinesScript : TimedBehaviour
        {
            private Animator lineAnimator;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                lineAnimator = GetComponent<Animator>();

                switch (bpm)
                {
                    case 60:
                        lineAnimator.speed = 1f;
                        break;
                    case 90:
                        lineAnimator.speed = 1.365f;
                        break;
                    case 120:
                        lineAnimator.speed = 1.635f;
                        break;
                    case 140:
                        lineAnimator.speed = 2f;
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

            public void EndEvent()
            {
                lineAnimator.SetBool("isSpeedLine", false);
            }

            public void StartAnim()
            {
                lineAnimator.SetBool("isSpeedLine", true);
            }
        }
    }
}