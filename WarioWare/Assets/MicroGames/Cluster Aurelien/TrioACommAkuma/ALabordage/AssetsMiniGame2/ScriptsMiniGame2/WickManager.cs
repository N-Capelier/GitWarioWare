using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace ALAbordage
    { 
        /// <summary>
       /// Simon PICARDAT
       /// </summary>

        public class WickManager : TimedBehaviour
        {
            public List<GameObject> wickTick;
            public int tickCount;
            public int tickMax;
            [HideInInspector] AudioSource audioTick;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                audioTick = GetComponent<AudioSource>();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (tickCount > tickMax)
                    gameObject.SetActive(false);
                if (tickCount == tickMax)
                {
                    audioTick.Play(0);
                    wickTick[tickCount].SetActive(false);
                    tickCount++;
                }

                if (tickCount < tickMax)
                {
                    audioTick.Play(0);
                    wickTick[tickCount].SetActive(false);
                    tickCount++;
                    wickTick[tickCount].transform.GetChild(0).gameObject.SetActive(true);
                }


            }
        }
    }
}