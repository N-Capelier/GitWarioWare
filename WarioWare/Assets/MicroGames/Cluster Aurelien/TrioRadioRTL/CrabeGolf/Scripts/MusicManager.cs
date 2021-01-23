using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadioRTL
{
    namespace CrabeGolf
    {
        public class MusicManager : TimedBehaviour
        {
            public AudioClip[] musics;
            AudioSource mySource;
            public override void Start()
            {
                base.Start(); //Do not erase this line!
                mySource = GetComponent<AudioSource>();
                switch (bpm)
                {
                    case (float)BPM.Slow:
                        mySource.clip = musics[0];
                        mySource.Play();
                        break;
                    case (float)BPM.Medium:
                        mySource.clip = musics[1];
                        mySource.Play();
                        break;
                    case (float)BPM.Fast:
                        mySource.clip = musics[2];
                        mySource.Play();
                        break;
                    case (float)BPM.SuperFast:
                        mySource.clip = musics[3];
                        mySource.Play();
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

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
        }
    }
}