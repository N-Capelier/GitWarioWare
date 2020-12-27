using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Visual_IslandDescriptionOpening : MonoBehaviour
{
    [SerializeField] private RectTransform parchemin;

    //for parchemin.sizeDelta
    [SerializeField] private float closedSize = 210f;
    [SerializeField] private float openedSize = 890f;


    private void OnEnable()
    {
        parchemin.DOScaleY(0.3f, 1);
    }

    private void OnDisable()
    {
        parchemin.DOScaleY(0.05f, 1);
    }
}
