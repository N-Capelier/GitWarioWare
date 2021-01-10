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
        public class HitMark : MonoBehaviour
        {
            public SpriteRenderer markShot;
            public SpriteRenderer markDontShot;
            private void OnTriggerEnter2D(Collider2D collision)
            {
                NormalCrabBehaviour ncb = collision.GetComponent<NormalCrabBehaviour>();
                CrabParrotBehavior cpb = collision.GetComponent<CrabParrotBehavior>();

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

