using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class TransitionWarioClassicTest : MonoBehaviour
{
    [SerializeField] private RawImage renderTexture;
    [SerializeField] private int TargetSceneIndex;


    [SerializeField] Vector2 CenterPos = Vector2.zero;
    [SerializeField] float fullScreenSize = 1f;
    [SerializeField] Ease easeType = Ease.Linear;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CenterViewport(renderTexture.rectTransform, 1f);
        }
    }

    public void CenterViewport(RectTransform canvas,  float duration)
    {
        canvas.DOAnchorPosX(CenterPos.x, duration, false).SetEase(easeType);
        canvas.DOAnchorPosY(CenterPos.y, duration, false).SetEase(easeType);
        canvas.DOScale(fullScreenSize, duration).SetEase(easeType);
    }
}
