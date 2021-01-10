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

            private MeshRenderer meshRenderer;


            private void Awake()
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }

            private void Update()
            {
                Scroll();
            }

            private void Scroll()
            {
                yScroll = Time.time * scrollSpeed;

                Vector2 offset = new Vector2(0f, yScroll);
                meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
            }
        }
    }
}
