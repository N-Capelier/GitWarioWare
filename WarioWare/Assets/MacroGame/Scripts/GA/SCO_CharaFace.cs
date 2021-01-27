using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewCharaFaces", menuName = "GameArtist/CharaFaces")]
public class SCO_CharaFace : ScriptableObject
{
    //List à edit (ne pas oublie d'edit aussi le script de base)
    [Space(10)]
    public Sprite faceHappy, faceSad, faceNeutral, faceAngry;

}
