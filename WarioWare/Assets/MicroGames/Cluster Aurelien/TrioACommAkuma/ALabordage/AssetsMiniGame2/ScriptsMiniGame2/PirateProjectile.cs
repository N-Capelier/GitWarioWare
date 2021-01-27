using UnityEngine;

namespace ACommeAkuma
{
    namespace ALAbordage
    {
        /// <summary>
        /// Simon PICARDAT
        /// </summary>
        public class PirateProjectile : TimedBehaviour
        {
            public Vector2 dir;
            public float angle;

            public Rigidbody2D rb;
            public Vector2 poseProj;


            public override void Start()
            {
                base.Start(); //Do not erase this line!
                rb = gameObject.GetComponent<Rigidbody2D>();
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                poseProj = new Vector2(transform.position.x, transform.position.y);
                base.FixedUpdate(); //Do not erase this line!
                dir = rb.velocity - poseProj;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation =  Quaternion.AngleAxis( angle, Vector3.forward);
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

        }
    }
}