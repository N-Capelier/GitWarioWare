using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioLLL
{
    namespace Cannonballs
    {
        public class Gabarits : TimedBehaviour
        {
            [SerializeField]
            private float baseSpeed;
            [SerializeField]
            private float speedModifier;
            [SerializeField]
            private float bpmDiviser;
            public GameObject Player;
            private Rigidbody2D rb;
            public bool isPlayerOut = true;
            public bool randomized = false;
            public float min_x = -15f;
            public float max_x = 15f;
            public float min_y = -5f;
            public float max_y = 5f;
            public Vector2 nextPosition;
            public GameObject Explosion;
            public AudioClip boom;
            public AudioClip splatter;
            public TrioLLL.Cannonballs.Soundmanager Audiomanager;
            public float volumeBoom =1f;
            public float volumeSplatter =1f;
            public override void Start()
            {
                Explosion.SetActive(false);
                base.Start(); //Do not erase this line!
                speedModifier = bpm / bpmDiviser;
                rb = GetComponent<Rigidbody2D>();
                nextPosition = new Vector2(Random.Range(min_x, max_x), Random.Range(min_y, max_y));
                Player = GameObject.FindGameObjectWithTag("Player");
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                if (Tick < 7)
                {
                    Move();
                }

                if (Tick >= 7)
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 7)
                {
                    Explosion.SetActive(true);
                }
            }

            private void Move()
            {
                if (randomized)
                {
                    if (Vector2.Distance(transform.position, nextPosition) <= 0.01f)
                    {
                        nextPosition = new Vector2(Random.Range(min_x, max_x), Random.Range(min_y, max_y));
                    }
                    transform.position = Vector2.MoveTowards(transform.position, nextPosition, baseSpeed * speedModifier);
                    //Vector2 newDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    //Debug.Log(newDirection);
                    //rb.velocity = newDirection.normalized * (baseSpeed * speedModifier);
                    //Debug.Log(rb.velocity);
                }
                else
                {
                    rb.velocity = (Player.transform.position - transform.position).normalized * (baseSpeed * speedModifier);
                }


            }
            private void OnTriggerStay2D(Collider2D collision)
            {
                if (collision.tag == "Player")
                {
                    //Audiomanager.PlaySFX(splatter, volumeSplatter);
                    isPlayerOut = false;
                    
                }


                else return;
            }
            private void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.tag == "Player")
                    isPlayerOut = true;
                else return;
            }
        }
    }
}