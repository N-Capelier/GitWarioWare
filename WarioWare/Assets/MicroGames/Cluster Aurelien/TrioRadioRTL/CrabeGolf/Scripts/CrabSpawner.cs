using UnityEngine;
using Caps;

namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>
    namespace CrabGolf
    {
        public class CrabSpawner : TimedBehaviour
        {
            public static CrabSpawner cs;

            public GameObject crab;
            public GameObject crabParrot;
            [Range(0, 4)]
            public int numberCP;
            public int CP;
            public DifficultyManager difficultyManager;
            public bool lose;

            public bool willWin = true;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                cs = this;
                PlayerBehavior.Instance.BPM = bpm;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                switch (Manager.Instance.currentDifficulty)
                {
                    case Difficulty.EASY:
                        numberCP = difficultyManager.easyCP;
                        break;
                    case Difficulty.MEDIUM:
                        numberCP = difficultyManager.mediumCP;
                        break;
                    case Difficulty.HARD:
                        numberCP = difficultyManager.hardCP;
                        break;
                    default:
                        break;
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                int typeCrab;

                if (Tick == 2)
                {
                    CrabBehaviour currentCrab = Instantiate(crab, new Vector3(-8, -3, 0), Quaternion.identity).GetComponent<CrabBehaviour>();

                }
                if (Tick == 3)
                {
                    if (CP < numberCP)
                    {

                        typeCrab = Random.Range(0, 2);
                    }
                    else
                    {
                        typeCrab = 1;
                    }

                    if (typeCrab == 1)
                    {
                        Instantiate(crab, new Vector3(-8, -3, 0), Quaternion.identity);
                    }
                    else if (typeCrab == 0)
                    {
                        Instantiate(crabParrot, new Vector3(-8, -3, 0), Quaternion.identity);
                        CP += 1;
                    }
                }

                if (Tick == 4)
                {
                    if (CP < numberCP)
                    {
                        typeCrab = Random.Range(0, 2);
                    }
                    else
                    {
                        typeCrab = 1;
                    }

                    if (typeCrab == 1)
                    {
                        Instantiate(crab, new Vector3(-8, -3, 0), Quaternion.identity);
                    }
                    else if (typeCrab == 0)
                    {
                        Instantiate(crabParrot, new Vector3(-8, -3, 0), Quaternion.identity);
                        CP += 1;
                    }
                }

                if (Tick == 5)
                {
                    if (CP < numberCP)
                    {

                        typeCrab = Random.Range(0, 2);
                    }
                    else
                    {
                        typeCrab = 1;
                    }

                    if (typeCrab == 1)
                    {
                        Instantiate(crab, new Vector3(-8, -3, 0), Quaternion.identity);
                    }
                    else if (typeCrab == 0)
                    {
                        Instantiate(crabParrot, new Vector3(-8, -3, 0), Quaternion.identity);
                        CP += 1;
                    }
                }

                if (Tick == 8 && !lose)
                {
                    Manager.Instance.Result(true);
                }
                else if (Tick == 8 && lose)
                {
                    Manager.Instance.Result(false);
                }
            }
        }
    }
}


