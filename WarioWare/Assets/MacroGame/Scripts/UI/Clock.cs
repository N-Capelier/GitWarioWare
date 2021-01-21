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
        public Slider rope;
        public Sprite explosionBarel;
        public Sprite initialBarrel;
        public Image barrel;
        private bool doOnce;
        private void Start()
        {
            bpm = (float)Manager.Instance.bpm;
            
            
        }
        private void Update()
        {
            if (!doOnce)
            {
                timer += Time.deltaTime;
                rope.value =1- timer * bpm / 60 / 8;
                if(timer*bpm/60 >= 7.8f)
                {
                    doOnce = true;
                    barrel.sprite = explosionBarel;
                }
            }
        }

        public void Reset()
        {
            doOnce = false;
            barrel.sprite = initialBarrel;
            doOnce = false;
            bpm = (float)Manager.Instance.bpm;
            timer = 0;
        }

    }

}



