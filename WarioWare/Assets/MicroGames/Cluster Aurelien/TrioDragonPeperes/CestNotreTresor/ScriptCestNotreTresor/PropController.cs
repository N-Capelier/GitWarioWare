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
        public class PropController : MonoBehaviour
        {

            public float propSpeed = 6f;
            public float bound_Y;


            private void Update()
            {
                MoveProp();
            }
            void MoveProp()
            {

                //se déplace sur l'axe Y en négatif
                Vector2 temp = transform.position;
                temp.y -= propSpeed * Time.deltaTime;
                transform.position = temp;

                //si le prop dépasse un certain axe, se fait détruire
                if (temp.y < bound_Y)
                    Destroy(gameObject, 1);
            }
        }
    }
}
