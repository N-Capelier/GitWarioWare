using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Footboom
    {
        /// <summary>
        /// Steve Guitton
        /// </summary>
        
        public class SuccessZoneSize : MonoBehaviour
        {
            public RectTransform rectTransform;
            public float heightModifierEasy;
            public float heightModifierHard;
            public BoxCollider2D boxColli;
            

            void Start()
            {
                rectTransform = GetComponent<RectTransform>();
                boxColli = GetComponent<BoxCollider2D>();
            }

            public void SizeEasy()
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y * heightModifierEasy);
                boxColli.size = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
            }

            public void SizeHard()
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y * heightModifierHard);
                boxColli.size = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
            }

            public void SizeMedium()
            {
                boxColli.size = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
            }
        }
    }
}
