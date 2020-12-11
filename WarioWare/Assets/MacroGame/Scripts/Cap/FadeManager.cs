using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Caps
{
    [RequireComponent(typeof(Image))]
    public class FadeManager : MonoBehaviour
    {
        private Image fade;

        private void Start()
        {
            fade = GetComponent<Image>();
        }
        public IEnumerator FadeInAndOut(float timer)
        {
            for (float i = 0; i < timer/2; i+= 0.01f)
            {
                fade.color = new Color(0, 0, 0, i *2/ timer);
                yield return new WaitForSeconds(0.01f);
            }
            for (float i = 0; i < timer / 2; i += 0.01f)
            {
                fade.color = new Color(0, 0, 0,1-( i *2/ timer));
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
