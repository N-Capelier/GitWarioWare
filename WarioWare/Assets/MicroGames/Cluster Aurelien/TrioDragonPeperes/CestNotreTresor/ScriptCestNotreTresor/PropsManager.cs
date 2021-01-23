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

            public GameObject spawnGauche;
            public GameObject spawnDroite;

            [Space]
            [Header("Stats")]
            public float timer = 3f;


            void Start()
            {
                Invoke("Spawner", timer);
            }


            void Spawner()
            {
                //à gauche

                if (Random.Range(0, 2) > 0)
                {
                    Instantiate(props[Random.Range(0, props.Length)], spawnGauche.transform.position, Quaternion.identity, spawnGauche.transform);
                }


                //à droite

                if (Random.Range(0, 2) > 0)
                {
                    Instantiate(props[Random.Range(0, props.Length)], spawnDroite.transform.position, Quaternion.identity, spawnDroite.transform);
                }


                Invoke("Spawner", timer);
            }
        }
    }
}
