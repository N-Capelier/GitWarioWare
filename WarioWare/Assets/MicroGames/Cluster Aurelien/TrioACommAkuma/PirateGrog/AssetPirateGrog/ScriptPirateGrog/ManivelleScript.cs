using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace PirateGrog
    {
        public class ManivelleScript : MonoBehaviour
        {
            private Animator manivelleAnim;
            private bool inputDetect;
            // Start is called before the first frame update
            void Start()
            {
                manivelleAnim = GetComponent<Animator>();

            }

            // Update is called once per frame
            void Update()
            {
                InputDetection();
            }

            private void InputDetection()
            {
                if (Input.GetAxisRaw("Left_Joystick_Y") < -0.1)
                {
                    manivelleAnim.SetBool("isManivelle", true);
                }
                else if (Input.GetAxisRaw("Left_Joystick_Y") > -0.01)
                {
                    manivelleAnim.SetBool("isManivelle", false);
                }

            }
        }
    }
}
