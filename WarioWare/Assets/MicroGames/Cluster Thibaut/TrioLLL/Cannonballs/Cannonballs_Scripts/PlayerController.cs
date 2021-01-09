using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioLLL
{
    namespace CannonBalls
    {
        public class PlayerController : MonoBehaviour
        {
            public AudioClip footPrints;
            [SerializeField]
            private float speed;
            [SerializeField]
            private float speedModifier;

            private Rigidbody2D rb;

            // Start is called before the first frame update
            void Start()
            {
                rb = GetComponent<Rigidbody2D>();
            }

            // Update is called once per frame
            void Update()
            {
                Move();
            }

            private void Move()
            {

                float inputHorizontal = Input.GetAxis("Right_Joystick_X");
                float inputVertical = Input.GetAxis("Right_Joystick_Y");
                rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * (speed * speedModifier);
            }
        }
    }
}