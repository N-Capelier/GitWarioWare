using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewCharaCloth", menuName = "GameArtist/CharaCloth")]
public class SCO_CharaCloth : ScriptableObject
{
    //List à edit (ne pas oublie d'edit aussi le script de base)
    [Space(10)]
    public Sprite hat, chest, leftArm, rightArm, legs;

}
