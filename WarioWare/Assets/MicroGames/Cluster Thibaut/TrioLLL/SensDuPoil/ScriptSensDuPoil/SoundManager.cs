using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioLLL
{
    namespace SensDuPoil
    {
        public class SoundManager : TimedBehaviour
        {
            #region AudioSources
//            public AudioSource musicSource;
            public AudioSource sfxSource;
            public AudioSource sfxSource2;
            public AudioSource musicSource;

            #endregion
            public AudioClip musiqueSpeed1;
            public AudioClip musiqueSpeed2;
            public AudioClip musiqueSpeed3;
            public AudioClip musiqueSpeed4;
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                //créer les audiosources


                // loop les musiques
                musicSource.loop = true;
                //            sfxSource.loop = true;

                //PlayMusic(musiqueSpeed1);
                if (Manager.Instance.bpm == BPM.Slow)
                {
                    PlayMusic(musiqueSpeed1);

                }
                else if (Manager.Instance.bpm == BPM.Medium)
                {
                    PlayMusic(musiqueSpeed2);
                }
                else if (Manager.Instance.bpm == BPM.Fast)
                {
                    PlayMusic(musiqueSpeed3);
                }
                else if (Manager.Instance.bpm == BPM.SuperFast)
                {
                    PlayMusic(musiqueSpeed4);
                }

               /* if (bpm == 60)
                {
                    PlayMusic(musiqueSpeed1);

                }
                else if (bpm == 80)
                {
                    PlayMusic(musiqueSpeed2);
                }
                else if (bpm == 100)
                {
                    PlayMusic(musiqueSpeed3);
                }
                else if (bpm == 120)
                {
                    PlayMusic(musiqueSpeed4);
                }*/

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
            public void PlayMusic(AudioClip musicClip)
            {
                //Debug.Log(musicClip);
                //Debug.Log(musicSource);
                musicSource.clip = musicClip;
                musicSource.Play();
            }

            public void PlaySFX(AudioClip clip, float volume)
            {
                sfxSource.PlayOneShot(clip, volume);

            }
            public void PlaySFX2(AudioClip clip, float volume)
            {
                sfxSource2.PlayOneShot(clip, volume);

            }
        }
    }
}