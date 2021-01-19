using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Soupe
{
    namespace EcraseMouche
    {
        /// <summary>
        /// Arthur Galland
        /// </summary>
        public class SoundManagerMouche : Singleton<SoundManagerMouche>
        {
            public AudioSource globalMusic;
            public AudioSource[] sfxSound;

            private void Awake()
            {
                CreateSingleton();
            }

        }
    }
}