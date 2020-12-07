using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Variables

        public int playerHp;
        public int beatcoins;
        public int food;

        public delegate void PlayerUIHandler();
        public event PlayerUIHandler UpdatePlayerUI ;

        #endregion

        private void Awake()
        {
            CreateSingleton(true);
        }

        void Start()
        {
            //Initialisation de l'UI du player
            UpdatePlayerUI.Invoke();
        }

        void Update()
        {

        }

        #region CustomMethods
        public void TakeDamage(int damage)
        {
            playerHp -= damage;
            UpdatePlayerUI.Invoke();
        }
        public void GainCoins(int coins)
        {
            beatcoins += coins;
            UpdatePlayerUI.Invoke();
        }
        public void GainFood(int f)
        {
            if(food + f >= 10)
            {
                food = 10;
            }
            else
            {
                food += f;
            }
            UpdatePlayerUI.Invoke();
        }
        #endregion
    }
}

