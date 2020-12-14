using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
[CreateAssetMenu(fileName = "New SoundList", menuName = "SoundList", order = 50)]
[System.Serializable]
public class SoundList : ScriptableObject
{
    [SerializeField] public List<SoundClassic> soundClassic = new List<SoundClassic>();
    [SerializeField] public List<SoundBpm> soundBpms = new List<SoundBpm>();
}
[System.Serializable]
public class SoundClassic
{
    public string name = string.Empty;
    public float volume = 0.5f;
    public AudioClip clip = default;
}
[System.Serializable]
public class SoundBpm
{
    public List<SoundClassic> sounds = new List<SoundClassic>();
    public List<BPM> bpm = new List<BPM>();
    public string name;
    public SoundBpm()
    {
        name = "NewSound";

        for (int i = 0; i < 4; i++)
        {
            sounds.Add(new SoundClassic());
            sounds[i].clip = default;            
            sounds[i].name = string.Empty;
            sounds[i].volume = 0.5f;
            switch (i)
            {
                case 0:
                    bpm.Add(BPM.Slow);
                    break;
                case 1:
                    bpm.Add(BPM.Medium);
                    break;
                case 2:
                    bpm.Add(BPM.Fast);
                    break;
                case 3:
                    bpm.Add(BPM.SuperFast);
                    break;
                default:
                    break;
            }
        }

    }
}
