﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sound;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{

    public GameObject loadingScreen;
    public Button playButton;
    public GameObject optionScreen;
    public Visual_GouvernailRotation gouvernail;
    public Slider loadingBar;
    //private bool canLoad;
    private AsyncOperation loading;
    public AudioSource mainMusic;
    public AudioSource buttonSounds;

    public EventTrigger[] buttonEventTriggers;
    private void Start()
    {
        SoundManager.Instance.ApplyAudioClip("Menu", mainMusic);
        mainMusic.PlaySecured();

        for (int i = 0; i < buttonEventTriggers.Length; i++)//link onSelect to all buttons
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((data) => { OnSelect(); });
            buttonEventTriggers[i].triggers.Add(entry);
        }
    }

   
    private void Update()
    {
        //disable because the scen is to fast to load
       /* if (canLoad)
        {
            loadingBar.value = loading.progress;
            if (loading.progress>=0.9f)
                loading.allowSceneActivation = true;
        }*/
    }

    private void OnSelect()
    {
        SoundManager.Instance.ApplyAudioClip("Selected", buttonSounds);
        buttonSounds.PlaySecured();
    }
    public void Play()
    {
        SoundManager.Instance.ApplyAudioClip("NewGame", buttonSounds);
        EventSystem.current.enabled = false;
        buttonSounds.PlaySecured();
        StartCoroutine(PlayEnumerator());
    }

    public void LoadFreeMode()
    {
        SoundManager.Instance.ApplyAudioClip("Clicked", buttonSounds);
        buttonSounds.PlaySecured();
        StartCoroutine(LoadFreeModeEnumerator());
    }

    public void Quit()
    {
        SoundManager.Instance.ApplyAudioClip("Clicked", buttonSounds);
        buttonSounds.PlaySecured();
        StartCoroutine(QuitEnumerator());
    }
    public void JukeBox()
    {
        SoundManager.Instance.ApplyAudioClip("Clicked", buttonSounds);
        buttonSounds.PlaySecured();
        StartCoroutine(LoadJukeBoxEnumerator());
    }

    private IEnumerator PlayEnumerator()
    {
        loadingScreen.SetActive(true);
        loading = SceneManager.LoadSceneAsync("WorldMap");
        loading.allowSceneActivation = false;
        //canLoad = true;
        for (float i = 0; i < 1; i+= 0.01f)
        {
            loadingBar.value = i;
            yield return new WaitForSeconds(buttonSounds.clip.length/100);
        }
        loading.allowSceneActivation = true;
    }
    public IEnumerator QuitEnumerator()
    {
        yield return new WaitForSeconds(buttonSounds.clip.length);
        Application.Quit();
    }
    public IEnumerator LoadFreeModeEnumerator()
    {
        yield return new WaitForSeconds(buttonSounds.clip.length);
        SceneManager.LoadScene("FreeMode");
    }
    public IEnumerator LoadJukeBoxEnumerator()
    {
        yield return new WaitForSeconds(buttonSounds.clip.length);
        SceneManager.LoadScene("JukeBox");
    }

    public void OptionOn()
    {
        optionScreen.SetActive(true);
        gouvernail.gameObject.SetActive(false);
    }

    public void OptionOff()
    {
        optionScreen.SetActive(false);
        gouvernail.gameObject.SetActive(true);
        playButton.Select();
    }
}
