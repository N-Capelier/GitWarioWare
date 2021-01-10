using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>
    namespace CrabGolf
    {
        public class NormalCrabBehaviour : TimedBehaviour
        {
            private Vector3 target;
            public float speed;
            private Vector3 position;
            public bool isShot;
            bool isFlying;
            int collisionState;

            public override void Start()
            {
                base.Start();

                target = new Vector3(8, -3, 0);
                position = gameObject.transform.position;
            }

            void Update()
            {
                float step = (speed * Time.deltaTime)* bpm;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
               // FindObjectOfType<AudioManager>().Play("Déplacement Crabe");

                if (gameObject.transform.position == target)
                {
                    switch (collisionState)
                    {
                        case 1:
                            FindObjectOfType<AudioManager>().Play("Explosion Bateau");

                            break;
                        case 2:
                            FindObjectOfType<AudioManager>().Play("Crabe dans l'eau");
                            break;

                        case 3:
                            FindObjectOfType<AudioManager>().Play("Crabe dans le ciel");
                            break;
                    }

                    if (!isShot)
                    {
                        CrabSpawner.cs.lose = true;
                    }

                    Destroy(gameObject);
                }

                if (isShot)
                {
                    if (!isFlying)
                    {
                        target = new Vector3(Random.Range(-8f, 8f), Random.Range(2f,5f), 0f);
                        isFlying = true;
                    }

                    speed = 0.5f;
                }
            }
            private void OnTriggerEnter2D(Collider2D collision)
            {
                Debug.Log("test");
                if (collision.name == ("Collider Bateau"))
                {
                    collisionState = 1;
                    Debug.Log("bateau");
                }
                if (collision.name == ("Collider Mer") && collisionState == 0)
                {
                    collisionState = 2;
                    Debug.Log("mer");
                }
                if (collision.name == ("Collider Ciel") && collisionState != 1)
                {
                    collisionState = 3;
                    Debug.Log("ciel");
                }
            }
        }
    }
}
