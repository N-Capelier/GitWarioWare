using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACommeAkuma
{
    namespace PirateGrog
    {
        public class LiquidWidthcontroler : Singleton<LiquidWidthcontroler>
        {
            private void Awake()
            {
                CreateSingleton();
            }

            public LineRenderer lineWidth;
        }
    }
}
