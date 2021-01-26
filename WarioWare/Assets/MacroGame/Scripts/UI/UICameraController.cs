using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using Player;
using Caps;
using UnityEngine.UI;

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

        public CinemachineVirtualCamera uiVcam;
        public CinemachineVirtualCamera tacticalVcam;
        public Button invisibleButton;

        private void Start()
        {
            targetTransform.position = Manager.Instance.eventSystem.firstSelectedGameObject.transform.position; ; ;
        }

        // Update is called once per frame
        void Update()
        {
            if(uiVcam.gameObject.activeSelf)
            {
                horizontalMove = Input.GetAxis("Right_Joystick_X");
                verticalMove = Input.GetAxis("Right_Joystick_Y");
                if (!PlayerInventory.Instance.inventoryCanvas.activeSelf && Manager.Instance.cantDoTransition && EventSystem.current != null && EventSystem.current.enabled)
                {
                    if (Mathf.Abs(Input.GetAxis("Left_Joystick_X")) > joystickDeadZone || Mathf.Abs(Input.GetAxis("Left_Joystick_Y")) > joystickDeadZone || !IsInsideMap())
                    {

                        targetTransform.position = Manager.Instance.eventSystem.currentSelectedGameObject.transform.position;

                    }

                    MoveTarget();
                }
            }

            if(Input.GetButtonDown("Back_Button"))
            {
                if (uiVcam.gameObject.activeSelf)
                {
                    PlayerMovement.Instance.ShowSelectedIslandInfo(PlayerMovement.Instance.playerIsland);
                    uiVcam.gameObject.SetActive(false);
                    tacticalVcam.gameObject.SetActive(true);
                    invisibleButton.Select();
                    //PlayerMovement.Instance.ShowFarNeighboursIcon();
                }
                else if(!uiVcam.gameObject.activeSelf)
                {
                    uiVcam.gameObject.SetActive(true);
                    tacticalVcam.gameObject.SetActive(false);
                    PlayerMovement.Instance.playerIsland.button.Select();
                    PlayerMovement.Instance.HideIcons();
                }
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

