using UnityEngine;

public class PersoFaceSwaper : MonoBehaviour
{
    [Header("Visage")]
    public SCO_FacePack FacePack;

    [Header("Emplacements"), SerializeField]
    private SpriteRenderer topHair;

    //List à edit (ne pas oublie d'edit aussi le SCO)

    [SerializeField]
    private SpriteRenderer botHair, leftEye, rightEye, leftEyebrow, rightEyebrow, mouth, chin, ear, neck /*,ajout*/;

    void FixedUpdate()
    {
        ChangeFace(FacePack);
    }

    public void ChangeFace(SCO_FacePack FacePack)
    {
        //List à edit 
        // ajout.sprite = ClothPack.ajout
        topHair.sprite = FacePack.topHair;
        botHair.sprite = FacePack.botHair;
        leftEye.sprite = FacePack.leftEye;
        rightEye.sprite = FacePack.rightEye;
        leftEyebrow.sprite = FacePack.leftEyebrow;
        rightEyebrow.sprite = FacePack.rightEyebrow;
        mouth.sprite = FacePack.mouth;
        chin.sprite = FacePack.chin;
        ear.sprite = FacePack.ear;
        neck.sprite = FacePack.neck;

    }
}