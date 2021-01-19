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
        public class PropsManager : MonoBehaviour
        {

            [SerializeField] GameObject[] props;


            [Space]
            [Header("Stats")]
            public float timer = 2f;

            [Space]
            [Header("Boundaries")]
            public float miniYGauche;
            public float maxYGauche;

            public float miniYDroite;
            public float maxYDroite;

            void Start()
            {
                Invoke("Spawner", timer);
            }


            void Spawner()
            {
                //à gauche
                float pos_Y_G = Random.Range(miniYGauche, maxYGauche);
                Vector3 tempG = transform.position;
                tempG.y = pos_Y_G;

                if (Random.Range(0, 2) > 0)
                {
                    Instantiate(props[Random.Range(0, props.Length)], tempG, Quaternion.identity);
                }


                //à droite
                float pos_Y_D = Random.Range(miniYDroite, maxYDroite);
                Vector3 tempD = transform.position;
                tempD.y = pos_Y_D;

                if (Random.Range(0, 2) > 0)
                {
                    Instantiate(props[Random.Range(0, props.Length)], tempD, Quaternion.identity);
                }


                Invoke("Spawner", timer);
            }
        }
    }
}
