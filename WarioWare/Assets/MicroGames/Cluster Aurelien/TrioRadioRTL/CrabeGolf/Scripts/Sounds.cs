using UnityEngine.Audio;
using UnityEngine;
using Caps;


namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>

    namespace CrabGolf
    {
        [System.Serializable]
        public class Sounds
        {
            public string name;

            public AudioClip clip;

            [Range(0f, 1f)]
            public float volume;
            [Range (.1f, 3f)]
            public float pitch;

            public bool loop;

            [HideInInspector]
            public AudioSource source;
        }

    }
}