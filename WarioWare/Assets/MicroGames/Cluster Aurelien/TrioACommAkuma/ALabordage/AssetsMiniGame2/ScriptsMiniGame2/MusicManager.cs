using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrioName
{
    namespace MiniGameName
    {
        public class MusicManager : TimedBehaviour
        {
            [Header("AudioSources")]
            public AudioSource[] sourceList;


            public override void Start()
            {
                base.Start(); //Do not erase this line!
                switch (bpm)
                {
                    case 60:
                        sourceList[0].Play();
                        break;
                    case 90:
                        sourceList[1].Play();
                        break;
                    case 120:
                        sourceList[2].Play();
                        break;
                    case 140:
                        sourceList[3].Play();
                        break;
                }
            }
        }
    }
 
}