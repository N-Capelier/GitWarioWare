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
            public AudioSource explosion;


            public void Start()
            {
                anim = gameObject.GetComponent<Animation>();
            }

            public void ActivateAnimation()
            {
                anim.Play();
                launch.Play();
            }   
            
            public void ActivateWin()
            {
                anim.Play();
                explosion.Play();
            }
        }
    }
}