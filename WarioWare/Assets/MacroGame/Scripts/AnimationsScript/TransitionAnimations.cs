using System.Collections;
using UnityEngine;
using Caps;
using DG.Tweening;
using UnityEngine.UI;
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
    public GameObject dot;
    public Sprite dotWin;
    public Sprite dotLose;
    private List<GameObject> dots = new List<GameObject>();
    public Image completionBar;

    public void Start()
    {

    }
    public void DisplayBarrel(Cap cap)
    {
        foreach (GameObject dot in dots)
        {
            Destroy(dot);
        }
        dots.Clear();
        var distance = Vector3.Distance(startPosition.position, endPosition.position);
        for (int i = 0; i < cap.length; i++)
        {

            var position = Vector3Extensions.AddX(startPosition.position, distance * i / cap.hasBarrel.Length);
            position = new Vector3(position.x, position.y -0.6f, position.z);
            var _dot = Instantiate(this.dot, position, Quaternion.identity, transform);
            dots.Add(_dot);

        }
        ship.transform.position = startPosition.position;
    }
    public void MoveShip(Cap cap, int capNumber, float shipMoveTime)
    {
        var distance = Vector3.Distance(startPosition.position, endPosition.position);
        var position = Vector3Extensions.AddX(startPosition.position, distance * (capNumber + 1) / cap.length);
        ship.transform.DOMoveX(position.x, shipMoveTime);
    }
    public void MoveShip(Cap cap, int capNumber, float shipMoveTime, bool win)
    {
        var distance = Vector3.Distance(startPosition.position, endPosition.position);
        var position = Vector3Extensions.AddX(startPosition.position, distance * (capNumber + 1) / cap.length);
        if (win)
            dots[capNumber].GetComponent<SpriteRenderer>().sprite = dotWin;
        else
            dots[capNumber].GetComponent<SpriteRenderer>().sprite = dotLose;

        ship.transform.DOMoveX(position.x, shipMoveTime);
    }
    public void PlayAnimation(float bpm, bool win)
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

    public void CompletionBar(float pourcentageCompleted, float duration)
    {
        StartCoroutine(CompletionBarAnim(pourcentageCompleted, duration));
    }

    private IEnumerator CompletionBarAnim(float pourcentageCompleted, float duration)
    {
        while (completionBar.fillAmount < pourcentageCompleted)
        {
            completionBar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
