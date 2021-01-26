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
        public Image craneImage;
        public Image moralFillBar;
        public TextMeshProUGUI beatcoinsCount;

        [Header("Cranes")]
        public Sprite crane_1;
        public Sprite crane_2;
        public Sprite crane_3;
        public Sprite crane_4;

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
            var moral = PlayerManager.Instance.moral;

            playerHpFillBar.fillAmount = (float)PlayerManager.Instance.playerHp / 300f;
            moralFillBar.fillAmount = (float) moral / 100;
            beatcoinsCount.text = PlayerManager.Instance.beatcoins.ToString();

            if(moral<25)
                craneImage.sprite = crane_1;
            else if(moral >= 25 && moral < 50)
                craneImage.sprite = crane_2;
            else if (moral >= 50 && moral < 75)
                craneImage.sprite = crane_3;
            else if (moral > 75)
                craneImage.sprite = crane_4;

        }
    }
}

