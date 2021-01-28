using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Cinquante
    {
        public class HeadBobbing : TimedBehaviour
        {
            private bool coroutinePlay;

            [Header("Transform References")]
            public Transform headTransform;
            public Transform cameraTransform;

            [Header("Head Bobbing")]
            private float bobFrequency = 5f;
            public float bobHorizontalAmplitude = 0.3f;
            public float bobVerticalAmplitude = 0.3f;
            [Range(0, 1)] public float headBobSmoothing = 0.1f;

            //State 
            public bool isWalking;
            private float walkingTime;
            private Vector3 targetCameraPosition;

            
            public override void Start()
            {
                base.Start();

                

                switch (bpm)
                {

                    case (float)BPM.Slow:
                        bobFrequency = 5f;
                        break;

                    case (float)BPM.Medium:
                        bobFrequency = 6f;
                        break;

                    case (float)BPM.Fast:
                        bobFrequency = 8f;
                        break;

                    case (float)BPM.SuperFast:
                        bobFrequency = 10f;
                        break;
                }
            }
            

            //FixedUpdate is called on a fixed time.
            public void Update()
            {
                base.FixedUpdate(); //Do not erase this line!
                
                if (!isWalking) walkingTime = 0;
                else walkingTime += Time.deltaTime;

                targetCameraPosition = headTransform.position + CalculateHeadBobOffsett(walkingTime);
                
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);
                
                if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001)
                    cameraTransform.position = targetCameraPosition;
                   
            }

            private Vector3 CalculateHeadBobOffsett(float t)
            {
                float horizontalOffset = 0;
                float verticalOffset = 0;
                Vector3 offset = Vector3.zero;

                if (t > 0)
                {
                    horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizontalAmplitude;
                    verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmplitude;

                    offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
                }

                return offset;
            }

            IEnumerator isWalkingTrue()
            {
                isWalking = true;
                coroutinePlay = true;
                yield return new WaitForSeconds(0.4f);
                isWalking = false;
                coroutinePlay = false;
            }

            public void bobOn()
            {
                if (coroutinePlay == false)
                {
                    StartCoroutine(isWalkingTrue());
                }
            }

        }
    }
}