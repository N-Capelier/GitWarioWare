using UnityEngine;

namespace ACommeAkuma
{
    namespace ALAbordage
    {
        /// <summary>
        /// Simon PICARDAT
        /// </summary>
        public class CannonControler : TimedBehaviour
        {

            public GameObject anchorActuel;

            


            public float cannonForce;
            public float projectileGravity;
            public GameObject pirateProject;
            public GameObject smokeParticle;


            public int numberOfPoints;
            public GameObject PointPrefab;
            public GameObject[] Points;

            [HideInInspector] public bool isLaunched;
            [HideInInspector] public Vector2 initialVelocity;

            [HideInInspector] public float bpmGameAccelerator;

            [HideInInspector] public AudioSource cannonBlast;



            public override void Start()
            {
                base.Start(); //Do not erase this line!
                Points = new GameObject[numberOfPoints];
                for (int i = 0; i < numberOfPoints; i++)
                {
                    Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
                    Points[i].transform.parent = gameObject.transform;
                }
                bpmGameAccelerator = bpm / 60;
                cannonBlast = anchorActuel.GetComponent<AudioSource>();
            }

            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
                initialVelocity = anchorActuel.transform.position - gameObject.transform.position;


                SetTrajectoryPoints(anchorActuel.transform.position,initialVelocity.normalized*cannonForce);


            }

            public override void TimedUpdate()
            {
                if (Tick == 6)
                {
                    if (isLaunched == false)
                    {
                        LaunchPirate();
                        isLaunched = true;
                    }
                }
            }
            public void LaunchPirate()
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
                cannonBlast.Play(0);
                GameObject bulletInstance = Instantiate(pirateProject, anchorActuel.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                Rigidbody2D rbPirate;
                rbPirate = bulletInstance.GetComponent<Rigidbody2D>();
                bulletInstance.transform.rotation = gameObject.transform.rotation;
                bulletInstance.transform.Rotate(0,0,-90);
                rbPirate.gravityScale = projectileGravity * bpmGameAccelerator * bpmGameAccelerator;
                rbPirate.velocity = initialVelocity.normalized * cannonForce * bpmGameAccelerator;
                GameObject smokeInstance = Instantiate(smokeParticle, anchorActuel.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0))) as GameObject;
            }

            void SetTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
            {
                float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
                float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
                float fTime = 0;

                fTime += 0.1f;
                for (int i = 0; i < numberOfPoints; i++)
                {
                    float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f*projectileGravity);
                    Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
                    Points[i].transform.position = pos;
                    Points[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * projectileGravity * fTime, pVelocity.x * bpmGameAccelerator) * Mathf.Rad2Deg);
                    fTime += 0.1f;
                }
            }
        }
    }
}