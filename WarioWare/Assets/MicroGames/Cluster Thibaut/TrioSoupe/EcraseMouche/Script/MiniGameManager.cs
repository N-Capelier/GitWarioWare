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
        public class MiniGameManager : TimedBehaviour
        {
            public static List<GameObject> jam = new List<GameObject>();
            [SerializeField]
            private List<GameObject> spawnPoint = new List<GameObject>();
            [SerializeField]
            private GameObject jamStain;
            [SerializeField]
            private GameObject flyBug;
            [SerializeField]
            private GameObject flyHierarchy;
            [SerializeField]
            private GameObject swatter;
            [SerializeField]
            private Transform posFly;

            private int tempIndJam;
            private int numberOfJam;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                jam.Clear();

                switch (bpm)
                {
                    case (float)BPM.Slow:
                        SoundManagerMouche.Instance.globalMusic[0].Play();
                        break;

                    case (float)BPM.Medium:
                        SoundManagerMouche.Instance.globalMusic[1].Play();
                        break;

                    case (float)BPM.Fast:
                        SoundManagerMouche.Instance.globalMusic[2].Play();
                        break;

                    case (float)BPM.SuperFast:
                        SoundManagerMouche.Instance.globalMusic[3].Play();
                        break;
                }
               

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        numberOfJam = 3;
                        break;

                    case Difficulty.MEDIUM:
                        numberOfJam = 4;
                        break;

                    case Difficulty.HARD:
                        numberOfJam = 4;
                        break;

                    default:
                        break;
                }

                //place jam
                PlaceJam();
                //place fly on the left jam
                PlaceFly();
                //place swatter on a jam but not the one with the fly
                PlaceSwatter();

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8)
                {
                    if (SwatterBehevior.flyIsDead == true)
                    {
                        Manager.Instance.Result(true);
                        Debug.Log("win");
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                        Debug.Log("Lose");
                    }
                }
            }

            public void PlaceJam()
            {
                List <GameObject> jamBis = new List<GameObject>(spawnPoint); //copie of the jam list
                for(int i=0; i<numberOfJam; i++) //instantiate the jams 
                {
                    tempIndJam = Random.Range(0, jamBis.Count); //temp random number
                    GameObject newStain = Instantiate(jamStain, jamBis[tempIndJam].transform); //intantiate a new jam
                    jamBis.Remove(jamBis[tempIndJam]);
                    jam.Add(newStain); //add the stain to the jam list 
                }
            }

            public void PlaceFly()
            {
                foreach(GameObject jamPoint in spawnPoint)
                {
                    if (jamPoint.transform.childCount > 0) //warning if the differents spawnpoints have other child update the 0
                    {
                        GameObject temp = Instantiate(flyBug, jamPoint.transform);
                        posFly = jamPoint.transform;
                        jamPoint.transform.GetChild(0).tag = "Enemy1"; //warning if the differents spawnpoints have other child update the 0
                        temp.transform.parent = flyHierarchy.transform;
                        break;
                    }
                }
            }

            public void PlaceSwatter()
            {
                int temp = Random.Range(0, numberOfJam);

                if (jam[temp].transform.position == posFly.position) //check if the fly is already on the jam stain
                {
                    PlaceSwatter(); //Déso Nico
                }
                else
                {
                    GameObject swatterPosTemp = Instantiate(swatter, jam[temp].transform);
                    swatterPosTemp.transform.parent = flyHierarchy.transform;
                }
            }
        }
    }
}