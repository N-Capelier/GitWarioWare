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
        public class ServalManager : TimedBehaviour
        {
            [HideInInspector] public bool leftActivated;
            [HideInInspector] public bool rightActivated;
            [HideInInspector] public bool sickServal;
            [HideInInspector] public bool happyServal;
            private bool jumped;
            public GameObject leftSickServal;
            public GameObject rightSickServal;
            public GameObject leftHappyServal;
            public GameObject rightHappyServal;
            public GameObject servalBack;
            public GameObject leftAnimalParent;
            public GameObject rightAnimalParent;
            public GameObject servalJumpLeft;
            public GameObject servalJumpRight;
            public GameObject soundManager;
            public GameObject featherParticle;
            public GameObject rightParticleSpawner;
            public GameObject leftParticleSpawner;
            public float time;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                leftActivated = false;
                rightActivated = false;
                sickServal = false;
                happyServal = false;
                jumped = false;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (jumped == false && leftActivated == true)
                {
                    if (sickServal == true)
                    {
                        StartCoroutine(SickLeft());
                    }
                    else if (happyServal == true)
                    {
                        StartCoroutine(HappyLeft());
                    }

                    jumped = true;
                }
                else if (jumped == false && rightActivated == true)
                {
                    if (sickServal == true)
                    {
                        StartCoroutine(SickRight());
                    }
                    else if (happyServal == true)
                    {
                        StartCoroutine(HappyRight());
                    }

                    jumped = true;
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

            IEnumerator SickLeft ()
            {
                servalBack.SetActive(false);
                servalJumpLeft.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalAttack = true;

                yield return new WaitForSeconds(time);

                servalJumpLeft.SetActive(false);
                leftAnimalParent.SetActive(false);

                yield return new WaitForSeconds(0.15f);

                soundManager.GetComponent<PintadeSoundManager>().frogDeath = true;

                yield return new WaitForSeconds(0.25f);

                leftSickServal.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalPuke = true;
            }

            IEnumerator HappyLeft ()
            {
                servalBack.SetActive(false);
                servalJumpLeft.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalAttack = true;

                yield return new WaitForSeconds(time);

                servalJumpLeft.SetActive(false);
                leftAnimalParent.SetActive(false);
                Instantiate(featherParticle, leftParticleSpawner.transform);

                yield return new WaitForSeconds(0.15f);

                soundManager.GetComponent<PintadeSoundManager>().pintadeDeath = true;

                yield return new WaitForSeconds(0.25f);

                leftHappyServal.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalHappy = true;
            }

            IEnumerator SickRight ()
            {
                servalBack.SetActive(false);
                servalJumpRight.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalAttack = true;

                yield return new WaitForSeconds(time);

                servalJumpRight.SetActive(false);
                rightAnimalParent.SetActive(false);

                yield return new WaitForSeconds(0.15f);

                soundManager.GetComponent<PintadeSoundManager>().frogDeath = true;

                yield return new WaitForSeconds(0.25f);

                rightSickServal.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalPuke = true;
            }

            IEnumerator HappyRight ()
            {
                servalBack.SetActive(false);
                servalJumpRight.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalAttack = true;

                yield return new WaitForSeconds(time);

                servalJumpRight.SetActive(false);
                rightAnimalParent.SetActive(false);
                Instantiate(featherParticle, rightParticleSpawner.transform);

                yield return new WaitForSeconds(0.15f);

                soundManager.GetComponent<PintadeSoundManager>().pintadeDeath = true;

                yield return new WaitForSeconds(0.25f);

                rightHappyServal.SetActive(true);
                soundManager.GetComponent<PintadeSoundManager>().servalHappy = true;
            }
        }
    }
}