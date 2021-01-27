using UnityEngine;

namespace Fleebos
{
    namespace BananaStrip
    {
        public class PeelBehaviour : TimedBehaviour
        {
            public GameObject popParticule;
            public override void Start()
            {
                base.Start(); //Do not erase this line!
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

            }

            public void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("Enemy2"))
                {
                    switch (currentDifficulty)
                    {
                        case Difficulty.EASY:
                            GameManagerBanana.gm.PeelEASY();
                            Instantiate(popParticule, transform.position, Quaternion.identity);
                            break;
                        case Difficulty.MEDIUM:
                            GameManagerBanana.gm.PeelMEDIUM();
                            Instantiate(popParticule, transform.position, Quaternion.identity);
                            break;
                        case Difficulty.HARD:
                            GameManagerBanana.gm.PeelHARD();
                            Instantiate(popParticule, transform.position,Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }
}