using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;


namespace TrapioWare
{
    namespace Climb
    {
        public class SunManager : TimedBehaviour
        {
            [SerializeField] private int timerDead = 0;
            
            public AudioClip[] bpmSounds;
            [SerializeField] private GameObject musiqueDeFond;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                
                switch (Manager.Instance.bpm)
                {
                    case BPM.Slow:
                        ClimbGameManager.Instance.mySpeed = 60;
                        musiqueDeFond.GetComponent<AudioSource>().clip = bpmSounds[0];
                        musiqueDeFond.GetComponent<AudioSource>().Play();

                        break;
                    case BPM.Medium:
                        ClimbGameManager.Instance.mySpeed = 90;
                        musiqueDeFond.GetComponent<AudioSource>().clip = bpmSounds[1];
                        musiqueDeFond.GetComponent<AudioSource>().Play();

                        break;
                    case BPM.Fast:
                        ClimbGameManager.Instance.mySpeed = 120;
                        musiqueDeFond.GetComponent<AudioSource>().clip = bpmSounds[2];
                        musiqueDeFond.GetComponent<AudioSource>().Play();
                        break;
                    case BPM.SuperFast:
                        ClimbGameManager.Instance.mySpeed = 140;
                        musiqueDeFond.GetComponent<AudioSource>().clip = bpmSounds[3];
                        musiqueDeFond.GetComponent<AudioSource>().Play();
                        break;
                    default:
                        break;
                }

                switch (Manager.Instance.currentDifficulty)
                {
                    case Difficulty.EASY:
                        ClimbGameManager.Instance.myDifficulty = 0;
                        Debug.Log(ClimbGameManager.Instance.myDifficulty);
                        ClimbGameManager.Instance.setDifficulty = true;
                        break;
                    case Difficulty.MEDIUM:
                        ClimbGameManager.Instance.myDifficulty = 1;
                        Debug.Log(ClimbGameManager.Instance.myDifficulty);
                        ClimbGameManager.Instance.setDifficulty = true;
                        break;
                    case Difficulty.HARD:
                        ClimbGameManager.Instance.myDifficulty = 2;
                        Debug.Log(ClimbGameManager.Instance.myDifficulty);
                        ClimbGameManager.Instance.setDifficulty = true;
                        break;
                    default:
                        break;
                }

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (ClimbGameManager.Instance.win && timerDead == 8)
                {
                    timerDead = 0;
                    ClimbGameManager.Instance.win = false;
                    Debug.Log("Win");
                    Manager.Instance.Result(true);
                }

                if (ClimbGameManager.Instance.lose && ClimbGameManager.Instance.win == false && timerDead == 8)
                {
                    Manager.Instance.Result(false);
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                timerDead++;

                if (TrapioWare.Climb.ClimbGameManager.Instance.needToStop == false && timerDead !=8)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("TickTrigger");
                }

                if(timerDead == 8 && ClimbGameManager.Instance.lose == false && ClimbGameManager.Instance.win == false)
                {
                    //Manager.Instance.Result(false);
                    Debug.Log("You Lose");
                    TrapioWare.Climb.ClimbGameManager.Instance.deadSound.Play();
                    TrapioWare.Climb.ClimbGameManager.Instance.lose = true;
                    TrapioWare.Climb.ClimbGameManager.Instance.needToStop = true;
                    
                }

            }
        }
    }
}