using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Caps;
using Player;
namespace FreeMode
{
    public class ButtonSpawner : MonoBehaviour
    {
        public ScrollController scrollController;
        public GameObject button;
        private List<Button> buttons = new List<Button>();
        public CapsSorter sorter;
        private bool doOnce;

        private void Start()
        {
                PlayerManager.Instance.UpdatePlayerUI += FakeDelegate;
            foreach (var id in sorter.idCards)
            {
                var _id = Instantiate(button, transform);
                string idName = id.name;
                idName = idName.Replace("ID_", "");
                _id.GetComponentInChildren<TextMeshProUGUI>().text = idName + " de " + id.trio;

                _id.GetComponent<Button>().onClick.AddListener(() => PlayCard(id));
                id.currentDifficulty = Difficulty.EASY;
                buttons.Add(_id.GetComponent<Button>());

                if (!doOnce)
                {
                    _id.GetComponent<Button>().Select();
                    doOnce = true;
                }
            }


        }


        private void FakeDelegate()
        {

        }
        private void PlayCard(IDCard card)
        {
           
            List<IDCard> idcarList = new List<IDCard>();

            for (int i = 0; i < 100; i++)
            {
                idcarList.Add(card);
            }

            Cap cap = new Cap(idcarList);

           StartCoroutine( Manager.Instance.StartCap(cap));
            SetUnSelectable();
    }
        private void SetUnSelectable()
        {
            scrollController.quiteButton.gameObject.SetActive(false);
            foreach (var button in buttons)
            {
                button.enabled = false;
                button.GetComponent<Image>().enabled = false;
                button.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

            }
        }
}
}
