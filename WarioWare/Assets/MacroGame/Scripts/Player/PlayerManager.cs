using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;
using Caps;
using UI;
using UnityEngine.SceneManagement;


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
        public event PlayerUIHandler UpdatePlayerUI;


        private bool inInventory = false;

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

            //Show / Hide Inventory //check micro UI inactive
            if(!Manager.Instance.capUI.activeSelf && Input.GetButtonDown("B_Button") && !inInventory)
            {
                Manager.Instance.macroUI.SetActive(false);
                PlayerInventory.Instance.Show();
                inInventory = true;
            }
            else if(!Manager.Instance.capUI.activeSelf && inInventory && Input.GetButtonDown("B_Button"))
            {
                Manager.Instance.macroUI.SetActive(true);
                PlayerInventory.Instance.Hide();
                inInventory = false;
            }
        }

        #region CustomMethods
        public void TakeDamage(int damage)
        {
            if(playerHp - damage <= 0)
            {
                playerHp = 0;

                StartCoroutine(DeathCoroutine());
                Debug.Log("You are dead");
            }
            else
            {
                playerHp -= damage;
            }
            UpdatePlayerUI.Invoke();
        }

        public void Heal(int health)
        {       
            playerHp += health;
            
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

        private IEnumerator DeathCoroutine()
        {
            FadeManager.Instance.FadeInAndOut(4);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Menu");    
        }
        #endregion
    }
}

