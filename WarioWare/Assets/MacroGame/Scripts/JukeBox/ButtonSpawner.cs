using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Sound;


namespace JukeBox
{

    public class ButtonSpawner : MonoBehaviour
    {
        public ScrollController scrollController;
        public GameObject button;
        private List<Button> buttons = new List<Button>();
        public SoundList soundList;
        public Button mainMenuButton;
        private AudioSource audioSource;
        private bool doOnce;
        public AudioSource audioSourceForMusic;
        private List<GameObject> buttonList = new List<GameObject>();
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            foreach (var sound in soundList.music)
            {
                var _sound = Instantiate(button, transform);
                string soundName = sound.name+ " de " + sound.author;

                _sound.GetComponentInChildren<TextMeshProUGUI>().text = soundName;
                buttonList.Add(_sound);
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.Select;
                entry.callback.AddListener((data) => { PlaySelectedSound(); });
                _sound.GetComponent<EventTrigger>().triggers.Add(entry);

                _sound.GetComponent<Button>().onClick.AddListener(() => PlayCard(sound.clip, _sound));
                buttons.Add(_sound.GetComponent<Button>());

                if (!doOnce)
                {
                    _sound.GetComponent<Button>().Select();
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
        private void PlayCard(AudioClip sound, GameObject button)
        {
            foreach (var buttonSound in buttonList)
            {
                buttonSound.GetComponent<MusicFillAmount>().ResetFilling();
            }
            button.GetComponent<MusicFillAmount>().StartFilling(audioSourceForMusic);
            audioSourceForMusic.clip = sound;
            audioSourceForMusic.Play();
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
