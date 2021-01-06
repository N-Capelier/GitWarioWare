using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    [RequireComponent(typeof(Image))]
    public class FadeManager : Singleton<FadeManager>
    {
        private Image fade;

        private void Awake()
        {
            CreateSingleton();

        }
        private void Start()
        {
            fade = GetComponent<Image>();
        }
        public IEnumerator FadeIn(float timer)
        {
            fade.color = new Color(0, 0, 0, 0);
             
             for (float i = 0; i < timer; i+= 0.01f)
             {
                 fade.color = new Color(0, 0, 0, i / timer);
                 yield return new WaitForSeconds(0.01f);
             }
            fade.color = Color.black;
            NoPanel();
        }

        public IEnumerator FadeOut(float timer)
        {
            fade.color = Color.black;
            for (float i = 0; i < timer / 2; i += 0.01f)
            {
                fade.color = new Color(0, 0, 0, 1 - (i  / timer));
                yield return new WaitForSeconds(0.01f);
            }
            fade.color = new Color(0, 0, 0, 0);

        }
        public void NoPanel()
        {
            fade.color = new Color(0, 0, 0, 0);
        }
    }
}
