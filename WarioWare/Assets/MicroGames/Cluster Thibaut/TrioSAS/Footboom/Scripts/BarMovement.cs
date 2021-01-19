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
        
        public class BarMovement : MonoBehaviour
        {
            public float barSpeed;
            public float modifierSlow;
            public float modifierFast;
            public float modifierVeryFast;
            // Update is called once per frame
            void Update()
            {
                transform.Translate(Vector3.up * barSpeed * Time.deltaTime);


            }

            void OnCollisionEnter2D(Collision2D col)
            {
                barSpeed = -barSpeed;
            }

            public void speedSlow()
            {
                barSpeed = barSpeed * modifierSlow;
            }

            public void speedMedium()
            {
                
            }

            public void speedFast()
            {
                barSpeed = barSpeed*modifierFast;
            }

            public void speedVeryFast()
            {
                barSpeed = barSpeed*modifierVeryFast;
            }
        }
    }
}
