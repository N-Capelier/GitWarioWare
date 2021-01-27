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
        public class GrassShakeScript : TimedBehaviour
        {
            public float magnitude;
            public float duration;
            public GameObject touffeParent;
            public GameObject manager;
            public GameObject soundManager;
            private int side;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                side = touffeParent.GetComponent<TouffeManager>().side;

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                switch (currentDifficulty)  //Shake des Touffes.
                {
                    case Difficulty.EASY:
                        if (Tick <= 3)
                        {
                            StartCoroutine(Shake(magnitude, duration));
                        }
                        break;

                    case Difficulty.MEDIUM:
                        if (Tick <= 3)
                        {
                            StartCoroutine(Shake(magnitude, duration));
                        }
                        break;

                    case Difficulty.HARD:
                        if (side == manager.GetComponent<PintadeGlobalManager>().bigChoice)
                        {
                            if (Tick <= 2)
                            {
                                StartCoroutine(Shake(magnitude, duration));
                            }
                        }
                        else if (side != manager.GetComponent<PintadeGlobalManager>().bigChoice)
                        {
                            if (Tick <= 4)
                            {
                                StartCoroutine(Shake(magnitude, duration));
                            }
                        }
                        break;
                }
            }

            IEnumerator Shake(float magnitude, float duration) //Coroutine de Shake des Touffes.
            {
                Vector3 originalPos = transform.position;

                float elapsed = 0.0f;

                if (side != manager.GetComponent<PintadeGlobalManager>().bigChoice)
                {
                    soundManager.GetComponent<PintadeSoundManager>().grass1 = true;
                }


                while (elapsed < duration)
                {
                    float x = Random.Range(-1f, 1f) * magnitude;

                    transform.position = new Vector3(originalPos.x + x, originalPos.y, originalPos.z);

                    elapsed += Time.deltaTime;

                    yield return null;
                }

                transform.position = originalPos;
            }
        }
    }
}