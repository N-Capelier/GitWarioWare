using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    public Button menu;
    public Toggle fullScreen;

    private void Start()
    {
        fullScreen.Select();
    }
    private void Update()
    {
        if (Input.GetButtonDown("B_Button"))
        {
            menu.Select();
        }
    }
    public void FullScreen(bool isFullSCreen)
    {
        if(isFullSCreen)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
    }

    public void Volume(float volume)
    {
        AudioListener.volume = volume;
    }
}
