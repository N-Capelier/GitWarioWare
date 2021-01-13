using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDMIMalediction : MonoBehaviour
{
    public GameObject hdmiPanel;

    public float curseLifeTime;
    public float flickerRate;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Curse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HDMI()
    {
        yield return new WaitForSeconds(curseLifeTime / flickerRate);
        if (!isActive)
        {
            hdmiPanel.SetActive(true);
            isActive = true;
        }
        else
        {
            hdmiPanel.SetActive(false);
            isActive = false;
        }

        StartCoroutine(HDMI());
    }

    private IEnumerator Curse()
    {
        StartCoroutine(HDMI());
        yield return new WaitForSeconds(curseLifeTime);
        gameObject.SetActive(false);
    }
}
