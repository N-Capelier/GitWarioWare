using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DEUTSCHMANN Lucas
/// </summary>

namespace RadioRTL
{
    namespace EatingContest
    {
       
        public class ButtonsOpacity : MonoBehaviour
        {
            public string inputName;
            public Sprite normal;
            public Sprite lowOpacity;
            SpriteRenderer spriteRenderer;
            // Start is called before the first frame update
            void Start()
            {
                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            }

            // Update is called once per frame
            void Update()
            {
                if (Input.GetButton(inputName))
                {
                    spriteRenderer.sprite = normal;
                }
                else
                {
                    spriteRenderer.sprite = lowOpacity;
                }
            }
        }
    }
}