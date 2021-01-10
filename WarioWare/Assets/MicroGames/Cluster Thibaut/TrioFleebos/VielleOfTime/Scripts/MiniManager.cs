using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Fleebos
{
    namespace VielleOfTime
    {
        public class MiniManager : TimedBehaviour
        {
            public VielleManager VM;
            public SequenceManager SM;


            public override void Start()
            {
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
                if (SM.NoteOrder >= SM.MusicNotes.Count && Tick == 8)
                {
                    Manager.Instance.Result(true);
                }

                if(Tick == 8 && SM.NoteOrder < SM.MusicNotes.Count)
                {
                    Manager.Instance.Result(false);
                }
            }
        }
    }
}