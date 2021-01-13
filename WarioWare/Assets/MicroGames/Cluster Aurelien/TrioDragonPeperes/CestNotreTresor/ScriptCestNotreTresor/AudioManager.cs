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

            private AudioSource running;
            private AudioSource ambiance;
            private AudioSource catched;

            [SerializeField] AudioClip[] audioClipArray;
            AudioClip lastClip;

            private void Start()
            {
                sounds = GetComponents<AudioSource>();

                running = sounds[0];
                ambiance = sounds[1];
                catched = sounds[2];
                
                
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
            #endregion
        }
    }
}
