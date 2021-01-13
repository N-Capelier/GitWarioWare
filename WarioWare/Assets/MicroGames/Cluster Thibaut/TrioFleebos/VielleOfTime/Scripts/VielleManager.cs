using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fleebos
{
    namespace VielleOfTime
    {
        /// <summary>
        /// Arthur SPELLANI
        /// </summary>
        
        public class VielleManager : TimedBehaviour
        {
            Vector2 joystickRotation;
            Vector2 currentJoystickRotation;

            public Animator animManivelle;
            public Animator animPoignée;          

            AudioSource drone;

            public Image turnSignal;

            [HideInInspector] public bool turnActivation = false;


            public override void Start()
            {
                base.Start(); //Do not erase this line!

                joystickRotation = new Vector2(1,1);
                turnSignal.color = Color.red;
                drone = GetComponentInChildren<AudioSource>();
                animManivelle.speed = 0;
                animPoignée.speed = 0;
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                currentJoystickRotation = new Vector2(Input.GetAxis("Left_Joystick_X"), Input.GetAxis("Left_Joystick_Y"));

                if (turnSignal.fillAmount > 0)
                {
                    turnSignal.fillAmount -= Time.deltaTime / 2;
                    animManivelle.speed = turnSignal.fillAmount;
                    animPoignée.speed = turnSignal.fillAmount;
                }      
                
                if(turnSignal.fillAmount > 0.85f && turnActivation == false)
                {
                    TurnIsOn();
                    animManivelle.speed = 1;
                    animPoignée.speed = 1;
                }
                if (turnSignal.fillAmount < 0.85f && turnActivation == true)
                {
                    TurnIsOff();
                    animManivelle.speed = turnSignal.fillAmount;
                    animPoignée.speed = turnSignal.fillAmount;
                }

                if (Vector2.Angle(currentJoystickRotation, joystickRotation) > 45 && Vector2.Angle(currentJoystickRotation, joystickRotation) < 100)
                {
                    turnSignal.fillAmount += 0.1f;
                    joystickRotation = currentJoystickRotation;
                }

                drone.volume = turnSignal.fillAmount;

            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

            public void TurnIsOn()
            {
                turnActivation = true;
                turnSignal.color = Color.white;
            }

            public void TurnIsOff()
            {
                turnActivation = false;
                turnSignal.color = Color.red;
            }
        }
    }
}