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
        public class PintadeSoundManager : TimedBehaviour
        {
            [HideInInspector] public bool grass1;
            [HideInInspector] public bool grass2;
            [HideInInspector] public bool pintade;
            [HideInInspector] public bool frog1;
            [HideInInspector] public bool frog2;
            [HideInInspector] public bool pintadeDeath;
            [HideInInspector] public bool frogDeath;
            [HideInInspector] public bool servalAttack;
            [HideInInspector] public bool servalHappy;
            [HideInInspector] public bool servalPuke;
            [HideInInspector] public bool windAmbiance;

            public GameObject leftGrass;
            public GameObject rightGrass;
            public GameObject pintadeSpawn;
            public GameObject leftFrog;
            public GameObject rightFrog;
            public GameObject pintadeDead;
            public GameObject frogDead;
            public GameObject servalAttacking;
            public GameObject servalContent;
            public GameObject servalVomit;
            public GameObject windContinue;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                grass1 = false;
                grass2 = false;
                pintade = false;
                frog1 = false;
                frog2 = false;
                pintadeDeath = false;
                frogDeath = false;
                servalAttack = false;
                servalHappy = false;
                servalPuke = false;
                windAmbiance = true;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (grass1 == true)
                {
                    leftGrass.GetComponent<AudioSource>().Play();
                    grass1 = false;
                }

                if (grass2 == true)
                {
                    rightGrass.GetComponent<AudioSource>().Play();
                    grass2 = false;
                }

                if (pintade == true)
                {
                    pintadeSpawn.GetComponent<AudioSource>().Play();
                    pintade = false;
                }

                if (frog1 == true)
                {
                    leftFrog.GetComponent<AudioSource>().Play();
                    frog1 = false;
                }

                if (frog2 == true)
                {
                    rightFrog.GetComponent<AudioSource>().Play();
                    frog2 = false;
                }

                if (pintadeDeath == true)
                {
                    pintadeDead.GetComponent<AudioSource>().Play();
                    pintadeDeath = false;
                }

                if (frogDeath == true)
                {
                    frogDead.GetComponent<AudioSource>().Play();
                    frogDeath = false;
                }

                if (servalAttack == true)
                {
                    servalAttacking.GetComponent<AudioSource>().Play();
                    servalAttack = false;
                }

                if (servalHappy == true)
                {
                    servalContent.GetComponent<AudioSource>().Play();
                    servalHappy = false;
                }

                if (servalPuke == true)
                {
                    servalVomit.GetComponent<AudioSource>().Play();
                    servalPuke = false;
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
        }
    }
}