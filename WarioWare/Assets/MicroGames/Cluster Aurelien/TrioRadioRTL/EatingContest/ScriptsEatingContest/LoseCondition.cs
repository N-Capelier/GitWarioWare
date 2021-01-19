using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace RadioRTL
{
    namespace EatingContest
    {
        public class LoseCondition : TimedBehaviour
        {
            bool win;
            public PlayerController player;
            public override void Start()
            {
                base.Start(); //Do not erase this line!
                
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                win = player.win;
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8)
                {
                    Debug.Log("win = " + win);
                    if (win)
                    {
                        Manager.Instance.Result(true);
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                    }
                    
                }
            }
        }
    }
}