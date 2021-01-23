using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapioWare
{
    namespace Climb
    {
        public class flecheStart : MonoBehaviour
        {
            [SerializeField] private int repetition = 0;
            [SerializeField] private float timeToWait = 0;
            [SerializeField] private float timebetweenShow = 0;
            [SerializeField] private bool firstTime = false;

            // Start is called before the first frame update
            void Start()
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(StartFade());
            }

           IEnumerator StartFade()
            {
                if (!firstTime)
                {
                    firstTime = true;
                    yield return new WaitForSeconds(timeToWait);
                }

                for (int i = 0; i < repetition; i++)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    yield return new WaitForSeconds(timebetweenShow);
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    yield return new WaitForSeconds(timebetweenShow);
                }
               
            }
        }
    }
}



