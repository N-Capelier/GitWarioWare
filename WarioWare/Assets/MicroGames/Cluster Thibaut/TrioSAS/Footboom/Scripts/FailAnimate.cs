using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Footboom
    {

        public class FailAnimate : MonoBehaviour
        {
            public AudioSource fail;
            // Start is called before the first frame update
            void Start()
            {
                fail = gameObject.GetComponent<AudioSource>();
            }

            public void ActivateFail()
            {
                fail.Play();
            }
        }
    }
}
