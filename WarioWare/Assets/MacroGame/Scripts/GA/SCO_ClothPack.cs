using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewClothPack", menuName = "GameArtist/ClothPack")]
public class SCO_ClothPack : ScriptableObject
{
    //List à edit (ne pas oublie d'edit aussi le script de base)
    [Space(10)]
    public Sprite leftArm, rightArm, chest, legs, leftFoot, rightFoot /*,ajout*/;
}
