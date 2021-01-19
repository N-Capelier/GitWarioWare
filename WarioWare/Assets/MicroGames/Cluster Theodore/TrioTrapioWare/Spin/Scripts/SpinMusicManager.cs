using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrapioWare
{
    namespace Spin
    {
        public class SpinMusicManager : TimedBehaviour
        {
            public BpmMusic[] musics;

            private AudioSource source;

            public override void Start()
            {
                base.Start();
                source = GetComponent<AudioSource>();
                AudioClip currentMusic = null;
                for(int i = 0; i < musics.Length; i++)
                {
                    if(musics[i].bpm == bpm)
                    {
                        currentMusic = musics[i].music;
                    }
                }

                if(currentMusic != null)
                {
                    source.clip = currentMusic;
                    source.Play();
                }
            }
        }

        [System.Serializable]
        public class BpmMusic
        {
            public AudioClip music;
            public float bpm;
        }
    }
}