using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

[CreateAssetMenu(menuName ="Malediction/Silence")]
public class SilenceMalediction : Malediction
{
    public Sprite fakeImage;
    private Sprite previousImage;
    public override void StartMalediction()
    {
        Manager.Instance.cantDisplayVerbe = true;
        previousImage = Manager.Instance.inputImage.sprite;
        Manager.Instance.inputImage.sprite = fakeImage;
    }

    public override void StopMalediction()
    {
        Manager.Instance.cantDisplayVerbe = false;
        Manager.Instance.inputImage.sprite = previousImage;
    }
}
