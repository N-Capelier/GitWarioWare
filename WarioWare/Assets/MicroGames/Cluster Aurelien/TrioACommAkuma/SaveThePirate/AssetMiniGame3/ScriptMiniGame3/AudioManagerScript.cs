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
        }
    }
}

