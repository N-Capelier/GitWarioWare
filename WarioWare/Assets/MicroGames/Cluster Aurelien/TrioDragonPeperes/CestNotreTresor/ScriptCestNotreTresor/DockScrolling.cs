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
        public class DockScrolling : MonoBehaviour
        {
            public float scrollSpeed = 0.1f;
            private float yScroll;

            private SpriteRenderer spriteRenderer;

            public Material material;

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
            }
        }
    }
}
