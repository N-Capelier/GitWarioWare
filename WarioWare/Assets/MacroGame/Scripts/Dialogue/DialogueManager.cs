using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
public class DialogueManager : Singleton<DialogueManager>
{
    private bool canSkip;
    private int currentDialogueNumber;
    private int dialogueInRow;
    private GameObject currenDialogue;
    public GameObject[] dialogues = new GameObject[5];
    private Vector3 positionToReset;
    private GameObject currentTarget;
    private int currentDelay;
    private void Awake()
    {
        CreateSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayDialogue(0,3);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSkip && Input.GetButtonDown("A_Button"))
        {
            Destroy(currenDialogue);
            canSkip = false;
            if (dialogueInRow > 0)
            {
                PlayDialogue(currentDialogueNumber, dialogueInRow, currentDelay);
            }
            else
            {
                if(currentTarget != null)
                {
                    currentTarget.transform.position = positionToReset;
                    currentTarget = null;
                }
                Manager.Instance.eventSystem.enabled = true;
                UI.UICameraController.canSelect = true;

            }
        }
    }


    public void PlayDialogue(int dialogueNumber, int _dialogueInRow = 0, int delay = 100, GameObject target= null, Transform direction = null)
    {
        currentDelay = delay;
        UI.UICameraController.canSelect = false;
        canSkip = true;
        dialogueInRow = _dialogueInRow;
        currenDialogue = Instantiate(dialogues[dialogueNumber], transform.position, Quaternion.identity);
        dialogueInRow--;
        currentDialogueNumber = dialogueNumber + 1;
        Manager.Instance.eventSystem.enabled = false;
        if(delay != 100)
        {
            currentDelay --;
        }
        if(target != null && currentDelay ==0 )
        {
            Player.PlayerManager.Instance.clouds.SetBool("IsZoneActivate", true);
            currentTarget = target;
            positionToReset = currentTarget.transform.position;
            currentTarget.transform.position = direction.position;
        }
    }
}
