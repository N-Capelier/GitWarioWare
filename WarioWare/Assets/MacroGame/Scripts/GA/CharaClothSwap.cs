using UnityEngine;

public class CharaClothSwap : MonoBehaviour
{
    [Header("Vêtements")]
    public SCO_CharaCloth ClothPack;

    [Header("Emplacements"), SerializeField]
    private SpriteRenderer hat;

    [SerializeField]
    private SpriteRenderer chest, leftArm, rightArm, legs;

    void FixedUpdate()
    {
        ChangeCloth(ClothPack);
    }

    public void ChangeCloth(SCO_CharaCloth ClothPack)
    {
        //List à edit 
        hat.sprite = ClothPack.hat;
        chest.sprite = ClothPack.chest;
        leftArm.sprite = ClothPack.leftArm;
        rightArm.sprite = ClothPack.rightArm;
        legs.sprite = ClothPack.legs;
    }
}
