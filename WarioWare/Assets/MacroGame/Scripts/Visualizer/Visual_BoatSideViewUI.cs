using UnityEngine;
using DG.Tweening;

public class Visual_BoatSideViewUI : MonoBehaviour
{
    private Transform boat;
    public float timeBPM = 0.3f;
    private bool left = false;
    public Ease rotType;
    public float rotValue = 30f;

    private void Awake()
    {
        boat = this.gameObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        InvokeRepeating("ShakeShipRight", 0, timeBPM);
    }

    private void ShakeShip(float time, float angleZ)
    {
        boat.DORotate(new Vector3(0, 0, angleZ), time * 0.5f).SetEase(Ease.InSine);
    }
    private void ShakeShipRight()
    {
        if (left)
        {
            boat.DORotate(new Vector3(0, 0, rotValue), timeBPM * 0.5f).SetEase(rotType);
        }
        else
        {
            boat.DORotate(new Vector3(0, 0, -rotValue), timeBPM * 0.5f).SetEase(rotType);
        }

        left = !left;

    }
}
