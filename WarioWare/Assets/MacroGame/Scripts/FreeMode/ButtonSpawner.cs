using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Caps;
using Player;
using Sound;
using UnityEngine.EventSystems;

namespace FreeMode
{
    public class ButtonSpawner : MonoBehaviour
    {
        public ScrollController scrollController;
        public GameObject button;
        private List<Button> buttons = new List<Button>();
        public CapsSorter sorter;
        public Button mainMenuButton;
        private AudioSource audioSource;
        private bool doOnce;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            foreach (var id in sorter.idCards)
            {
                var _id = Instantiate(button, transform);
                string idName = id.name;
                idName = idName.Replace("ID_", "");
                _id.GetComponentInChildren<TextMeshProUGUI>().text = idName + " de " + id.trio;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.Select;
                entry.callback.AddListener((data) => { PlaySelectedSound(); });
                _id.GetComponent<EventTrigger>().triggers.Add(entry);

                _id.GetComponent<Button>().onClick.AddListener(() => PlayCard(id));
                id.currentDifficulty = Difficulty.EASY;
                buttons.Add(_id.GetComponent<Button>());

                if (!doOnce)
                {
                    _id.GetComponent<Button>().Select();
                    doOnce = true;
                }
            }

            EventTrigger.Entry entry_ = new EventTrigger.Entry();
            entry_.eventID = EventTriggerType.Select;
            entry_.callback.AddListener((data) => { PlaySelectedSound(); });
            mainMenuButton.gameObject.GetComponent<EventTrigger>().triggers.Add(entry_);

            mainMenuButton.onClick.AddListener(() => PlayClickedSound());

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

            StartCoroutine(Manager.Instance.StartMiniGame(cap));
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

        private void PlayClickedSound()
        {
            SoundManager.Instance.ApplyAudioClip("Clicked", audioSource);
            audioSource.PlaySecured();
        }

        private void PlaySelectedSound()
        {
            SoundManager.Instance.ApplyAudioClip("Selected", audioSource);
            audioSource.PlaySecured();
        }
    }
}
