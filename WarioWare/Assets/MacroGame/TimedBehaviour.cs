using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using UnityEngine.SceneManagement;

public class TimedBehaviour : MonoBehaviour
{
   [HideInInspector] public float bpm = 60;
    [HideInInspector] public Manager.difficulty currentDifficulty = 0;

   
   public double timer;
    public double currentTime;
    public virtual void Start()
    {
        if (SceneManager.GetActiveScene().name == "TestingScene")
        {
            bpm =(float) Manager.Instance.bpm;
            currentDifficulty = Manager.Instance.currentDifficulty;
        }
        currentTime = AudioSettings.dspTime;
    }


    public virtual void FixedUpdate()
    {
        timer = AudioSettings.dspTime - currentTime;
        if (timer >= 60 / bpm)
        {
            currentTime = AudioSettings.dspTime;
            TimedUpdate();
        }
    }

    /// <summary>
    /// TimedUdpate is call at each tic. Use this if you want your script to update with rithme
    /// </summary>
    public virtual void TimedUpdate()
    {

    }

}
