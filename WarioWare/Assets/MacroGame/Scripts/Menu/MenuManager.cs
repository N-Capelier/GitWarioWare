using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider loadingBar;
    private bool canLoad;
    private AsyncOperation loading;
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

    public void Play()
    {
        StartCoroutine(PlayEnumerator());
    }
    private IEnumerator PlayEnumerator()
    {
        loadingScreen.SetActive(true);
        loading = SceneManager.LoadSceneAsync("Zone1");
        loading.allowSceneActivation = false;
        canLoad = true;
        for (float i = 0; i < 1; i+= 0.01f)
        {
            loadingBar.value = i;
            yield return new WaitForSeconds(0.01f);
        }
        loading.allowSceneActivation = true;
    }
    public void Quite()
    {
        Application.Quit();
    }
}
