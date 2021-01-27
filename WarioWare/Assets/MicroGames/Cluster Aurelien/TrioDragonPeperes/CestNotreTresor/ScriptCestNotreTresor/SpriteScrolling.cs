using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace CestNotreTresor
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>
        public class SpriteScrolling : MonoBehaviour
        {
            public float scrollSpeed = 0.1f;
            public Material material;
            private float yScroll;

            private SpriteRenderer spriteRenderer;


            private void Awake()
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            private void Update()
            {
                Scroll();
            }

            private void Scroll()
            {
                yScroll = Time.time * scrollSpeed;

                Vector2 offset = new Vector2(0f, yScroll);
                material.SetTextureOffset("_BaseMap", offset);
                Debug.Log(material.GetTextureOffset("_BaseMap"));
            }
        }
    }
}
