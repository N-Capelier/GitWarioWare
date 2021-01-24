using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;
using Caps;
using UI;
using UnityEngine.SceneManagement;
using Sound;


namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        #region Variables
        [Header ("Ressources")]
        public int playerHp;
        public int beatcoins;
        public int food;
        public int keyStoneNumber;
        [Header("MaxRessources")]
        public int maxFood;

        public delegate void PlayerUIHandler();
        public event PlayerUIHandler UpdatePlayerUI;

        [Header("Sound")]
        public AudioSource audioSource;

        [Header("Death")]
        public GameObject death;
        [HideInInspector] public bool inInventory = false;

        //Rewards
        [HideInInspector] public int isSturdy = 0;
        [HideInInspector] public int sturdyHealAmmount = 3;

        #endregion

        private void Awake()
        {
            CreateSingleton(true);
        }

        void Start()
        {
            //Initialisation de l'UI du player
            UpdatePlayerUI.Invoke();
            //set up value from debug
            playerHp = DebugToolManager.Instance.ChangeVariableValue("playerHp");
            beatcoins = DebugToolManager.Instance.ChangeVariableValue("beatcoins");
            food = DebugToolManager.Instance.ChangeVariableValue("food");
            maxFood = DebugToolManager.Instance.ChangeVariableValue("maxFood");
        }

        void Update()
        {

            //Show / Hide Inventory //check micro UI inactive
            if(!Manager.Instance.capUI.activeSelf && Input.GetButtonDown("Start_Button") && !inInventory)
            {
                PlayerInventory.Instance.Show();
            }
            else if(!Manager.Instance.capUI.activeSelf && inInventory && (Input.GetButtonDown("Start_Button") || Input.GetButtonDown("B_Button")))
            {
                PlayerInventory.Instance.Hide(); 
            }
        }



        #region CustomMethods
        public void TakeDamage(int damage, bool isBoss = false)
        {
            if(playerHp - damage <= 0)
            {
                playerHp = 0;

                if (isSturdy > 0)
                {
                    playerHp = sturdyHealAmmount;
                    isSturdy = 0;
                    PlayerInventory.Instance.SearchAndDestroyItem("Rattrapage");
                }
                else
                {
                    StartCoroutine(DeathCoroutine(isBoss));
                    Debug.Log("You are dead");
                }
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
            else if(food + f <= 0)
            {
                food = 0;
            }
            else
            {
                food += f;
            }
            UpdatePlayerUI.Invoke();
        }

        private IEnumerator DeathCoroutine(bool isBoss = false)
        {
            if(!isBoss)
                SoundManager.Instance.ApplyAudioClip("gameOverJingle", audioSource, Manager.Instance.bpm);
            else
                SoundManager.Instance.ApplyAudioClip("gameOverJingleBoss", audioSource, Manager.Instance.bpm);

            audioSource.PlaySecured();
            PlayerInventory.Instance.rewardCanvas.SetActive(true);
            death.SetActive(true);
            yield return new WaitForSeconds(audioSource.clip.length);
            Manager.Instance.EndGame();
            Instance.EndGame();
            if (SceneManager.GetActiveScene().name == "FreeMode")
                SceneManager.LoadScene("FreeMode");
            else
                SceneManager.LoadScene("Menu");    
        }
        #endregion
    }
}

