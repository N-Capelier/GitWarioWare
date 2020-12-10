using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UICameraController : MonoBehaviour
{

    private float horizontalMove;
    private float verticalMove;

    public CinemachineVirtualCamera UICamera;
    public RectTransform mapCanvas;
    public Transform targetTransform;
    public Transform playerTransform;
    public float cameraSpeed;
    public float joystickDeadZone;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Left_Joystick_X");
        verticalMove = Input.GetAxis("Left_Joystick_Y");

        if(Mathf.Abs(horizontalMove) < joystickDeadZone && Mathf.Abs(verticalMove) < joystickDeadZone)
        {
            targetTransform.position = playerTransform.position;
        }
        else
        {
            MoveTarget();
        }

        if(!IsInsideMap())
        {
            targetTransform.position = playerTransform.position;
        }
    }

    void MoveTarget()
    {
        targetTransform.position += new Vector3(horizontalMove, verticalMove, 0) * cameraSpeed * Time.deltaTime;
    }

    private bool IsInsideMap()
    {
        if(Mathf.Abs(targetTransform.position.x) > mapCanvas.rect.width / 2 || Mathf.Abs(targetTransform.position.y) > mapCanvas.rect.height / 2)
        {
            return false;
        }
        else
        {
            return true;
        }        
    }
}
