using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimations : MonoBehaviour
{
    public Animator charaAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayAnimation( float bpm, bool win)
    {
        charaAnimator.speed = 60f / bpm;
        if(!win)
        charaAnimator.SetTrigger("Sad");
    }
}
