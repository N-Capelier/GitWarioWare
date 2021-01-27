using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Caps;

public class Verbe : MonoBehaviour
{
    public Image[] images = new Image[4];

    private void OnEnable()
    {
        StartCoroutine(VerbeCouroutine());
    }
    private IEnumerator VerbeCouroutine()
    {
        images[0].enabled = true;
        for (int i = 1; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
            images[i].enabled = true;
        }

        for (int i = 0; i < images.Length; i++)
        {
            //Debug.Log(Manager.Instance.transitionMusic.clip.length);
            yield return new WaitForSeconds(Manager.Instance.transitionMusic.clip.length / 4);
            if(i!=2)
                images[i].enabled = false;
            images[i + 1].gameObject.SetActive(true);
        }     
             

    }
}
