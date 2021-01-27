using System.Collections;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace ChasseAuTresor
    {

        /// <summary>
        /// Noé Blanc
        /// </summary>

        public class PlayerTimedController : TimedBehaviour
        {
            private bool canMove;

            public float distance;
            private float speed;
            private float footstepRotation;
            private int randomNumber;
            

            public GameObject treasure;
            public GameObject target;
            public GameObject bulle;
            public GameObject spritePerso;
            public GameObject jumpParticles;
            public GameObject inputAppearance;

            public int inputNumber;
            public int inputMinNumber;

            public GameObject footstep1;
            public GameObject footstep2;
            public GameObject footstep3;
            public GameObject footstep4;

            private SpriteRenderer spriteRenderer;
            private AudioSource audiosource;

            public Sprite upSprite;
            public Sprite downSprite;
            public Sprite leftSprite;
            public Sprite rightSprite;
            public Sprite movingRightSprite;
            public Sprite movingLeftSprite;
            public Sprite movingUpSprite;
            public Sprite movingDownSprite;

            private Sprite movingSprite;
            private Sprite idleSprite;

            public float bpmAccelerator;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                canMove = true;

                distance = 0.8f;

                spriteRenderer = spritePerso.GetComponent<SpriteRenderer>();

                audiosource = gameObject.GetComponent<AudioSource>();

                bpmAccelerator = bpm / 60;

                target.transform.position = new Vector3(0, 0, 0);

                speed = 5f;

                idleSprite = downSprite;

                bulle.SetActive(false);

                inputNumber = 0;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                inputMinNumber = inputAppearance.GetComponent<InputsChoiceScript>().inputMinNumber;

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * bpmAccelerator * Time.deltaTime);

                if (treasure.GetComponent<WinOrLooseScript>().finished == false)
                {
                    if (Mathf.Abs(Input.GetAxisRaw("Left_Joystick_X")) <= 0.2 && Mathf.Abs(Input.GetAxisRaw("Left_Joystick_Y")) <= 0.2 && transform.position == target.transform.position)
                    {
                        canMove = true;
                    }
                }

                if(transform.position == target.transform.position)
                {
                    spriteRenderer.sprite = idleSprite;
                }
                else if (transform.position != target.transform.position)
                {
                    spriteRenderer.sprite = movingSprite;
                }
                

                if (canMove == true)
                {
                    if (Mathf.Abs(Input.GetAxisRaw("Left_Joystick_X")) >= 0.95f)
                    {
                        if (Input.GetAxisRaw("Left_Joystick_X") > 0f)
                        {
                            footstepRotation = -90;
                            target.transform.position += new Vector3(distance, 0, 0);
                            StartCoroutine(Jump());
                            movingSprite = movingRightSprite;
                            idleSprite = rightSprite;
                            audiosource.Play();
                            inputNumber = inputNumber + 1;
                        }
                        if (Input.GetAxisRaw("Left_Joystick_X") < 0f)
                        {
                            footstepRotation = 90;
                            target.transform.position += new Vector3(-distance, 0, 0);
                            StartCoroutine(Jump());
                            movingSprite = movingLeftSprite;
                            idleSprite = leftSprite;
                            audiosource.Play();
                            inputNumber = inputNumber + 1;
                        }

                        canMove = false;
                    }

                    if (Mathf.Abs(Input.GetAxisRaw("Left_Joystick_Y")) >= 0.95f)
                    {
                        if (Input.GetAxisRaw("Left_Joystick_Y") > 0f)
                        {
                            footstepRotation = 0;
                            target.transform.position += new Vector3(0, distance, 0);
                            StartCoroutine(Jump());
                            movingSprite = movingUpSprite;
                            idleSprite = upSprite;
                            audiosource.Play();
                            inputNumber = inputNumber + 1;
                        }
                        if (Input.GetAxisRaw("Left_Joystick_Y") < 0f)
                        {
                            footstepRotation = 180;
                            target.transform.position += new Vector3(0, -distance, 0);
                            StartCoroutine(Jump());
                            movingSprite = movingDownSprite;
                            idleSprite = downSprite;
                            audiosource.Play();
                            inputNumber = inputNumber + 1;
                        }
                        canMove = false;
                    }
                }

                if(transform.position == target.transform.position)
                {
                    audiosource.Stop();
                }

                if(transform.position == treasure.transform.position && inputNumber >= inputMinNumber)
                {
                    bulle.SetActive(true);
                }
                else
                {
                    bulle.SetActive(false);
                }

                if (treasure.GetComponent<WinOrLooseScript>().finished == true)
                {
                    canMove = false;
                }

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

            IEnumerator Jump()
            {
                for(float i = 0; i < (speed * bpmAccelerator * Time.deltaTime)/2; i += Time.deltaTime)
                {
                    spritePerso.transform.position += new Vector3(0, 0.05f, 0);
                    yield return null;
                }
                for (float i = 0; i < (speed * bpmAccelerator * Time.deltaTime) / 2; i += Time.deltaTime)
                {
                    spritePerso.transform.position += new Vector3(0, -0.05f, 0);
                    yield return null;
                }

                randomNumber = Random.Range(1, 5);

                if (randomNumber == 1)
                {
                    GameObject.Instantiate(footstep1, transform.position, Quaternion.Euler(0, 0, footstepRotation));
                }
                if (randomNumber == 2)
                {
                    GameObject.Instantiate(footstep2, transform.position, Quaternion.Euler(0, 0, footstepRotation));
                }
                if (randomNumber == 3)
                {
                    GameObject.Instantiate(footstep3, transform.position, Quaternion.Euler(0, 0, footstepRotation));
                }
                if (randomNumber == 4)
                {
                    GameObject.Instantiate(footstep4, transform.position, Quaternion.Euler(0, 0, footstepRotation));
                }

                Instantiate(jumpParticles, spritePerso.transform.position, Quaternion.identity);


                yield return null;
            }
        }
    }
}