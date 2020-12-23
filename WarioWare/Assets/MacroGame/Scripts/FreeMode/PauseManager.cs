using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Caps;
using Player;



namespace FreeMode
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject MenuPause;
        public Button UnPauseButton;

        private void Update()
        {
            if (Input.GetButtonDown("Start_Button") && !MenuPause.activeSelf && Manager.Instance.isLoaded)
            {
                
                MenuPause.SetActive(true);
                Time.timeScale = 0;
                UnPauseButton.Select();
            }
            else if (Input.GetButtonDown("Start_Button") && MenuPause.activeSelf)
                UnPause();
        }

        public void UnPause()
        {
            Time.timeScale = 1;
            MenuPause.SetActive(false);
        }

        public void FreeMode()
        {

            Time.timeScale = 1;
            Manager.Instance.EndGame();
            PlayerManager.Instance.EndGame();
            SceneManager.LoadScene("FreeMode");
        }
        public void Menu()
        {
            Time.timeScale = 1;
            PlayerManager.Instance.EndGame();
            Manager.Instance.EndGame();
            SceneManager.LoadScene("Menu");
        }
    }
}
