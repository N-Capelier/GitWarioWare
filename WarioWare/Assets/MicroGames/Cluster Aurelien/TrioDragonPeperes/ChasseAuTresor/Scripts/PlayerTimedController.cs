using System.Collections;
using System.Collections.Generic;
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

            public GameObject treasure;
            public GameObject target;

            private SpriteRenderer spriteRenderer;

            public Sprite upSprite;
            public Sprite downSprite;
            public Sprite leftSprite;
            public Sprite rightSprite;

            public float bpmAccelerator;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                canMove = true;

                distance = 0.8f;

                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

                bpmAccelerator = bpm / 60;

                target.transform.position = new Vector3(0, 0, 0);

                speed = 5f;

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * bpmAccelerator * Time.deltaTime);

                if (treasure.GetComponent<WinOrLooseScript>().finished == false)
                {
                    if (Mathf.Abs(Input.GetAxisRaw("Left_Joystick_X")) <= 0.2 && Mathf.Abs(Input.GetAxisRaw("Left_Joystick_Y")) <= 0.2 && transform.position == target.transform.position)
                    {
                        canMove = true;
                    }
                }

                if (canMove == true)
                {
                    if (Mathf.Abs(Input.GetAxisRaw("Left_Joystick_X")) >= 0.95f)
                    {
                        if (Input.GetAxisRaw("Left_Joystick_X") > 0f)
                        {
                            target.transform.position += new Vector3(distance, 0, 0);                            
                            spriteRenderer.sprite = rightSprite;
                        }
                        if (Input.GetAxisRaw("Left_Joystick_X") < 0f)
                        {
                            target.transform.position += new Vector3(-distance, 0, 0);
                            spriteRenderer.sprite = leftSprite;
                        }

                        canMove = false;
                    }

                    if (Mathf.Abs(Input.GetAxisRaw("Left_Joystick_Y")) >= 0.95f)
                    {
                        if (Input.GetAxisRaw("Left_Joystick_Y") > 0f)
                        {
                            target.transform.position += new Vector3(0, distance, 0);
                            spriteRenderer.sprite = upSprite;
                        }
                        if (Input.GetAxisRaw("Left_Joystick_Y") < 0f)
                        {
                            target.transform.position += new Vector3(0, -distance, 0);
                            spriteRenderer.sprite = downSprite;
                        }
                        canMove = false;
                    }
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
        }
    }
}