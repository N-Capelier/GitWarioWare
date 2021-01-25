using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioSAS
{
    namespace Footboom
    {
        public class GetMusic : MonoBehaviour
        {
            public AudioSource mBPM60;
            public AudioSource mBPM80;
            public AudioSource mBPM100;
            public AudioSource mBPM120;

            public void Maestro60()
            {
                mBPM60.Play();
            }

            public void Maestro80()
            {
                mBPM80.Play();
            }

            public void Maestro100()
            {
                mBPM100.Play();
            }

            public void Maestro120()
            {
                mBPM120.Play();
            }
        }

        
    }
}