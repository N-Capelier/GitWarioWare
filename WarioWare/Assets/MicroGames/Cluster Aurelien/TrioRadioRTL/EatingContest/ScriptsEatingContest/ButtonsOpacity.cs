using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

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
            public PlayerController playerController;
            public bool revert;
            // Start is called before the first frame update
            void Start()
            {
                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            }

            // Update is called once per frame
            void Update()
            {
                if (Manager.Instance.currentDifficulty == Difficulty.EASY || Manager.Instance.currentDifficulty == Difficulty.MEDIUM)
                {
                    if (!revert)
                    {
                        if (playerController.rottenPlate)
                        {
                            spriteRenderer.sprite = normal;
                        }
                        else
                        {
                            spriteRenderer.sprite = lowOpacity;
                        }
                    }
                    else
                    {
                        if (playerController.rottenPlate)
                        {
                            spriteRenderer.sprite = lowOpacity;
                        }
                        else
                        {
                            spriteRenderer.sprite = normal;
                        }
                    }
                    
                }
                else
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
}