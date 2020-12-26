using Rewards;
using Islands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Caps;
using Player;
using TMPro;

namespace Shop
{
    public class ShopManager : Singleton<ShopManager>
    {
        [Header ("Shop UI")]
        public GameObject shopCanvas;
        public Button[] shopSlots;
        public Image[] shopItemImages;
        public GameObject itemDescriptionContainer;
        public TextMeshProUGUI itemDescription;
        public TextMeshProUGUI itemPrice;

        [HideInInspector] public List<Reward> shopItems = new List<Reward>();
        private List<Reward> allResources = new List<Reward>();
        private List<Reward> allItems = new List<Reward>();

        private bool inShop;


        private void Awake()
        {
            CreateSingleton();
        }


        void Start()
        {
            InitializeShop();
        }

        private void Update()
        {
            if (inShop && (Input.GetButtonDown("Start_Button") || Input.GetButtonDown("B_Button")))
            {
                Hide();
            }
        }

        void InitializeShop()
        {
            //Part resources from other item types;
            for (int i = 0; i < IslandCreator.Instance.gameRewards.Length; i++)
            {
                if(IslandCreator.Instance.gameRewards[i].type == RewardType.Resource)
                {
                    allResources.Add(IslandCreator.Instance.gameRewards[i]);
                }
                else
                {
                    allItems.Add(IslandCreator.Instance.gameRewards[i]);
                }
            }

            //Initialize shop stocked items
            Reward _reward = IslandCreator.Instance.GetRandomReward(allResources.ToArray());
            shopItems.Add(_reward);
            allResources.Remove(_reward);
            
            for(int i = 0; i < 2; i++)
            {
                _reward = IslandCreator.Instance.GetRandomReward(allItems.ToArray());
                shopItems.Add(_reward);
                allItems.Remove(_reward);
            }
           

            
            for (int i = 0; i < shopSlots.Length; i++)
            {
                Debug.Log(shopItems[i]);
                //shopItemImages[i].sprite = shopItems[i].sprite;
            }
        }

        public void Show()
        {
            Manager.Instance.macroUI.SetActive(false);

            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (shopItems[i] == null)
                {
                    shopItemImages[i].gameObject.SetActive(false);
                }
                else
                {
                    shopItemImages[i].gameObject.SetActive(true);
                }
            }

            shopCanvas.SetActive(true);
            shopSlots[0].Select();
            inShop = true;
        }

        public void Hide()
        {
            inShop = false;
            shopCanvas.SetActive(true);
            Manager.Instance.macroUI.SetActive(true);
            PlayerMovement.Instance.ResetFocus();
        }

        public void BuyItem(Button clickedButton)
        {
            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (clickedButton == shopSlots[i] && shopItems[i] != null)
                {
                    if(PlayerManager.Instance.beatcoins >= shopItems[i].price)
                    {
                        PlayerManager.Instance.GainCoins(-shopItems[i].price);
                        if(shopItems[i].type == RewardType.Resource)
                        {
                            shopItems[i].ApplyPassiveEffect();
                        }
                        else
                        {
                            PlayerInventory.Instance.SetItemToAdd(shopItems[i]);
                        }

                        shopItems[i] = null;
                        shopItemImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Pas assez de Beatcoins");
                    }
                }
            }
        }

        public void ShowSelectedInfo(Button selectedSlot)
        {
            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (shopSlots[i] == selectedSlot && shopItems[i] != null)
                {

                    //itemDescription.text = shopItems[i].GetDescription();
                    itemPrice.text = shopItems[i].price.ToString();
                    itemDescriptionContainer.SetActive(true);
                    break;
                }
                else
                {
                    itemDescriptionContainer.SetActive(false);
                }
            }
        }
    }

}
