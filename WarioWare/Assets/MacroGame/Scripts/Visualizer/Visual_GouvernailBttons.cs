using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual_GouvernailBttons : MonoBehaviour
{
    public RectTransform gouvernail = null;
    private RectTransform myButton = null;
    private float gouvAngle;

    private void Awake()
    {
        myButton = this.gameObject.GetComponent<RectTransform>();
        gouvAngle = gouvernail.rotation.eulerAngles.z;
    }

    void Update()
    {
        float nexGouvAngle = gouvernail.rotation.eulerAngles.z;

        float deltaRotation = -1* (nexGouvAngle - gouvAngle);

        //myButton.rotation.eulerAngles.Set(0,0,-gouvAngle);
        //myButton.rotation.SetEulerRotation(new Vector3 (0,0, -gouvAngle));
        //myButton.localEulerAngles.SetZ(-gouvAngle);
        //myButton.localRotation.SetEulerAngles(0, 0, -gouvAngle);
        myButton.Rotate(new Vector3(0, 0, deltaRotation));

        gouvAngle = nexGouvAngle;
    }
}
