using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LeRafiot
{
    namespace UnDeuxTroisRequin
    {
        public class SharkManager : TimedBehaviour
        {

            #region Variables
            [Header ("Active le systeme de spawn du requin")]
            public bool sharkSysteme;

            [Header("Values Script")]
            public int tickBeforeSpawn;
            public int tickSharkStay;
            public int startTick;

            [Header ("True si requin présent")]
            public bool sharkIsHere;

            [Header ("Object à renseigner")]
            public GameObject sharkPrefab;
            public Transform spawnStart;
            public Transform spawnEnd;

            [Header ("UI")]
            public Image sign;
            public Image signStroke;
            public Image warningSign;

            [Header("Alert")]
            public Vector2 startScale;
            public Vector2 finalScale;

            [Header("Script")]
            public RopeController ropeControllerScript;

            private GameObject actualShark;

            private float counterTick;
            private int counterTickShark;
            private bool canFlash;

            private bool lockSpawn;
            public SoundManager123Requin sound;

            #endregion

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                canFlash = false;
                sign.gameObject.SetActive(false);
                signStroke.gameObject.SetActive(false);
                warningSign.gameObject.SetActive(false);

                counterTick = 0;
                counterTickShark = 0;
            }


            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!    

                SharkFlash();

                if (ropeControllerScript.win)
                {
                    canFlash = false;
                    sign.gameObject.SetActive(false);
                    signStroke.gameObject.SetActive(false);
                    warningSign.gameObject.SetActive(false);
                }
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                SharkSpawn();
            }

            void SharkFlash()
            {              
                if (counterTick > 0 && counterTick != tickBeforeSpawn)                
                {
                    if (canFlash)
                    {
                        canFlash = false;

                        sign.gameObject.SetActive(true);
                        signStroke.gameObject.SetActive(true);
                        warningSign.gameObject.SetActive(true);

                        StartCoroutine(IncreaseScale(warningSign ,startScale, finalScale, (1f * (60 / bpm))));
                    }
                }
            }

            void SharkSpawn()
            {
                if (sharkSysteme == true)
                {
                    if (Tick == startTick)
                    {
                        canFlash = true;
                    }

                    if (Tick >= startTick && counterTick != tickBeforeSpawn)
                    {
                        counterTick++;
                    }

                    if (counterTick == tickBeforeSpawn)
                    {                  
                        if (counterTickShark < tickSharkStay)
                        {
                            counterTickShark++;

                            if (lockSpawn == false && !ropeControllerScript.win)
                            {
                                sound.sfxSound[3].Play();
                                //sign.gameObject.SetActive(true);
                                lockSpawn = true;
                                //sharkIsHere = true;
                                actualShark = Instantiate(sharkPrefab, spawnStart.transform.position, spawnStart.transform.rotation);
                                actualShark.SetActive(true);
                                StartCoroutine(MoveToPosition(actualShark.transform, spawnEnd.transform.position, (tickSharkStay * (60 / bpm))));
                            }
                        }
                        else
                        {
                            sign.gameObject.SetActive(false);
                            signStroke.gameObject.SetActive(false);
                            warningSign.gameObject.SetActive(false);
                            //sharkIsHere = false;
                            if (actualShark != null)
                            {
                                actualShark.SetActive(false);
                            }
                        }
                    }
                }
            }

          

            private void OnTriggerEnter2D(Collider2D col)
            {
                if (col.CompareTag("Enemy1"))
                {
                    sharkIsHere = true;
                }
            }
            private void OnTriggerExit2D(Collider2D col)
            {
                if (col.CompareTag("Enemy1"))
                {
                    sharkIsHere = false;
                }
            }

            public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
            {
                if(transform != null)
                {
                    Vector3 currentPos = transform.position;
                    float t = 0;
                    while (t < 1)
                    {
                        t += Time.deltaTime / timeToMove;
                        transform.position = Vector3.Lerp(currentPos, position, t);
                        yield return null;
                    }
                }             
            }

            public IEnumerator IncreaseScale(Image objectToScale, Vector2 scale, Vector2 endScale, float timeToFade)
            {
                objectToScale.rectTransform.localScale = startScale;
                Vector2 currentScale = scale;
                float t = 0;
                while (t < 1)
                {
                    t += Time.deltaTime / timeToFade;
                    objectToScale.rectTransform.localScale = Vector3.Lerp(currentScale, endScale, t);
                    yield return null;
                }
            }
        }
    }
}