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
        public class NewScrolling : MonoBehaviour
        {
            public float scrollSpeed = 0.1f;
            public float spriteVerticalLength;

            public Vector2 originalPos;

            private void Update()
            {
                Scroll();
            }

            private void Scroll()
            {
                if(transform.position.y < originalPos.y - spriteVerticalLength)
                {
                    transform.position = originalPos + new Vector2(0, spriteVerticalLength);
                }

                transform.position = new Vector2(originalPos.x, transform.position.y - scrollSpeed * Time.deltaTime);
            }
        }
    }
}
