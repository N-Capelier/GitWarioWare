using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Fleebos
{
    namespace LeMusicien
    {
        public class RotationBanjo : TimedBehaviour
        {
            public List<float> rotations;
            public bool rightTriggerPressed;
            public bool leftTriggerPressed;
            public float rotationValue;
            public float rotationPositions;

            public GameObject mouth;

            public Animator m_Animator;
            public float animatorBaseSpeed;

            public GameObject audiance;

            public AudioClip[] soundList;
            public AudioSource randomChordAS;
            public AudioSource crowd;
            public AudioSource cheers;
            public AudioSource boos;

            public ParticleSystem notesBanjo;
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                m_Animator = audiance.GetComponent<Animator>();
                animatorBaseSpeed = m_Animator.speed;
                m_Animator.speed = animatorBaseSpeed * (bpm / 60);

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        rotationValue = 60f;
                        rotationPositions = 360f/rotationValue;
                        break;
                    case Difficulty.MEDIUM:
                        rotationValue = 45f;
                        rotationPositions = 360f/rotationValue;
                        break;
                    case Difficulty.HARD:
                        rotationValue = 30f;
                        rotationPositions = 360f/rotationValue;
                        break;
                }

                for (int i = 0; i < rotationPositions; i++)
                {
                    rotations.Add(rotationValue * i);
                }

                int rand = Random.Range(1, rotations.Count);
                transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

                crowd.Play();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (Tick < 6 && rightTriggerPressed == false && Input.GetAxis("Right_Trigger") == 1f)
                {
                    transform.Rotate(new Vector3(0, 0, -rotationValue));
                    rightTriggerPressed = true;
                    RandomChord();
                    notesBanjo.Play();
                }

                if (Input.GetAxis("Right_Trigger") == 0f)
                {
                    rightTriggerPressed = false;
                }

                if (Tick < 6 && leftTriggerPressed == false && Input.GetAxis("Left_Trigger") == 1f)
                {
                    transform.Rotate(new Vector3(0, 0, rotationValue));
                    leftTriggerPressed = true;
                    RandomChord();
                    notesBanjo.Play();
                }

                if (Input.GetAxis("Left_Trigger") == 0f)
                {
                    leftTriggerPressed = false;
                }

                if (transform.eulerAngles.z > -1 && transform.eulerAngles.z < 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

                if (transform.eulerAngles.z == 0f)
                {
                    mouth.transform.localScale = new Vector3(transform.localScale.x * 0.75f, transform.localScale.y * -0.75f, transform.localScale.z * 0.75f);
                }
                else
                {
                    mouth.transform.localScale = new Vector3(transform.localScale.x * 0.75f, transform.localScale.y * 0.75f, transform.localScale.z * 0.75f);
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 6)
                {
                    crowd.Stop();

                    Debug.Log(transform.eulerAngles.z);
                    if (transform.eulerAngles.z == 0f)
                    {
                        Debug.Log("win");
                        cheers.Play();
                    }

                    else if (transform.eulerAngles.z != 0f)
                    {
                        Debug.Log("loose");
                        boos.Play();
                    }
                }

                if (Tick == 8)
                {
                    if (transform.eulerAngles.z == 0f)
                    {
                        Manager.Instance.Result(true);
                    }

                    else if (transform.eulerAngles.z != 0f)
                    {
                        Manager.Instance.Result(false);
                    }
                }
            }
            private void RandomChord()
            {
                int randomSound = Random.Range(0, soundList.Length - 1);
                randomChordAS.clip = soundList[randomSound];
                randomChordAS.Play();
            }
        }
    }
}