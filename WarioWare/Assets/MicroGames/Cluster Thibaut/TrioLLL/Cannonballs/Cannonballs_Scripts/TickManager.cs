using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioLLL
{
    namespace Cannonballs
    {
        public class TickManager : TimedBehaviour
        {
            private TrioLLL.Cannonballs.Gabarits[] gabarits;
            public override void Start()
            {
                base.Start(); //Do not erase this line!
                gabarits = FindObjectsOfType<TrioLLL.Cannonballs.Gabarits>();
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
                    bool win = true;
                    foreach (TrioLLL.Cannonballs.Gabarits gabarit in gabarits)
                    {
                        if (!gabarit.isPlayerOut) win=false;
                    }
                    Debug.Log(win);
                    Manager.Instance.Result(win);
                }
            }
        }
    }
}