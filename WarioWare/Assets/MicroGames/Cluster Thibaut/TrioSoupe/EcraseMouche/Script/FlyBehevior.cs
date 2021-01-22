using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Soupe
{
    namespace EcraseMouche
    {
        /// <summary>
        /// Arthur Galland
        /// </summary>
        public class FlyBehevior : TimedBehaviour
        {
            [SerializeField]
            private GameObject gameManager;
            [SerializeField]
            private List<GameObject> jamToGoPoints = new List<GameObject>();
            private int countOfJam;
            [SerializeField]
            private bool flyCanMove;
            private float tempVelocity;
            private int difficulty;
            private Vector3 spriteOrientation;
            private int timeSpeedMultiply; //used for tweaking the fly speed formula with the difficulty level (faster)

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                flyCanMove = false;
                SwatterBehevior.flyIsDead = false;
                countOfJam = 0;

                jamToGoPoints = MiniGameManager.jam; //copie of the jam list for the movement

                for (int i = 0; i < jamToGoPoints.Count; i++) //remove from the list the jam under the fly
                {
                    if(jamToGoPoints[i].tag == "Enemy1")
                    {
                        jamToGoPoints.Remove(jamToGoPoints[i]);
                    }
                }

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        difficulty = 1;
                        timeSpeedMultiply = 6;
                        break;

                    case Difficulty.MEDIUM:
                        difficulty = 2;
                        timeSpeedMultiply = 4;
                        break;

                    case Difficulty.HARD:
                        difficulty = 3;
                        timeSpeedMultiply = 2;
                        break;

                    default:
                        break;

                }

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
            }

            public void Update()
            {
                if (flyCanMove && !SwatterBehevior.flyIsDead)
                {
                    //movement with sin 
                    transform.position = Vector3.MoveTowards(transform.position, jamToGoPoints[countOfJam].transform.position, tempVelocity * Time.deltaTime) + transform.up * Mathf.Sin(Time.time * 20) * 0.02f;
                    //sound of fly when moving
                }
                else
                {
                    SoundManagerMouche.Instance.sfxSound[0].Stop();
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

                if (difficulty == 1)
                {
                    //every 2 tic the fly move to another location during 1 tic 
                    switch (Tick)
                    {
                        case 2:
                            FlyMovement();
                        break;

                        case 3:
                            IncreaseFlyJamCount();
                            break;
                        case 5:
                            FlyMovement();
                            break;
                        case 6:
                            flyCanMove = false;
                            break;

                        default:
                            break;

                    }
                }

                if (difficulty == 2)
                {
                    //every 1 tic the fly move to another location during 1 tic 
                    switch (Tick)
                    {
                        case 2:
                            FlyMovement();
                            break;

                        case 3:
                            IncreaseFlyJamCount();
                            break;

                        case 4:
                            FlyMovement();
                            break;

                        case 5:
                            IncreaseFlyJamCount();
                            break;

                        case 7:
                            FlyMovement();
                            break;

                        case 8:
                            flyCanMove = false;
                            break;

                        default:
                            break;

                    }
                }

                if (difficulty == 3)
                {
                    //every tic the fly move to another location 
                    switch (Tick)
                    {
                        case 1:
                            FlyMovementHard();
                            break;

                        case 2:
                            FlyMovementHard();
                            break;

                        case 3:
                            FlyMovementHard();
                            break;

                        case 4:
                            FlyMovementHard();
                            break;

                        case 5:
                            FlyMovementHard();
                            break;

                        case 6:
                            FlyMovementHard();
                            break;

                        case 7:
                            FlyMovementHard();
                            break;

                        case 8:
                            flyCanMove = false;
                            break;

                        default:
                            break;

                    }

                }

            }

            void FlyMovement()
            {
                tempVelocity = Vector3.Distance(transform.position, jamToGoPoints[countOfJam].transform.position) / ((10* timeSpeedMultiply) / bpm); //calcul the speed of the fly by dividing the distance between the fly and the next point by the time with the bpm
                flyCanMove = true;
                SoundManagerMouche.Instance.sfxSound[0].Play();

                if (!SwatterBehevior.flyIsDead)
                {
                    //turn the sprite in the direction of the new jam stain when she move
                    Vector3 dir = jamToGoPoints[countOfJam].transform.position - transform.position;
                    Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.right, dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 1000 * Time.deltaTime);
                }

            }

            void IncreaseFlyJamCount()
            {
                countOfJam++;
                flyCanMove = false;
            }

            void FlyMovementHard()
            {
                flyCanMove = true;
                SoundManagerMouche.Instance.sfxSound[0].Play();
                countOfJam++;

                //the fly make an another turn of the jam stain list
                if (countOfJam >= 3)
                {
                    countOfJam = 0;
                }

                tempVelocity = Vector3.Distance(transform.position, jamToGoPoints[countOfJam].transform.position) / ((10* timeSpeedMultiply) / bpm); //calcul the speed of the fly by dividing the distance between the fly and the next point by the time with the bpm (2x speed)

                if (!SwatterBehevior.flyIsDead)
                {
                    //turn the sprite in the direction of the new jam stain when she move
                    Vector3 dir = jamToGoPoints[countOfJam].transform.position - transform.position;
                    Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.right, dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 1000 * Time.deltaTime);
                }

            }
        }
       
    }
}