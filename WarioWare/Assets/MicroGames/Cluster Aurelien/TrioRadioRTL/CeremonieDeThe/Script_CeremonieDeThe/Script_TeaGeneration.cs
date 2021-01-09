using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RadioRTL
{

    namespace CeremonieDeThe {

        public class Script_TeaGeneration : MonoBehaviour
        {

            /// <summary>
            /// Riwan HERVOUET
            /// </summary>
            /// 

            //1-Variables
            //1.1- Prefab
            public GameObject waterDrop;

            //1.2- Numbers
            public float waterDropBySeconds;
            private float currentCoolDown;
            private float coolDown;

            //1.3- Transform
            public Transform spawner;


            // Start is called before the first frame update
            void Start()
            {

                coolDown = 1 / waterDropBySeconds;

            }

            // Update is called once per frame
            void FixedUpdate()
            {

                coolDown -= Time.fixedDeltaTime;

                if (coolDown <= 0)
                {

                    Instantiate(waterDrop, spawner.position, Quaternion.identity);

                    currentCoolDown = coolDown;

                }

            }
        }

    }

}
