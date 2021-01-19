using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonsPeperes
{
    namespace ChasseAuTresor
    {
        /// <summary>
        /// Noé Blanc
        /// </summary>

        public class MusicScript : TimedBehaviour
        { 
            public AudioSource audiosource60;
            public AudioSource audiosource80;
            public AudioSource audiosource100;
            public AudioSource audiosource120;

            public override void Start()
            {
                base.Start(); //Do not erase this line!                


                if (bpm == 60)
                {
                    audiosource60.Play();
                }
                if (bpm == 80)
                {
                    audiosource80.Play();
                }
                if (bpm == 100)
                {
                    audiosource100.Play();
                }
                if (bpm == 120)
                {
                    audiosource120.Play();
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