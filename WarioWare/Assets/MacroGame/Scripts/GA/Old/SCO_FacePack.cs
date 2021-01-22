using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewFacePack", menuName = "GameArtist/FacePack")]
public class SCO_FacePack : ScriptableObject
{
    //List à edit (ne pas oublie d'edit aussi le script de base)
    [Space(10)]
    public Sprite topHair, botHair, leftEye, rightEye, leftEyebrow, rightEyebrow, mouth, chin, ear, neck /*,ajout*/;
}
