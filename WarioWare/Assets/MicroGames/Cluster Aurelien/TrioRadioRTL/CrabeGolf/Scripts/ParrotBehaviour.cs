using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadioRTL
{
    /// <summary>
    /// Théo Valet
    /// </summary>

    namespace CrabGolf
    {
        public class ParrotBehaviour : TimedBehaviour
        {
            private Vector3 target;
            public float speed;
            private Vector3 position;
            public bool isShot;
            bool isFlying;
            int collisionState;
            public GameObject exploPlume;
            AudioSource volePerroquet;
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

                if (gameObject.transform.position == target)
                {
                    if (isFlying)
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
                    }
                    Destroy(gameObject);
                }

                if (isShot)
                {
                    CrabSpawner.cs.lose = true;
                    if (!isFlying)
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        Instantiate(exploPlume, gameObject.transform.position, Quaternion.identity);
                        isFlying = true;
                    }

                    speed = 0.5f;
                }
            }
            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.name == ("Collider Bateau"))
                {
                    collisionState = 1;
                }
                if (collision.name == ("Collider Mer") && collisionState == 0)
                {
                    collisionState = 2;
                }
                if (collision.name == ("Collider Ciel") && collisionState != 1)
                {
                    collisionState = 3;
                }
            }
        }
    }
}