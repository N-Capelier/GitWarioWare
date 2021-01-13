
using UnityEngine;
using Caps;
using DG.Tweening;
using System.Collections.Generic;

public class TransitionAnimations : MonoBehaviour
{
    public Animator longHairChara;
    public Animator asianWomenAnimator;
    public Animator quartierMaitreChara;
    public GameObject ship;
    public Transform startPosition;
    public Transform endPosition;
    public GameObject barrel;
    private List<GameObject> barrels = new List<GameObject>();
    public void DisplayBarrel(Cap cap)
    {
        foreach (GameObject barrel in barrels)
        {
            Destroy(barrel);
        }
        barrels.Clear();
        var distance = Vector3.Distance(startPosition.position, endPosition.position);
        for (int i = 0; i < cap.hasBarrel.Length; i++)
        {
            if (cap.hasBarrel[i])
            {
                var position = Vector3Extensions.AddX(startPosition.position, distance * i / cap.hasBarrel.Length);
                var _barel =Instantiate(barrel, position, Quaternion.identity, transform);
                barrels.Add(_barel);
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
        
            longHairChara.speed = 60f / bpm;
            asianWomenAnimator.speed = 60f / bpm;
            quartierMaitreChara.speed = 60f / bpm;
            if (!win)
            {
                longHairChara.SetTrigger("Sad");
                asianWomenAnimator.SetTrigger("Sad");
                quartierMaitreChara.SetTrigger("Sad");
            }
            else
            {
                longHairChara.SetTrigger("Happy");
                asianWomenAnimator.SetTrigger("Happy");
                quartierMaitreChara.SetTrigger("Happy");
            }
              
    }
}
