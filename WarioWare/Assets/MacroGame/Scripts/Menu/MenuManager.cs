using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sound;
public class MenuManager : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider loadingBar;
    //private bool canLoad;
    private AsyncOperation loading;
    private AudioSource mainMusic;

    public MenuButton play;
    private void Start()
    {
        mainMusic = GetComponent<AudioSource>();
        SoundManager.Instance.ApplyAudioClip("Menu", mainMusic);
        mainMusic.PlaySecured();
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

    public void Play(MenuButton button)
    {
        StartCoroutine(PlayEnumerator(button));
    }

    public void LoadFreeMode(MenuButton button)
    {
        StartCoroutine(LoadFreeModeEnumerator(button));
    }

    public void Quit(MenuButton button)
    {
        StartCoroutine(QuitEnumerator(button));
    }

    private IEnumerator PlayEnumerator(MenuButton button)
    {
        yield return new WaitForSeconds(button.playerInputs.clip.length);
        loadingScreen.SetActive(true);
        loading = SceneManager.LoadSceneAsync("Zone1");
        loading.allowSceneActivation = false;
        //canLoad = true;
        for (float i = 0; i < 1; i+= 0.01f)
        {
            loadingBar.value = i;
            yield return new WaitForSeconds(0.01f);
        }
        loading.allowSceneActivation = true;
    }
    public IEnumerator QuitEnumerator(MenuButton button)
    {
        yield return new WaitForSeconds(button.playerInputs.clip.length);
        Application.Quit();
    }
    public IEnumerator LoadFreeModeEnumerator(MenuButton button)
    {
        yield return new WaitForSeconds(button.playerInputs.clip.length);
        SceneManager.LoadScene("FreeMode");
    }
}
