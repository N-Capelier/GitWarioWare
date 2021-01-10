using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace PirateGrog
    {
        /// <summary>
        /// PARET Paul
        /// </summary>
        public class LiquidCollisionDetection : MonoBehaviour
        {
            [HideInInspector] public bool asCollided = false;

            private void OnCollisionEnter2D(Collision2D other)
            {
                asCollided = true;
            }
        }
    }
}

