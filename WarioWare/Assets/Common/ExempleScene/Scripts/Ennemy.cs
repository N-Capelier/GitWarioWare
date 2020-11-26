using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace ExampleScene
{
    public class Ennemy : MonoBehaviour
    {
        [Range(0.1f,1f)]
        public float speed;
        //distance at wich the ennemy will hit the enney
        public float minDistance;

        [HideInInspector]
        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down*speed);

            TestPosition();

            if (transform.position.y <= -11)
            {
                Destroy(gameObject);
            }
        }

        // Raycast down and test if the ennemy is close enough to collid with the player
        private void TestPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            if (hit.collider != null)
            {
                if (transform.position.y - hit.transform.position.y <= minDistance)
                {
                    Manager.Instance.Result(false);
                }
            }
        }
    }

}
