
using UnityEngine;
using UnityEngine.UI;


namespace JukeBox
{
    public class MusicFillAmount : MonoBehaviour
    {
        public Image imageToFill;
        private AudioSource mainAudioSource;
        private bool canFill;
        private float timer;

        // Update is called once per frame
        void Update()
        {
            if (canFill)
            {
                timer += Time.deltaTime;
                imageToFill.fillAmount = timer / mainAudioSource.clip.length;
                if(timer>= mainAudioSource.clip.length)
                {
                    ResetFilling();
                }
            }
        }

        public void StartFilling(AudioSource audioSource)
        {
            mainAudioSource = audioSource;
            canFill = true;
            timer = 0;
        }
        public void ResetFilling()
        {
            canFill = false;
            timer = 0;
            imageToFill.fillAmount = 0;
        }
    }

}

