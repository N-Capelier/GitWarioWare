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

        #endregion

        private void Awake()
        {
            CreateSingleton(true);
        }

        void Start()
        {

        }

        void Update()
        {

        }

        #region CustomMethods
        public void TakeDamage(int damage)
        {
            playerHp -= damage;
        }
        public void GainCoins(int coins)
        {
            beatcoins += coins;
        }
        public void GainFood(int f)
        {
            food += f;
        }
        #endregion
    }
}

