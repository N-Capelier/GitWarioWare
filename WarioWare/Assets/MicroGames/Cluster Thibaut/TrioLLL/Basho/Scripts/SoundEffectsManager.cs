using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace TrioLLL
{
    namespace Basho
    {
        /// <summary>
        /// Louis Vitrant
        /// </summary>
        public class SoundEffectsManager : TimedBehaviour
        {
            private AudioSource music;
            private AudioSource mainSource;
            public AudioClip bossLevel1Entry;
            public AudioClip bossLevel2Entry;
            public AudioClip bossLevel3Entry;
            public AudioClip bossFall;
            public AudioClip musicLevel1;
            public AudioClip musicLevel2;
            public AudioClip musicLevel3;

            public override void Start()
            {
                base.Start();
                music = GetComponent<AudioSource>();
                mainSource = GetComponent<AudioSource>();

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        music.PlayOneShot(musicLevel1);
                        mainSource.PlayOneShot(bossLevel1Entry);
                        break;
                    case Difficulty.MEDIUM:
                        music.PlayOneShot(musicLevel2);
                        mainSource.PlayOneShot(bossLevel2Entry);
                        break;
                    case Difficulty.HARD:
                        music.PlayOneShot(musicLevel3);
                        mainSource.PlayOneShot(bossLevel3Entry);
                        break;
                    default:
                        throw new System.Exception("enum not checked!");
                }
            }
        }
    }
}
