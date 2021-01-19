using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Footboom
    {
        public class AnimateCannonBall : MonoBehaviour
        {
            public Animation anim;
            public AudioSource launch;


            public void Start()
            {
                anim = gameObject.GetComponent<Animation>();
                launch = gameObject.GetComponent<AudioSource>();
                
            }

            public void ActivateAnimation()
            {
                anim.Play();
                launch.Play();
            }

            
        }
    }
}