using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Cinquante
    {
        public class AudioManagerScript : TimedBehaviour
        {
            
            public AudioSource StartSound;
            public AudioSource BreathSound;
            public AudioSource StepSound;
            public AudioSource EndSound;

            /*
            public AudioSource Run_60Bpm;
            public AudioSource Run_90Bpm;
            public AudioSource Run_120Bpm;
            public AudioSource Run_140Bpm;
            */

            public AudioSource musicSource;
            public AudioClip Run_60Bpm;
            public AudioClip Run_80Bpm;
            public AudioClip Run_100Bpm;
            public AudioClip Run_120Bpm;

            //public AudioClip BreathMoment;

            // Start is called before the first frame update
            public override void Start()
            {
                base.Start();

                switch (bpm)
                {
                    
                    case (float)BPM.Slow:
                        musicSource.PlayOneShot(Run_60Bpm, 0.4f);
                        break;

                   case (float)BPM.Medium:
                        musicSource.PlayOneShot(Run_80Bpm, 0.4f);
                        break;

                    case (float)BPM.Fast:
                        musicSource.PlayOneShot(Run_100Bpm, 0.4f);
                        break;

                    case (float)BPM.SuperFast:
                        musicSource.PlayOneShot(Run_120Bpm, 0.4f);
                        break;
                }
            }

            // Update is called once per frame
            public void StartScene()
            {
                StartSound.Play();

            }

            public void Running()
            {
                StepSound.Play();
            }

            public void StopRunning()
            {
                StepSound.Stop();
            }

            public void Breath()
            {
                //BreathSound.PlayOneShot(BreathMoment, 0.3f);            
            }

            public void EndScene()
            {
                EndSound.Play();
            }


        }
    }
}
