using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Testing { 
    public class Manager : Singleton<Manager>
    {
        private void Awake()
        {
            CreateSingleton(true);
        }

        #region Variables
        public IDCard idCard;
        [Range(60, 180)]
        public float bpm = 60;
        public enum difficulty
        {
            EASY,
            MEDIUM,
            HARD
        }
        public difficulty currentDifficulty;

        [Header("UI Management")]
        public GameObject panel;
        public TextMeshProUGUI resultText;

        #endregion

        #region Methods
        public void Start()
        {
            SceneManager.LoadScene(idCard.microGameScene.BuildIndex, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Debug the result of the game on a panel
        /// </summary>
        /// <param name="win"> if true the game is won , if false the game is lost</param>
        public void Result(bool win)
        {
            if(win)           
                resultText.text = "You Won!";
            
            else        
                resultText.text = "You Lost!";
            
            panel.SetActive(true);
        }
        #endregion
    }
}