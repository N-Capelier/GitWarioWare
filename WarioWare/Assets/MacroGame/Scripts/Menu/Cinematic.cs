using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class Cinematic : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    public string nextSceneName;
    private bool loading = false;
    public GameObject buttonSprite;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
        StartCoroutine(NextScene());
        StartCoroutine(SkipVideo());

    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds((float)videoClip.length);
        if(!loading)
        {
            SceneManager.LoadScene(nextSceneName);
            loading = true;
        }
    }

    private IEnumerator SkipVideo()
    {
        yield return new WaitUntil(() => Input.GetButtonDown("A_Button"));
        buttonSprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => Input.GetButtonDown("A_Button"));
        if (!loading)
        {
            SceneManager.LoadScene(nextSceneName);
            loading = true;
        }
    }
}
