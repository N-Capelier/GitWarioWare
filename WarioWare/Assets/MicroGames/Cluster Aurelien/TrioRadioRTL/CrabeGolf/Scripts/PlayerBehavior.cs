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
        public class PlayerBehavior : MonoBehaviour
        {
            Animator animator;
            public bool strikeUnlock = false;
            public float animFrappe;
            bool isInAnim;
            public bool canShoot = true;
            public bool hit;
            public GameObject sable;

            private void Start()
            {
                animator = GetComponent<Animator>();
            }

            void Update()
            {
                if (canShoot)
                {
                    if (Input.GetButton("A_Button") && !hit)
                    {
                        strikeUnlock = true;
                        animator.SetBool("Prepare", true);
                    }
                    else if (Input.GetButtonUp("A_Button") && strikeUnlock && !hit)
                    {                        
                        animator.SetBool("Shoot", true);
                        strikeUnlock = false;
                        StartCoroutine(ResetHit());
                    }
                    else if (!strikeUnlock)
                    {
                        animator.SetBool("Prepare", false);
                        animator.SetBool("Shoot", false);     
                    }
                }            
            }     
            IEnumerator ResetHit()
            {
                hit = true;

                yield return new WaitForSeconds(animFrappe);
                hit = false;
            }
            
            private void OnTriggerStay2D(Collider2D other)
            {
                if (hit)
                {
                        NormalCrabBehaviour ncb = other.GetComponent<NormalCrabBehaviour>();
                        CrabParrotBehavior cpb = other.GetComponent<CrabParrotBehavior>();

                        if (ncb != null)
                        {
                            ncb.isShot = true;
                            FindObjectOfType<AudioManager>().Play("Coup de Golf");
                            Instantiate(sable, new Vector3(gameObject.transform.position.x-1.5f,gameObject.transform.position.y,0), Quaternion.identity);
                    }
                        else if (cpb != null)
                        {
                            cpb.isShot = true;
                            FindObjectOfType<AudioManager>().Play("Frappe Perroquet");
                            canShoot = false;
                            StartCoroutine(Lose());

                        }
                    IEnumerator Lose()
                    {
                        yield return new WaitForSeconds(1);
                        animator.SetBool("Lose", true);
                        FindObjectOfType<AudioManager>().Play("Défaite");
                    }

                }
                
            }
          
        }
    }
  
}
