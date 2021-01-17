using Rewards;
using Islands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Caps;
using Player;
using TMPro;
using ExampleScene;
using Sound;
using SoundManager = Sound.SoundManager;

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
        public TextMeshProUGUI itemName;

        [HideInInspector] public List<Reward> shopItems = new List<Reward>();
        private List<Reward> allResources = new List<Reward>();
        private List<Reward> allItems = new List<Reward>();

        [HideInInspector] public bool inShop;

        private AudioSource audioSource;


        private void Awake()
        {
            CreateSingleton();
            
        }


        void Start()
        {
            InitializeShop();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Y_Button"))
            {
                //Show(); //--Feature testing
            }

            if (inShop && (Input.GetButtonDown("B_Button")) && !PlayerManager.Instance.inInventory)
            {
                Hide();
            }

            if(inShop && Input.GetButtonDown("Start_Button"))
            {
                shopCanvas.SetActive(false);
            }
        }

        public void InitializeShop()
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
            Reward _reward = IslandCreator.Instance.FisherYates(allItems.ToArray())[0];
            shopItems.Add(_reward);
            allResources.Remove(_reward);
            
            for(int i = 0; i < 2; i++)
            {
                _reward = IslandCreator.Instance.FisherYates(allResources.ToArray())[0];
                shopItems.Add(_reward);
                allItems.Remove(_reward);
            }
            
            for (int i = 0; i < shopSlots.Length; i++)
            {
                shopItemImages[i].sprite = shopItems[i].sprite;
            }
        }

        public void Show()
        {
            SoundManager.Instance.ApplyAudioClip("Clicked", audioSource);
            audioSource.PlaySecured();

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
            SoundManager.Instance.ApplyAudioClip("Cancel", audioSource);
            audioSource.PlaySecured();

            inShop = false;
            shopCanvas.SetActive(false);
            Manager.Instance.macroUI.SetActive(true);
            Manager.Instance.shipOpening.gameObject.SetActive(false);
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
                        SoundManager.Instance.ApplyAudioClip("CollectItem", audioSource);
                        audioSource.PlaySecured();

                        PlayerManager.Instance.GainCoins(-shopItems[i].price);
                        if(shopItems[i].type == RewardType.Resource)
                        {
                            shopItems[i].ApplyPassiveEffect();
                        }
                        else
                        {
                            shopCanvas.SetActive(false);
                            PlayerInventory.Instance.SetItemToAdd(shopItems[i]);
                        }

                        shopItems[i] = null;
                        shopItemImages[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        SoundManager.Instance.ApplyAudioClip("ClickedImpossible", audioSource);
                        audioSource.PlaySecured();
                    }
                }
            }
        }

        public void ShowSelectedInfo(Button selectedSlot)
        {
            SoundManager.Instance.ApplyAudioClip("Selected", audioSource);
            audioSource.PlaySecured();

            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (shopSlots[i] == selectedSlot && shopItems[i] != null)
                {

                    itemDescription.text = shopItems[i].GetDescription();
                    itemPrice.text = shopItems[i].price.ToString();
                    itemName.text = shopItems[i].rewardName;
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
