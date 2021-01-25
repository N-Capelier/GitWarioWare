using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Player;

namespace UI
{
    public class PlayerRessourcesUI : MonoBehaviour
    {
        public Image playerHpFillBar;
        public TextMeshProUGUI beatcoinsCount;

        // Start is called before the first frame update
        void Start()
        {
            PlayerManager.Instance.UpdatePlayerUI += UpdateUI ;
            UpdateUI();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateUI()
        {
            playerHpFillBar.fillAmount = (float)PlayerManager.Instance.playerHp / 300f;
            beatcoinsCount.text = PlayerManager.Instance.beatcoins.ToString();
        }   
    }
}

