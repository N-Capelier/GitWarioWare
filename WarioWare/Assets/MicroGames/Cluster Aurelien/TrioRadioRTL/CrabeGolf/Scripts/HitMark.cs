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
        public class HitMark : MonoBehaviour
        {
            public SpriteRenderer markShot;
            public SpriteRenderer markDontShot;
            private void OnTriggerEnter2D(Collider2D collision)
            {
                CrabBehaviour ncb = collision.GetComponent<CrabBehaviour>();
                ParrotBehaviour cpb = collision.GetComponent<ParrotBehaviour>();

                if (cpb != null)
                {
                    markDontShot.enabled = true;
                }
                else if (ncb != null)
                {
                    markShot.enabled = true;
                }

            }
            private void OnTriggerExit2D(Collider2D collision)
            {
                markShot.enabled = false;
                markDontShot.enabled = false;
            }
        }
    }
}

