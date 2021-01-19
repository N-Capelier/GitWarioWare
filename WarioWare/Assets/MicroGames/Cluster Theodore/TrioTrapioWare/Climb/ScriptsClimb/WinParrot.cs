using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapioWare
{
    namespace Climb
    {
        public class WinParrot : MonoBehaviour
        {
            public void Win()
            {
                ClimbGameManager.Instance.animationDone = true;
            }
        }

    }
}
