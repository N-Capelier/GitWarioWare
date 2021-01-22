using UnityEngine;
using System;


namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>

    namespace CrabGolf
    {
        public class AudioManager : MonoBehaviour
        {
            public Sounds[] sounds;
            public AudioSource gestionSon;

            private void Awake()    
            {
                foreach (Sounds s in sounds)
                {
                    s.source = gameObject.AddComponent<AudioSource>();
                    s.source.clip = s.clip;

                    s.source.volume = s.volume;
                    s.source.pitch = s.pitch;
                    s.source.loop = s.loop;
                }
            }
            public void Play (string name)
            {
                Sounds s = Array.Find(sounds, sound => sound.name == name);
                s.source.Play();
                gestionSon.clip = s.source.clip;
            }
        }
    }
}

