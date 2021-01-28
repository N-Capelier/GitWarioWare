using UnityEngine;

public class CharaFaceSwap : MonoBehaviour
{
    [Header("Vêtements")]
    public SCO_CharaFace FacePack;

    [Header("Emplacements"), SerializeField]
    private SpriteRenderer Face;

    void Awake()
    {
        FaceChangeNeutral();
    }

    public void FaceChangeNeutral()
    {
        Face.sprite = FacePack.faceNeutral;
    }
    public void FaceChangeHappy()
    {
        Face.sprite = FacePack.faceHappy;
    }
    public void FaceChangeAngry()
    {
        Face.sprite = FacePack.faceAngry;
    }
    public void FaceChangeSad()
    {
        Face.sprite = FacePack.faceSad;
    }
}
