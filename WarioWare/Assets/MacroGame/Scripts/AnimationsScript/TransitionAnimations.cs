
using UnityEngine;
using Caps;
using DG.Tweening;
using System.Collections.Generic;

public class TransitionAnimations : MonoBehaviour
{
    public Animator ghost;
    public Animator cat;
    public Animator nerde;
    public Animator coolGirl;
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
        
            ghost.speed = 60f / bpm;
            cat.speed = 60f / bpm;
            nerde.speed = 60f / bpm;
            coolGirl.speed = 60f / bpm;
            if (!win)
            {
                ghost.SetTrigger("Sad");
                cat.SetTrigger("Sad");
                nerde.SetTrigger("Sad");
                coolGirl.SetTrigger("Sad");
            }
            else
            {
                ghost.SetTrigger("Happy");
                nerde.SetTrigger("Happy");
                cat.SetTrigger("Happy");
                coolGirl.SetTrigger("Happy");
            }
              
    }

    public void SpeedUp(float bpm)
    {
        ghost.speed = 60f / bpm;
        cat.speed = 60f / bpm;
        nerde.speed = 60f / bpm;
        coolGirl.speed = 60f / bpm;

        ghost.SetTrigger("SpeedUp");
        cat.SetTrigger("SpeedUp");
        nerde.SetTrigger("SpeedUp");
        coolGirl.SetTrigger("SpeedUp");
    }
}
