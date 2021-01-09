using UnityEngine;

public class PersoClothSwaper : MonoBehaviour
{
    [Header("Vêtements")]
    public SCO_ClothPack ClothPack;

    [Header("Emplacements"), SerializeField]
    private SpriteRenderer chest;
    
    //List à edit (ne pas oublie d'edit aussi le SCO)

    [SerializeField] 
    private SpriteRenderer leftArm, rightArm, legs, leftFoot, rightFoot /*,ajout*/;

    void FixedUpdate()
    {
        ChangeCloth(ClothPack);
    }

    public void ChangeCloth(SCO_ClothPack ClothPack)
    {
        //List à edit 
        // ajout.sprite = ClothPack.ajout
        chest.sprite = ClothPack.chest;
        leftArm.sprite = ClothPack.leftArm;
        rightArm.sprite = ClothPack.rightArm;
        legs.sprite = ClothPack.legs;
        leftFoot.sprite = ClothPack.leftFoot;
        rightFoot.sprite = ClothPack.rightFoot;
    }
}
