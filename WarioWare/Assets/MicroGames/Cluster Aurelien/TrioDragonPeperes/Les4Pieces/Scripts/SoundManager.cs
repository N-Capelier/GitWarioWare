using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace Les4Pieces
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>
        public class SoundManager : MonoBehaviour
        {
            private AudioSource[] sounds;
            public AudioSource audioSource;

            #region sons audiosource

            private AudioSource music60;
            private AudioSource music80;
            private AudioSource music100;
            private AudioSource music120;
            private AudioSource coin;
            private AudioSource foule_negative;
            private AudioSource foule_positive;
            private AudioSource paper;
            #endregion
            [SerializeField] AudioClip[] audioClipArray;
            AudioClip lastClip;

            GameManager gameManager;

            private void Start()
            {
                gameManager = FindObjectOfType<GameManager>();
                sounds = GetComponents<AudioSource>();
                #region sons
                music60 = sounds[0];
                music80 = sounds[1];
                music100 = sounds[2];
                music120 = sounds[3];

                coin = sounds[4];
                foule_negative = sounds[4];
                foule_positive = sounds[5];
                paper = sounds[6];
                #endregion


            }


            #region Functions pour lancer les sons
            public void PlayCoin()
            {
                coin.Play();
            }

            public void PlayFoule_N()
            {
                //foule_negative.Play();
                paper.Play();
            }

            public void PlayFoule_P()
            {
                foule_positive.Play();
            }

            public void PlayPaper()
            {
                foule_negative.Play();
            }


            public void PlayMusic()
            {
                if (gameManager.bpm == 60)
                {
                    music60.Play();
                }

                if (gameManager.bpm == 80)
                {
                    music80.Play();
                }

                if (gameManager.bpm == 100)
                {
                    music100.Play();
                }

                if (gameManager.bpm == 120)
                {
                    music120.Play();
                }

            }
            #endregion

            #region Functions pour arreter les sons

            public void StopMusic()
            {
                if (gameManager.bpm == 60)
                {
                    music60.Stop();
                }

                if (gameManager.bpm == 80)
                {
                    music80.Stop();
                }

                if (gameManager.bpm == 100)
                {
                    music100.Stop();
                }

                if (gameManager.bpm == 120)
                {
                    music120.Stop();
                }
            }
            #endregion
        }
    }
}
