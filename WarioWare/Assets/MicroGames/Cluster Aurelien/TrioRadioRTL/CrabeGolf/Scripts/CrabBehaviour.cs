using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testing;

namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>
    namespace CrabGolf
    {
        public class CrabBehaviour : TimedBehaviour
        {
            private Vector3 target;
            public float speed;
            private Vector3 position;
            public bool isShot;
            bool isFlying;
            int collisionState;
            public GameObject etoile;
            public GameObject goutte;
            public GameObject explosion;
            float BPM;

            public override void Start()
            {
                base.Start();

                target = new Vector3(8, -3, 0);
                position = gameObject.transform.position;
                BPM = PlayerBehavior.Instance.BPM;
            }

            void Update()
            {
                float step = (speed * Time.deltaTime) * BPM;
                transform.position = Vector3.MoveTowards(transform.position, target, step);

                if (gameObject.transform.position.x >= 3f && !isShot)
                {
                    CrabSpawner.cs.lose = true;
                    Debug.Log("perd");
                }

                if (gameObject.transform.position == target)
                {
                    switch (collisionState)
                    {
                        case 1:
                            FindObjectOfType<AudioManager>().Play("Explosion Bateau");
                            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

                            break;
                        case 2:
                            FindObjectOfType<AudioManager>().Play("Crabe dans l'eau");
                            Instantiate(goutte, gameObject.transform.position, Quaternion.identity);
                            break;

                        case 3:
                            FindObjectOfType<AudioManager>().Play("Crabe dans le ciel");
                            Instantiate(etoile, gameObject.transform.position, Quaternion.identity);
                            break;
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
