using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioSoupe
{
    namespace ServiceRapide
    {
        /// <summary>
        ///  Manon Ghignoni
        /// </summary>
        public class MusicManager : TimedBehaviour
        {
            public AudioClip music60Bpm;
            public AudioClip music80Bpm;
            public AudioClip music100Bpm;
            public AudioClip music120Bpm;
            AudioSource audiosource;

            public override void Start()
            {
                base.Start();
                audiosource = GetComponent<AudioSource>();
                switch (bpm)
                {
                    case 60:
                        audiosource.clip = music60Bpm;
                        break;
                    case 80:
                        audiosource.clip = music80Bpm;
                        break;
                    case 100:
                        audiosource.clip = music100Bpm;
                        break;
                    case 120:
                        audiosource.clip = music120Bpm;
                        break;
                }
                audiosource.Play();
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

        }
    }
}
