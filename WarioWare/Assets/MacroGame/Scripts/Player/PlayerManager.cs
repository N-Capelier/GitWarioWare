using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Variables
        [Header ("Ressources")]
        public int playerHp;
        public int beatcoins;
        public int food;

        [Header("MaxRessources")]
        public int maxFood;

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
            if(playerHp - damage <= 0)
            {
                playerHp = 0;
                //Death event Here
                Debug.Log("You are dead");
            }
            else
            {
                playerHp -= damage;
            }
            UpdatePlayerUI.Invoke();
        }
        public void GainCoins(int coins)
        {
            beatcoins += coins;
            UpdatePlayerUI.Invoke();
        }
        public void GainFood(int f)
        {
            if(food + f >= maxFood)
            {
                food = maxFood;
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

