using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using Caps;

namespace RadioRTL
{
    /// <summary>
    /// Riwan HERVOUET
    /// </summary>

    namespace CeremonieDeThe
    {
        public class Script_SoundManager : MonoBehaviour
        {
            public Script_Sounds [] sounds;
            //public AudioSource [] gestionSon;

            /*private void Awake()
            {
                foreach (Script_Sounds s in sounds)
                {
                    s.source = gameObject.AddComponent<AudioSource>();
                    s.source.clip = s.clip;

                    s.source.volume = s.volume;
                    s.source.pitch = s.pitch;
                    s.source.loop = s.loop;
                }
            }*/
            public void Play(string name, int index)
            {
                Script_Sounds _sound = null;

                for (int i = 0; i < sounds.Length; i++)
                {
                    if (name == sounds[i].name)
                        _sound = sounds[i];
                }
                if(_sound == null)
                {
                    //Debug.LogError($"Sound not found: {name}");
                }
                _sound.source.clip = _sound.clip;
                _sound.source.Play();
                //_sound.source.loop = true;
                //gestionSon[index].clip = s.source.clip;
                //Debug.Log("Play sound : " + _sound.clip.name + " - on source: " + _sound.source.name);




                /*Element 0 musique
                 Element 1 Victoire & defaite
                Element 2 eau qui coule
                Element 3 bateau qui tangue
                Element 4 eau qui touche la tasse*/
            }

            public void Stop(string name)
            {
                
                Script_Sounds _sound = null;

                for (int i = 0; i < sounds.Length; i++)
                {
                    if (name == sounds[i].name)
                        _sound = sounds[i];
                }
                if (_sound == null)
                {
                    //Debug.LogError($"Sound not found: {name}");
                }

                _sound.source.Stop();

                //Debug.Log("AudioSource Stopped");
            }
        }
    }
}