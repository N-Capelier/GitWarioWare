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

    [Header("Variable")]
    public float rotSpeed = 0.2f;
    public GameObject[] buttons = null;
    public Ease rotType = Ease.Linear;

    public int buttonSelect = 0;
    //Explicit position
    //public float[] rotButton = null;

    void Update()
    {
        selected = eventSyst.currentSelectedGameObject;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (selected == buttons[i].transform.GetChild(0).gameObject)
            {
                buttonSelect = i;
                gouvernail.DORotate(new Vector3(0, 0, (i * 30)), rotSpeed).SetEase(rotType);

                //Explicit position
                //gouvernail.DORotate(new Vector3(0, 0, rotButton[i]), 0.3f);
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < (buttonSelect - 2))
            {
                buttons[i].SetActive(false);
            }
            else
            {
                buttons[i].SetActive(true);
            }
        }
    }
}
