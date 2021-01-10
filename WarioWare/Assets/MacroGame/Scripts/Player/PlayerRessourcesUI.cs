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
        public TextMeshProUGUI playerHp;
        public TextMeshProUGUI beatcoinsCount;
        public TextMeshProUGUI foodCount;

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
            beatcoinsCount.text = PlayerManager.Instance.beatcoins.ToString();
            foodCount.text = PlayerManager.Instance.food.ToString();
            playerHp.text = PlayerManager.Instance.playerHp.ToString();
        }
    }
}

