using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SD_UsualAction;
namespace Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private SoundList soundList;
        private void Awake()
        {
            CreateSingleton(true);
        }
        public void ApplyAudioClip(string name, AudioSource audioSource)
        {
            bool isSelected = false;
            foreach (SoundClassic soundClassic in soundList.soundClassic)
            {
                if(soundClassic.name == name)
                {
                    audioSource.clip = soundClassic.clip;
                    audioSource.volume = soundClassic.volume;
                    isSelected = true;
                }
            }
            if (!isSelected)
            {
                Debug.LogError("No sound have this name");
            }
        }
    }
}
