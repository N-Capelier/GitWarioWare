using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioLLL
{
    namespace Cannonballs
    {

    

        public class DifficultyManager : TimedBehaviour
        {
            public GameObject Player;
            public GameObject GabaritRandomD1;
            public GameObject GabaritRandomD2;
            public GameObject GabaritFollow;
            public GameObject GabaritPosition;
            public override void Start()
            {
                base.Start(); //Do not erase this line!
                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        Instantiate(GabaritRandomD1, Player.transform.position, Quaternion.identity, transform);
                        break;
                    case Difficulty.MEDIUM:
                        Instantiate(GabaritRandomD2, Player.transform.position, Quaternion.identity, transform);
                        Instantiate(GabaritFollow, GabaritPosition.transform.position, Quaternion.identity, transform);
                        break;
                    case Difficulty.HARD:
                        Instantiate(GabaritRandomD2, Player.transform.position, Quaternion.identity, transform);
                        Instantiate(GabaritFollow, Player.transform.position, Quaternion.identity, transform);
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