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
            Cursor.visible = false;
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
        public void ApplyAudioClip(string name, AudioSource audioSource, BPM bpm)
        {
            bool isSelected = false;
            //Debug.Log(soundList.soundBpms);
            foreach (SoundBpm soundBpm in soundList.soundBpms)
            {
                if (soundBpm.name == name)
                {
                    switch (bpm)
                    {
                        case BPM.Slow:
                            audioSource.clip = soundBpm.sounds[0].clip;
                            audioSource.volume = soundBpm.sounds[0].volume;
                            break;
                        case BPM.Medium:
                            audioSource.clip = soundBpm.sounds[1].clip;
                            audioSource.volume = soundBpm.sounds[1].volume;
                            break;
                        case BPM.Fast:
                            audioSource.clip = soundBpm.sounds[2].clip;
                            audioSource.volume = soundBpm.sounds[2].volume;
                            break;
                        case BPM.SuperFast:
                            audioSource.clip = soundBpm.sounds[3].clip;
                            audioSource.volume = soundBpm.sounds[3].volume;
                            break;
                        default:
                            break;
                    }
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
