using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

[CreateAssetMenu(menuName ="Malediction/Silence")]
public class SilenceMalediction : Malediction
{
    public override void StartMalediction()
    {
        Manager.Instance.cantDisplayVerbe = true;
    }

    public override void StopMalediction()
    {
        Manager.Instance.cantDisplayVerbe = false;
    }
}
