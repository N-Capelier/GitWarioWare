using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sound;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class MenuButton : Selectable, ISelectHandler , ISubmitHandler
{ 
    public AudioSource onSelected;
    public AudioSource playerInputs;
    private bool doOnce;
    public void Update()
    {
        if (!doOnce)
        {
            SoundManager.Instance.ApplyAudioClip("Selected", onSelected);
            doOnce = true;
        }

    }
    //Do this when the selectable UI object is selected.
    public override void OnSelect(BaseEventData eventData)
    {
        onSelected.PlaySecured();
        
    }
    public void OnSubmit(BaseEventData eventData)
    {
        if (!IsInteractable())
        {
           // SoundManager.Instance.ApplyAudioClip("ClikImpossible", playerInputs);
        }
        else
        {
           // SoundManager.Instance.ApplyAudioClip("Clicked", playerInputs);
        }
        playerInputs.PlaySecured();
    }


}