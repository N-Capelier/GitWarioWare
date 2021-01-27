using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadioRTL
{
    namespace EatingContest
    {
        public class Idle : MonoBehaviour
        {
            
            public float idleSpeed;
            bool goingUp;
            bool isRunning;
            
            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {
                if (!isRunning)
                {
                    StartCoroutine(ChangeDirection());
                }
                
                if (goingUp)
                {
                    transform.position += Vector3.up.normalized * idleSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position += Vector3.down.normalized * idleSpeed * Time.deltaTime;
                }
            }

            IEnumerator ChangeDirection()
            {
                isRunning = true;
                
                yield return new WaitForSeconds(1);
                goingUp = !goingUp;
                isRunning = false;
            }
        }
    }
}

