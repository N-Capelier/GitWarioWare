using UnityEngine;
using Caps;

namespace ACommeAkuma
{
    namespace ALAbordage
    {
        /// <summary>
        /// Simon PICARDAT
        /// </summary>
        public class EnnemiBridge : TimedBehaviour
        {
            [HideInInspector] public bool win = false;
            public GameObject victoriousPirate;

            public AudioSource bonk;
            public AudioClip bonkClip;

            public override void Start()
            {
                base.Start(); //Do not erase this line!
                bonk = GetComponent<AudioSource>();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8 && win == false)
                    Manager.Instance.Result(false);
                if(Tick == 8 && win == true)
                    Manager.Instance.Result(true);
            }


            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.gameObject.CompareTag( "Projectile") && Tick < 8)
                {
                    win = true;
                    bonk.Play(0);
                    GameObject pirateInstance = Instantiate(victoriousPirate, other.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    Destroy(other.gameObject);
                }
            }
                private void OnCollisionEnter2D(Collision2D collision)
                {
                    bonk.Play(0);
                }
        }
    }
}