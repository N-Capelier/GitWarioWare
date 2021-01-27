
using System.Collections.Generic;
using UnityEngine;

namespace Fleebos
{
    namespace BananaStrip
    {
        public class SoundManager : TimedBehaviour
        {
            public static SoundManager sd;

            public List<AudioSource>audioList = new List<AudioSource>();

            public void Awake()
            {
                sd = this;
            }
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

            }

            public void PlaySound(int index)
            {
                if(audioList[index].isPlaying == false)
                {
                    audioList[index].Play();
                }
            }

            public void StopSound(int index)
            {
                audioList[index].Stop();
            }
        }
    }
}