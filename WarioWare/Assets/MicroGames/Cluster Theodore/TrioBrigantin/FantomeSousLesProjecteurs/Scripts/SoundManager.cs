using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Brigantin
{
    namespace FantomeSousLesProjecteurs
    {
        public class SoundManager : MonoBehaviour
        {
            #region Variables

            public AudioClip backgroundMusic;
            public AudioClip victorySound;
            public AudioClip defeatSound;

            private AudioSource source;

            #endregion

            #region Unity methods

            private void Awake()
            {

            }

            private void Start()
            {
                source = GetComponent<AudioSource>();
            }

            private void Update()
            {

            }

            #endregion

            #region Methods

            public void PlayMusic()
            {
                if(!source.isPlaying && backgroundMusic != null)
                {
                    source.clip = backgroundMusic;
                    source.Play();
                }
            }

            public void StopMusic()
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }

            public void PlayVictory()
            {
                if (!source.isPlaying)
                {
                    source.clip = victorySound;
                    source.Play();
                }
            }

            public void PlayDefeat()
            {
                if (!source.isPlaying)
                {
                    source.clip = defeatSound;
                    source.Play();
                }
            }

            #endregion
        }
    }
}