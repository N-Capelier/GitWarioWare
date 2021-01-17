using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

public class AppelMalediction : MonoBehaviour
{
    public  AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.ApplyAudioClip("Menu", audioSource);
       // SoundManager.Instance.ApplyAudioClip("AppelCurse", audioSource);
        audioSource.PlaySecured();
    }

}
