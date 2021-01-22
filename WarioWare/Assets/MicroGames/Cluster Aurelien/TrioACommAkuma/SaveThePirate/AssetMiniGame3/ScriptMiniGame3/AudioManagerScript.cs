using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace SaveThePirate
    {
        /// <summary>
        /// PARET Paul
        /// </summary>
        public class AudioManagerScript : MonoBehaviour
        {
            [Header("AudioSources")]
            public AudioSource[] sourceList;

            public void PlayExploSFX()
            {
                sourceList[0].Play();
            }
            public void PlayImpactSFX()
            {
                sourceList[1].Play();
            }
            public void PlayHelpSFX()
            {
                sourceList[2].Play();
            }
            public void PlayMusic60()
            {
                sourceList[3].Play();
            }
            public void PlayMusic90()
            {
                sourceList[4].Play();
            }
            public void PlayMusic120()
            {
                sourceList[5].Play();
            }
            public void PlayMusic140()
            {
                sourceList[6].Play();
            }
            public void PlayMoveSound()
            {
                sourceList[7].Play();
            }
            public void PlayThanksVoice()
            {
                sourceList[8].Play();
            }
        }
    }
}

