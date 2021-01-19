using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dragons_Peperes
{
    namespace CestNotreTresor
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>
        public class AudioManager : MonoBehaviour
        {
            private AudioSource[] sounds;
            public AudioSource audioSource;

            #region sons audiosource
            private AudioSource running;
            private AudioSource ambiance;
            private AudioSource catched;

            private AudioSource music60;
            private AudioSource music80;
            private AudioSource music100;
            private AudioSource music120;
            #endregion
            [SerializeField] AudioClip[] audioClipArray;
            AudioClip lastClip;

            EnemyManager gameManager;

            private void Start()
            {
                gameManager = FindObjectOfType<EnemyManager>();
                sounds = GetComponents<AudioSource>();
                #region sons
                running = sounds[0];
                ambiance = sounds[1];
                catched = sounds[2];

                music60 = sounds[3];
                music80 = sounds[4];
                music100 = sounds[5];
                music120 = sounds[6];
                #endregion


                PlayMusic();

            }

            AudioClip RandomClip()
            {
                int attempts = 3;
                AudioClip newClip = audioClipArray[Random.Range(0, audioClipArray.Length)];

                while (newClip == lastClip && attempts > 0)
                {
                    newClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
                    attempts--;
                }

                lastClip = newClip;
                return newClip;
            }

            #region Functions pour lancer les sons
            public void PlayRunning()
            {
                running.Play();
            }

            public void PlayAmbiance()
            {
                ambiance.Play();
            }

            public void PlayCatched()
            {
                catched.Play();
            }

            public void PlayRandomReplique()
            {
                audioSource.PlayOneShot(RandomClip());
            }

            public void PlayMusic()
            {
                if(gameManager.bpm == 60)
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
            public void StopRunning()
            {
                running.Stop();
            }

            public void StopAmbiance()
            {
                ambiance.Stop();
            }

            public void StopCatched()
            {
                catched.Stop();
            }

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
