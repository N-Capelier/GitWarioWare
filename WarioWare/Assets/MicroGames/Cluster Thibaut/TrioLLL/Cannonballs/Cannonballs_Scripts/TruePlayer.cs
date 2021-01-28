using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioLLL
{
    namespace Cannonballs
    {
        public class TruePlayer : TimedBehaviour
        {
            [SerializeField]
            private float speed;
            [SerializeField]
            private float speedModifier;
            [SerializeField]
            private float bpmDiviser;
            private Rigidbody2D rb;
            private Animator animator;
            public GameObject Face;
            public GameObject Dos;
            public AudioClip footPrints;
            public AudioClip boom;
            public AudioClip splatter;
            public Soundmanager Audiomanager;
            [HideInInspector] public List<Gabarits> gabarits = new List<Gabarits>();
            private bool hasExploded = false;
            private bool footprintOn = false;

            public override void Start()
            {
                animator = gameObject.GetComponent<Animator>();
                speedModifier = bpm/bpmDiviser ;
                base.Start(); //Do not erase this line!
                rb = GetComponent<Rigidbody2D>();
                //gabarits = FindObjectsOfType<TrioLLL.Cannonballs.Gabarits>();
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
                    foreach (Gabarits gabarit in gabarits)
                    {

                        if (!gabarit.isPlayerOut && !hasExploded)
                        {
                            Face.SetActive(true);
                            Dos.SetActive(false);
                            animator.SetBool("Boom", true);
                            
                            Audiomanager.PlaySFX(splatter,2);
                        }
                    }
                    if (hasExploded == false)
                    {
                        Audiomanager.PlaySFX(boom, 1);
                        hasExploded = true;
                    }


                }


            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }
            private void Move()
            {
                float inputHorizontal = Input.GetAxis("Left_Joystick_X");
                float inputVertical = Input.GetAxis("Left_Joystick_Y");
                rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * (speed * speedModifier);
                //animations
                if (inputVertical > 0)
                {

                    Dos.SetActive(true);
                    Face.SetActive(false);
                    animator.SetBool("RunNorth", true);
                    animator.SetBool("RunSouth", false);
                    animator.SetBool("Immobile", false);

                }
                if (inputVertical <= 0)
                {

                    Face.SetActive(true);
                    Dos.SetActive(false);
                    animator.SetBool("RunNorth",false);
                    animator.SetBool("RunSouth",true);
                    animator.SetBool("Immobile",false);


                }
                if (inputVertical == 0 && inputVertical == 0)
                {

                    Face.SetActive(true);
                    Dos.SetActive(false);
                    animator.SetBool("Immobile",true);
                    animator.SetBool("RunNorth", false);
                    animator.SetBool("RunSouth", false);
                    
                }
                //sons
                if (inputVertical != 0 || inputHorizontal !=0)
                {
                    if(inputVertical <= 0) 
                    {
                        Face.SetActive(true);
                        Dos.SetActive(false);
                        animator.SetBool("RunNorth", false);
                        animator.SetBool("RunSouth", true);
                        animator.SetBool("Immobile", false);
                    }

                    if (footprintOn == false)
                    {
                        StartCoroutine("FootPrints");
                        footprintOn = true;
                    }
                }

            }
            IEnumerator FootPrints()
            {
                Audiomanager.PlayFootPrints(footPrints,1);
                yield return new WaitForSeconds(0.3f);
                footprintOn = false;
            }

        }
    }
}