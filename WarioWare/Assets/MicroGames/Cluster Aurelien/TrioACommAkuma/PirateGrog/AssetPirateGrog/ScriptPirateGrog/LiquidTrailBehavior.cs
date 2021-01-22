using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace PirateGrog
    {
        /// <summary>
        /// PARET Paul
        /// </summary>
        public class LiquidTrailBehavior : MonoBehaviour
        {
            private LineRenderer liquidRenderer;

            public Transform startPoint;
            public Transform endPoint;

            public float dropSpeed;

            private bool canCallStart = true;
            private bool canCallEnd = false;

            private void Awake()
            {
                liquidRenderer = this.GetComponent<LineRenderer>();
            }

            private void Update()
            {
                liquidRenderer.SetPosition(0, startPoint.position);
                liquidRenderer.SetPosition(1, endPoint.position);

                GetInput();

            }

            private void GetInput()
            {
                bool spaceIsPressed = Input.GetAxisRaw("Left_Joystick_Y") < -0.1f;
                bool spaceIsReleased = Input.GetAxisRaw("Left_Joystick_Y") > -0.01f;


                if (spaceIsPressed && canCallStart)
                {
                    canCallStart = false;
                    StartDrop();
                    canCallEnd = true;
                }

                if (spaceIsReleased && canCallEnd)
                {
                    canCallEnd = false;
                    EndDrop();
                    canCallStart = true;
                }

            }

            private void StartDrop()
            {
                startPoint.localPosition = Vector2.zero;
                endPoint.localPosition = Vector2.zero;

                startPoint.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                endPoint.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                startPoint.GetComponent<Rigidbody2D>().gravityScale = 0f;
                endPoint.GetComponent<Rigidbody2D>().gravityScale = dropSpeed;
            }

            private void EndDrop()
            {
                startPoint.GetComponent<BoxCollider2D>().isTrigger = false;
                startPoint.GetComponent<Rigidbody2D>().gravityScale = dropSpeed;
            }

            //private void CollisionEvent()
            //{
            //    startPoint.GetComponent<BoxCollider2D>().isTrigger = true;

            //    startPoint.GetComponent<Rigidbody2D>().gravityScale = 0f;
            //    endPoint.GetComponent<Rigidbody2D>().gravityScale = 0f;

            //    startPoint.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //    endPoint.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            //    startPoint.position = Vector2.zero;
            //    endPoint.position = Vector2.zero;

            //    startPoint.GetComponent<LiquidCollisionDetection>().asCollided = false;
            //}
        }
    }
}

