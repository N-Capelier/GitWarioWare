using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioLLL
{
    namespace Pintade
    {
        /// <summary>
        /// VIDAL Luc
        /// </summary>
        public class TouffeManager : TimedBehaviour
        {
            public GameObject frog;
            public GameObject pintade;
            public GameObject frogHat;
            public GameObject pintadeHat;
            public GameObject frogPatch;
            public GameObject pintadePatch;
            public GameObject animalsParent;
            public GameObject manager;
            public GameObject soundManager;
            public int side;

            [HideInInspector] public int choice;
            [HideInInspector] public bool pintadeON;
            [HideInInspector] public bool frogON;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                choice = 0;
                pintadeON = false;
                frogON = false;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                base.TimedUpdate();

                if(Tick == 1)
                {
                    choice = Random.Range(1, 3);
                }

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        if (Tick == 4)
                        {
                            if (pintadeON == true)
                            {
                                animalsParent.SetActive(true);
                                pintade.SetActive(true);
                                soundManager.GetComponent<PintadeSoundManager>().pintade = true;
                            }
                            else if (frogON == true)
                            {
                                animalsParent.SetActive(true);
                                frog.SetActive(true);
                                if (side == 0)
                                {
                                    soundManager.GetComponent<PintadeSoundManager>().frog1 = true;
                                }
                                else if (side == 1)
                                {
                                    soundManager.GetComponent<PintadeSoundManager>().frog2 = true;
                                }
                                
                            }
                        }
                        break;

                    case Difficulty.MEDIUM:
                        if (Tick == 4)
                        {
                            if (pintadeON == true)
                            {
                                animalsParent.SetActive(true);
                                pintade.SetActive(true);
                                pintadeHat.SetActive(true);
                                soundManager.GetComponent<PintadeSoundManager>().pintade = true;
                            }
                            else if (frogON == true)
                            {
                                animalsParent.SetActive(true);
                                frog.SetActive(true);
                                frogHat.SetActive(true);
                                if (side == 0)
                                {
                                    soundManager.GetComponent<PintadeSoundManager>().frog1 = true;
                                }
                                else if (side == 1)
                                {
                                    soundManager.GetComponent<PintadeSoundManager>().frog2 = true;
                                }
                            }
                        }
                        break;

                    case Difficulty.HARD:
                        if (side == manager.GetComponent<PintadeGlobalManager>().bigChoice)
                        {
                            if (Tick == 3)
                            {
                                if (pintadeON == true)
                                {
                                    animalsParent.SetActive(true);
                                    pintade.SetActive(true);
                                    pintadeHat.SetActive(true);
                                    pintadePatch.SetActive(true);
                                    soundManager.GetComponent<PintadeSoundManager>().pintade = true;
                                }
                                else if (frogON == true)
                                {
                                    animalsParent.SetActive(true);
                                    frog.SetActive(true);
                                    frogHat.SetActive(true);
                                    frogPatch.SetActive(true);
                                    if (side == 0)
                                    {
                                        soundManager.GetComponent<PintadeSoundManager>().frog1 = true;
                                    }
                                    else if (side == 1)
                                    {
                                        soundManager.GetComponent<PintadeSoundManager>().frog2 = true;
                                    }
                                }
                            }
                        }
                        else if (side != manager.GetComponent<PintadeGlobalManager>().bigChoice)
                        {
                            if (Tick == 5)
                            {
                                if (pintadeON == true)
                                {
                                    animalsParent.SetActive(true);
                                    pintade.SetActive(true);
                                    pintadeHat.SetActive(true);
                                    pintadePatch.SetActive(true);
                                    soundManager.GetComponent<PintadeSoundManager>().pintade = true;
                                }
                                else if (frogON == true)
                                {
                                    animalsParent.SetActive(true);
                                    frog.SetActive(true);
                                    frogHat.SetActive(true);
                                    frogPatch.SetActive(true);
                                    if (side == 0)
                                    {
                                        soundManager.GetComponent<PintadeSoundManager>().frog1 = true;
                                    }
                                    else if (side == 1)
                                    {
                                        soundManager.GetComponent<PintadeSoundManager>().frog2 = true;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}