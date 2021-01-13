using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Visual_GouvernailRotation : MonoBehaviour
{
    [Header("Mise en place")]
    public EventSystem eventSyst;
    public RectTransform gouvernail;
    private GameObject selected;
    public GameObject[] buttons = null;

    [Header("Variable")]
    public float rotOffset = 0f;
    public float rotSpeed = 0.2f;
    public Ease rotType = Ease.Linear;
    [Space(10)]
    public float ZoomSize = 1.5f;
    public Ease scaleType = Ease.Linear;

    private int buttonSelect = 0;
    //Explicit position
    //public float[] rotButton = null;

    void Update()
    {
        selected = eventSyst.currentSelectedGameObject;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (selected == buttons[i])
            {
                buttonSelect = i;
                gouvernail.DORotate(new Vector3(0, 0,(i * 30) + rotOffset), rotSpeed).SetEase(rotType);

                //Explicit position
                //gouvernail.DORotate(new Vector3(0, 0, rotButton[i]), 0.3f);
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < (buttonSelect - 1) || i > (buttonSelect + 2))
            {
                buttons[i].SetActive(false);
            }
            else
            {
                buttons[i].SetActive(true);
            }

            if (i == buttonSelect)
            {
                buttons[i].transform.DOScale(ZoomSize, 0.2f).SetEase(scaleType);
            }
            else if(buttons[i].transform.localScale != Vector3.one)
            {
                buttons[i].transform.DOScale(1f, 0.2f).SetEase(scaleType);
            }
        }
    }
}
