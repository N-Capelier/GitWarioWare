using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UICameraController : MonoBehaviour
    {

        private float horizontalMove;
        private float verticalMove;

        public RectTransform mapCanvas;
        public Transform targetTransform;
        public Transform playerTransform;

        public float cameraSpeed;
        public float joystickDeadZone;

        // Update is called once per frame
        void Update()
        {
            horizontalMove = Input.GetAxis("Right_Joystick_X");
            verticalMove = Input.GetAxis("Right_Joystick_Y");

            if (Mathf.Abs(Input.GetAxis("Left_Joystick_X")) > joystickDeadZone || Mathf.Abs(Input.GetAxis("Left_Joystick_Y")) > joystickDeadZone || !IsInsideMap())
            {
                targetTransform.position = playerTransform.position;
            }
            
            if (Mathf.Abs(horizontalMove) < joystickDeadZone && Mathf.Abs(verticalMove) < joystickDeadZone)
            {
                targetTransform.position = EventSystem.current.currentSelectedGameObject.transform.position;
            }
            else
            {
                MoveTarget();
            }
        }

        void MoveTarget()
        {
            targetTransform.position += new Vector3(horizontalMove, verticalMove, 0) * cameraSpeed * Time.deltaTime;
        }

        private bool IsInsideMap()
        {
            if (Mathf.Abs(targetTransform.position.x) > mapCanvas.rect.width / 2 || Mathf.Abs(targetTransform.position.y) > mapCanvas.rect.height / 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

