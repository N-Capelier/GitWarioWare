using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragons_Peperes
{
    namespace CestNotreTresor
    {
        /// <summary>
        /// Mael Ricou
        /// </summary>

        public class PlayerController : MonoBehaviour
        {

            [SerializeField] float speed = 10f;
            private float moveInput;

            //[SerializeField]bool canMove;

            Rigidbody2D rb;
            AudioManager audioManager;

            private void Start()
            {
                rb = GetComponent<Rigidbody2D>();
                audioManager = FindObjectOfType<AudioManager>();

                audioManager.PlayRunning();
                audioManager.PlayAmbiance();
            }

            private void Update()
            {
                //Debug.Log(Input.GetAxis("Left_Joystick_X"));

                if(Mathf.Abs(moveInput) < 0.5 && Mathf.Abs(moveInput) > -0.5)
                {
                    speed = 0f;
                }
                else
                {
                    speed = 10f;
                }
            }

            private void FixedUpdate()
            {
                MovePlayer();
            }

            private void MovePlayer()
            {
                moveInput = Input.GetAxisRaw("Left_Joystick_X");
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }

        }
    }
}

   
