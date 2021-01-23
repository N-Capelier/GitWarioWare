using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace ACommeAkuma
{
    namespace SaveThePirate
    {
        /// <summary>
        /// PARET Paul
        /// </summary>
        public class LevelManagerScript : TimedBehaviour
        {
            [Header("Levels")]
            public GameObject level1Prefab;
            public GameObject level2Prefab;
            public GameObject level3Prefab;

            [Header("Difficulty")]
            public Difficulty difficulty;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        Instantiate(level1Prefab, transform);
                        break;
                    case Difficulty.MEDIUM:
                        Instantiate(level2Prefab, transform);
                        break;
                    case Difficulty.HARD:
                        Instantiate(level3Prefab, transform);
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