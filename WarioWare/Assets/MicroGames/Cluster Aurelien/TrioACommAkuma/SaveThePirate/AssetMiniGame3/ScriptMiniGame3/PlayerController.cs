using System.Collections;
using UnityEngine;
using Caps;

namespace ACommeAkuma
{
    namespace SaveThePirate
    {

        /// <summary>
        /// Paul PARET
        /// </summary>
        public class PlayerController : TimedBehaviour
        {
            #region variables
            [Header("Player Movement")]
            public float rotationSpeed;
            
            [Header("TickEvent")]
            public float boostStrengh;
            public float velocityLossSpeed;

            [Header("Debug")]
            [SerializeField] private float rotationDir;
            public bool asWin;
            [SerializeField] private GameObject audioManagerGO;
            [SerializeField] private GameObject speedOverlayGO;

            [Header("Prefab References")]
            public GameObject exploVfx;

            [Header("Scene GO References")]
            public GameObject goalGO;

            private float rBumperHold = 0f;
            private float lBumperHold = 0f;
            private Rigidbody2D playerRb;
            private float velocityLoss;
            private bool canApplyForce;
            private GameObject motorGO;
            private GameObject vfxAnchor;
            private bool canImpactSFX = true;
            #endregion

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                audioManagerGO = GameObject.Find("AudioManagerSTP");
                speedOverlayGO = GameObject.Find("SpeedOverlay");

                playerRb = GetComponent<Rigidbody2D>();
                rotationDir = 0f;
                asWin = false;
                motorGO = transform.GetChild(1).gameObject;
                vfxAnchor = motorGO.transform.GetChild(0).gameObject;

                switch (bpm)
                {
                    case 60:
                        audioManagerGO.GetComponent<AudioManagerScript>().PlayMusic60();
                        break;
                    case 80:
                        audioManagerGO.GetComponent<AudioManagerScript>().PlayMusic90();
                        break;
                    case 100:
                        audioManagerGO.GetComponent<AudioManagerScript>().PlayMusic120();
                        break;
                    case 120:
                        audioManagerGO.GetComponent<AudioManagerScript>().PlayMusic140();
                        break;
                }
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                if (Tick <= 8 && !asWin)
                {
                    GetInput();
                    RotateMotor();

                    if (canApplyForce)
                    {
                        ApplyForce();
                    }
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick < 8 && !asWin)
                {
                    ActivateExplo();
                    canApplyForce = true;
                    velocityLoss = 1f;
                }

                else if (Tick == 8)
                {
                    if (asWin)
                    {
                        Manager.Instance.Result(true);
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                    }
                }
            }

            private void GetInput()
            {
                rBumperHold = Input.GetAxis("Right_Trigger");

                lBumperHold = Input.GetAxis("Left_Trigger");

                DirManager();
                ApplyTorque();
            }

            private void DirManager()
            {
                if (lBumperHold > rBumperHold)
                {
                    rotationDir = -1f * (Mathf.Exp(lBumperHold - rBumperHold) - 1f);
                }
                else if (rBumperHold > lBumperHold)
                {
                    rotationDir = 1f * (Mathf.Exp(rBumperHold - lBumperHold) - 1f);
                }
                else if (rBumperHold == lBumperHold)
                {
                    rotationDir = 0f;
                }
            }

            private void ApplyTorque()
            {
                playerRb.AddTorque((rotationDir * rotationSpeed), ForceMode2D.Force);
            }

            /// <summary>
            /// Apply a force that slowly decrease with time.
            /// </summary>
            private void ApplyForce()
            {
                playerRb.AddForce(transform.TransformDirection(Vector2.right) * boostStrengh * velocityLoss , ForceMode2D.Force);

                if (velocityLoss > 0f)
                    velocityLoss -= velocityLossSpeed * Time.deltaTime;

                else
                    canApplyForce = false;
            }

            /// <summary>
            /// Rotate the motor at the back of the boat depending one the ratation of the boat.
            /// </summary>
            private void RotateMotor()
            {
                if (lBumperHold > 0 && rBumperHold <= 0)
                    motorGO.transform.localEulerAngles = new Vector3(0f, 0f, 25f * lBumperHold);

                else if (lBumperHold <= 0 && rBumperHold > 0)
                    motorGO.transform.localEulerAngles = new Vector3(0f, 0f, -25f * rBumperHold);

                else if ((lBumperHold > 0 && rBumperHold > 0) || (lBumperHold <= 0 && rBumperHold <= 0))
                    motorGO.transform.localEulerAngles = Vector3.zero;
            }

            private void ActivateExplo()
            {
                //VFX
                Instantiate(exploVfx, vfxAnchor.transform.position, Quaternion.identity, vfxAnchor.transform);
                speedOverlayGO.GetComponent<SpeedLinesScript>().StartAnim();

                //SFX
                audioManagerGO.GetComponent<AudioManagerScript>().PlayExploSFX();
                audioManagerGO.GetComponent<AudioManagerScript>().PlayMoveSound();
            }


            private void OnTriggerEnter2D(Collider2D other)
            {
                if (other.gameObject.tag == "Finish" && !asWin)
                {
                    asWin = true;

                    //VFX
                    goalGO.transform.GetChild(0).GetComponent<Animator>().SetBool("AsWin", true);

                    playerRb.velocity = Vector2.zero;
                    playerRb.angularVelocity = 0f;
                    playerRb.isKinematic = true;
                }
                
            }

            private void OnCollisionEnter2D(Collision2D other)
            {
                if (other.gameObject.tag == "Wall" && canImpactSFX)
                {
                    canImpactSFX = false;
                    audioManagerGO.GetComponent<AudioManagerScript>().PlayImpactSFX();
                    StartCoroutine(CooldownDown());
                }
            }

            private IEnumerator CooldownDown()
            {
                yield return new WaitForSeconds(0.3f);

                canImpactSFX = true;
            }
        }
    }
}