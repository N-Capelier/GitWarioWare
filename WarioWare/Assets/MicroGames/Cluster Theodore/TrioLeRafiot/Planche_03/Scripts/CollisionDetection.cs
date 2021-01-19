using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;


namespace LeRafiot
{
    namespace Planche
    {
        /// <summary>
        /// Guillaume Rogé
        /// This script trigger the detection of player
        /// </summary>

        public class CollisionDetection : TimedBehaviour
        {
            public bool enemyInZone;
            public bool playerInZone;

            private bool playerTouched;

            public override void Start()
            {
                base.Start();
                enemyInZone = false;
                playerInZone = false;

            }

            public override void FixedUpdate()
            {
                base.FixedUpdate();
            }

            public override void TimedUpdate()
            {
                if (Tick == 8 && !Manager.Instance.panel.activeSelf && playerTouched)
                {
                    Manager.Instance.Result(false);
                }
            }

            private void Update()
            {
                if(playerInZone && enemyInZone && !Manager.Instance.panel.activeSelf && !playerTouched)
                {
                    playerTouched = true;
                    RandomEnemySpawn.Instance.spawnDisabled = true;

                    PlayerController.Instance.canMove = false;
                    //Manager.Instance.Result(false);
                    SoundManagerPlanche.Instance.sfxSound[1].Play();
                }
            }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.tag == ("Ennemy1"))
                {
                    enemyInZone = true;
                }

                if (collision.gameObject.tag == ("Player"))
                {
                    playerInZone = true;
                }
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.gameObject.tag == ("Ennemy1"))
                {
                    enemyInZone = false;
                }

                if (collision.gameObject.tag == ("Player"))
                {
                    playerInZone = false;
                }
            }
        }

    }
}
