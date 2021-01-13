using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Dragons_Peperes
{
    namespace ChasseAuTresor
    {
        /// <summary>
        /// Noé Blanc
        /// </summary>

        public class WinOrLooseScript : TimedBehaviour
        {
            public GameObject player;

            private SpriteRenderer spriteRenderer;
            public Sprite chest;

            public bool finished;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

                finished = false;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if(Tick == 8 && transform.position == player.transform.position)
                {
                    Debug.Log("Gagné !");
                    spriteRenderer.sprite = chest;
                    finished = true;
                    Manager.Instance.Result(true);
                }
                if(Tick == 8 && transform.position != player.transform.position)
                {
                    Debug.Log("Perdu !");
                    finished = true;
                    Manager.Instance.Result(false);
                }
            }
        }
    }
}