using UnityEngine;

public class CharaFaceSwap : MonoBehaviour
{
    [Header("Vêtements")]
    public SCO_CharaFace FacePack;

    [Header("Emplacements"), SerializeField]
    private SpriteRenderer Face;

    [Header("Anim"), SerializeField]
    private Animator animator;
    public Animation happy; 
    public Animation sad; 
    public Animation iddle; 
    public Animation angry; 

    void Awake()
    {
        ChangeFace(FacePack);
    }

    public void ChangeFace(SCO_CharaFace _FacePack)
    {
        //List à edit 
        Face.sprite = _FacePack.faceNeutral;
    }
}
