using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections;
namespace UI
{
    public class Visual_IslandDescriptionOpening : MonoBehaviour
    {
        [SerializeField] private RectTransform parchemin;

        //for parchemin.sizeDelta
        [SerializeField] private float closedSize = 0f;
        [Range(0, 1)]
        [SerializeField] private float openedSize = 0.3f;

        public float openingTime = 1;


        private void OnEnable()
        {
            parchemin.DOScaleY(openedSize, openingTime);
        }

        private void OnDisable()
        {
            parchemin.DOScaleY(closedSize, 1);
        }

        public void Close()
        {
            parchemin.transform.localScale = Vector3.up*openedSize; 
            parchemin.DOScaleY(closedSize, openingTime);
            StartCoroutine(WaitToClose());
        }

        private IEnumerator WaitToClose()
        {
            yield return new WaitForSeconds(openingTime);
            gameObject.SetActive(false);

        }
    }
}
