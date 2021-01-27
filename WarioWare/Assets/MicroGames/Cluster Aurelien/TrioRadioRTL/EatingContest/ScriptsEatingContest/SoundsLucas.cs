using UnityEngine.Audio;
using UnityEngine;
using Caps;


namespace RadioRTL
{
    /// <summary>
    /// DEUTSCHMANN Lucas
    /// </summary>

    namespace EatingContest
    {
        [System.Serializable]
        public class SoundsLucas
        {
            public string name;

            public AudioClip clip;

            [Range(0f, 1f)]
            public float volume;
            [Range(.1f, 3f)]
            public float pitch;

            public bool loop;

            [HideInInspector]
            public AudioSource source;
        }

    }
}
