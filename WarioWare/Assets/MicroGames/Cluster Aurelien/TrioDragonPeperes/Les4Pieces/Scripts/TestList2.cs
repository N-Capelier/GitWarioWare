using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonsPeperes
{
    namespace Les4Pieces
    {
        public class TestList2 : TimedBehaviour
        {
            public List<GameObject> symboles = new List<GameObject>();

           public List<GameObject> pieces = new List<GameObject>();
 

            
            public override void Start()
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    int rand = Random.Range(0, symboles.Count);
                    Instantiate(symboles[rand], pieces[i].transform.position, Quaternion.identity);
                    symboles.RemoveAt(rand);
                }

                base.Start(); //Do not erase this line!

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