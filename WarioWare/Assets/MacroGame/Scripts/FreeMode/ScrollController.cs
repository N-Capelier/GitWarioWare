using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ScrollController : MonoBehaviour
{
    #region Variables

    // settings
    public float scrollSpeed = 10f;

    public Button quiteButton;
    [SerializeField]
    private RectTransform layoutListGroup = null;

    // temporary variables
    private RectTransform targetScrollObject;
    private bool scrollToSelection = true;

    // references
    private RectTransform scrollWindow;
    private RectTransform currentCanvas;
    private ScrollRect targetScrollRect;
    #endregion

    // Use this for initialization
    private void Start()
    {
        targetScrollRect = GetComponent<ScrollRect>();
        scrollWindow = targetScrollRect.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("B_Button") && quiteButton.gameObject.activeSelf && SceneManager.GetActiveScene().name == "FreeMode")
        {
            quiteButton.Select();
        }
        ScrollRectToLevelSelection();
    }

    private void ScrollRectToLevelSelection()
    {
        var events = EventSystem.current;

        // check main references
        bool referencesAreIncorrect =
            (targetScrollRect == null || layoutListGroup == null || scrollWindow == null);
        if (referencesAreIncorrect == true)
        {
            return;
        }

        // get calculation references
        RectTransform selection = events.currentSelectedGameObject != null ?
            events.currentSelectedGameObject.GetComponent<RectTransform>() :
            null;

        if (selection != targetScrollObject)
        {
            scrollToSelection = true;
        }

        // check if scrolling is possible
        bool isScrollDirectionUnknown = (selection == null || scrollToSelection == false);

        if (isScrollDirectionUnknown == true || selection.transform.parent != layoutListGroup.transform)
        {
            return;
        }

        bool finishedX = false, finishedY = false;

        if (targetScrollRect.vertical)
        {
            // move the current scroll rect to correct position
            float selectionPos = -selection.anchoredPosition.y;

            //float elementHeight = layoutListGroup.sizeDelta.y / layoutListGroup.transform.childCount;
            //float maskHeight = currentCanvas.sizeDelta.y + scrollWindow.sizeDelta.y;
            float listPixelAnchor = layoutListGroup.anchoredPosition.y;

            // get the element offset value depending on the cursor move direction
            float offlimitsValue = 0;

            offlimitsValue = listPixelAnchor - selectionPos;
            // move the target scroll rect
            targetScrollRect.verticalNormalizedPosition += (offlimitsValue / layoutListGroup.sizeDelta.y) * Time.deltaTime * scrollSpeed;

            finishedY = Mathf.Abs(offlimitsValue) < 2f;
        }

       
        // check if we reached our destination
        if (finishedX && finishedY)
        {
            scrollToSelection = false;
        }
        // save last object we were "heading to" to prevent blocking
        targetScrollObject = selection;
    }

}
