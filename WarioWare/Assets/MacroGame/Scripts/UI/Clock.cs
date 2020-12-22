using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using UnityEngine.UI;
namespace UI
{
    public class Clock : MonoBehaviour
    {
        [HideInInspector] public float timer;
        [HideInInspector] public float bpm;
        private Image clockImage;
        private void Start()
        {
            clockImage = GetComponent<Image>();
            bpm = (float)Manager.Instance.bpm;
        }
        private void Update()
        {
            timer += Time.deltaTime;
            clockImage.fillAmount = timer * bpm / 60 / 8;
        }

    }

}



