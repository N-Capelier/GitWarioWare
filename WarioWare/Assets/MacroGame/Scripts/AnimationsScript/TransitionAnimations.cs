
using UnityEngine;
using Caps;
using DG.Tweening;


public class TransitionAnimations : MonoBehaviour
{
    public Animator charaAnimator;
    public GameObject ship;
    public Transform startPosition;
    public Transform endPosition;
    public GameObject barrel;
    public void DisplayBarrel(Cap cap)
    {
        var distance = Vector3.Distance(startPosition.position, endPosition.position);
        for (int i = 0; i < cap.hasBarrel.Length; i++)
        {
            if (cap.hasBarrel[i])
            {
                var position = Vector3Extensions.AddX(startPosition.position, distance * i / cap.hasBarrel.Length);
                Instantiate(barrel, position, Quaternion.identity, transform);

            }
        }
        ship.transform.position = startPosition.position;
    }
    public void MoveShip(Cap cap, int capNumber, float shipMoveTime)
    {
        var distance = Vector3.Distance(startPosition.position, endPosition.position);
        var position = Vector3Extensions.AddX(startPosition.position, distance * (capNumber+1) / cap.hasBarrel.Length);
        ship.transform.DOMoveX(position.x,shipMoveTime);
    }
    public void PlayAnimation( float bpm, bool win)
    {
        charaAnimator.speed = 60f / bpm;
        if(!win)
        charaAnimator.SetTrigger("Sad");
    }
}
