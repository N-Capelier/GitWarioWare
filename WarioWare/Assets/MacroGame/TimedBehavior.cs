using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;
using UnityEngine.SceneManagement;

public class TimedBehavior : MonoBehaviour
{
    float bpm = 60;
    Manager.difficulty currentDifficulty = 0;
  
    private float timer;

    public virtual void Start()
    {
        if (SceneManager.GetActiveScene().name == "TestingScene")
        {
            bpm = Manager.Instance.bpm;
            currentDifficulty = Manager.Instance.currentDifficulty;
        }
    }


    public virtual void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 60 / bpm)
        {
            timer = 0;
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
