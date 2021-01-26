using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IslandIcone_Showcase : MonoBehaviour
{
    public RectTransform icone;
    public float duration = 1f;
    public Ease easeType = Ease.Linear;
    public float elevationY = 100f;

    private Image image_icone;
    private Vector3 startPosition;
    private Color transparency = new Color(0, 0, 0, 0);

    private void Awake()
    {
        image_icone = icone.GetComponent<Image>();
        image_icone.color = transparency;
        startPosition = icone.position;
    }

    public void Show(float _duration)
    {
        icone.position = startPosition;
        image_icone.color = transparency;
        icone.DOMoveY(startPosition.y + elevationY, _duration).SetEase(easeType);
        image_icone.DOColor(Color.white, _duration).SetEase(easeType);
    }

    public void Hide(float _duration)
    {
        icone.DOAnchorPosY(startPosition.y, _duration).SetEase(easeType);
        image_icone.DOColor(transparency, _duration).SetEase(easeType);
    }

    private void OnEnable()
    {
        Show(duration);
    }
}
