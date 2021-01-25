using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Cinquante
    {

        public class AnimationScript : MonoBehaviour
        {

            public Animation InputAnimation;

            public void Start()
            {
                InputAnimation = gameObject.GetComponent<Animation>();
            }

            public void StartScene()
            {
                InputAnimation.Play();
            }
        }
    }
}