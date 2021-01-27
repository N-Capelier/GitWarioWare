using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioLLL
{
    namespace Basho
    {
        /// <summary>
        /// Louis Vitrant
        /// </summary>
        public class SoundEffectsManager : TimedBehaviour
        {
            public AudioSource music;
            public AudioSource mainSource;
            public AudioClip bossLevel1Entry;
            public AudioClip bossLevel2Entry;
            public AudioClip bossLevel3Entry;
            public AudioClip bossFall;
            public AudioClip musicLevel1_60;
            public AudioClip musicLevel1_80;
            public AudioClip musicLevel1_100;
            public AudioClip musicLevel1_120;
            public AudioClip musicLevel2_60;
            public AudioClip musicLevel2_80;
            public AudioClip musicLevel2_100;
            public AudioClip musicLevel2_120;
            public AudioClip musicLevel3_60;
            public AudioClip musicLevel3_80;
            public AudioClip musicLevel3_100;
            public AudioClip musicLevel3_120;

            public override void Start()
            {
                base.Start();

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        switch(bpm)
                        {
                            case 60:
                                music.PlayOneShot(musicLevel1_60);
                                mainSource.PlayOneShot(bossLevel1Entry);
                                break;
                            case 80:
                                music.PlayOneShot(musicLevel1_80);
                                mainSource.PlayOneShot(bossLevel2Entry);
                                break;
                            case 100:
                                music.PlayOneShot(musicLevel1_100);
                                mainSource.PlayOneShot(bossLevel3Entry);
                                break;
                            case 120:
                                music.PlayOneShot(musicLevel1_120);
                                mainSource.PlayOneShot(bossLevel3Entry);
                                break;
                            default:
                                throw new System.Exception("enum not checked!");
                        }
                        break;
                    case Difficulty.MEDIUM:
                        switch (bpm)
                        {
                            case 60:
                                music.PlayOneShot(musicLevel2_60);
                                mainSource.PlayOneShot(bossLevel1Entry);
                                break;
                            case 80:
                                music.PlayOneShot(musicLevel2_80);
                                mainSource.PlayOneShot(bossLevel2Entry);
                                break;
                            case 100:
                                music.PlayOneShot(musicLevel2_100);
                                mainSource.PlayOneShot(bossLevel3Entry);
                                break;
                            case 120:
                                music.PlayOneShot(musicLevel2_120);
                                mainSource.PlayOneShot(bossLevel3Entry);
                                break;
                            default:
                                throw new System.Exception("enum not checked!");
                        }
                        break;
                    case Difficulty.HARD:
                        switch (bpm)
                        {
                            case 60:
                                music.PlayOneShot(musicLevel3_60);
                                mainSource.PlayOneShot(bossLevel1Entry);
                                break;
                            case 80:
                                music.PlayOneShot(musicLevel3_80);
                                mainSource.PlayOneShot(bossLevel2Entry);
                                break;
                            case 100:
                                music.PlayOneShot(musicLevel3_100);
                                mainSource.PlayOneShot(bossLevel3Entry);
                                break;
                            case 120:
                                music.PlayOneShot(musicLevel3_120);
                                mainSource.PlayOneShot(bossLevel3Entry);
                                break;
                            default:
                                throw new System.Exception("enum not checked!");
                        }
                        break;
                    default:
                        throw new System.Exception("enum not checked!");
                }
            }
        }
    }
}
