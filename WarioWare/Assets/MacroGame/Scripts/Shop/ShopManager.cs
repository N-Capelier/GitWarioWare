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
        public TextMeshProUGUI[] itemPrices;
        public GameObject itemDescriptionContainer;
        public TextMeshProUGUI itemDescription;
        public TextMeshProUGUI itemName;
        public RectTransform monkeyHand;

        public Island[] shopIslands;
        private int loadedShopIndex;

        [HideInInspector] public List<Reward>[] shopItems = new List<Reward>[10];
        private List<Reward> allResources = new List<Reward>();
        private List<Reward> allItems = new List<Reward>();

        [HideInInspector] public bool inShop;

        private AudioSource audioSource;
        public AudioSource ambianceSource;

        private void Awake()
        {
            CreateSingleton();       
        }


        void Start()
        {
            InitializeShop();
            audioSource = GetComponent<AudioSource>();
            SoundManager.Instance.ApplyAudioClip("ShopMusic", ambianceSource);
        }

        private void Update()
        {

            if (inShop && (Input.GetButtonDown("B_Button")) && !PlayerManager.Instance.inInventory)
            {
                Hide();
            }

            if(inShop && Input.GetButtonDown("Start_Button"))
            {
                shopCanvas.SetActive(false);
            }
        }

        private void InitializeRewardLists()
        {
            //Part resources from other item types;
            for (int i = 0; i < IslandCreator.Instance.gameRewards.Length; i++)
            {
                if (IslandCreator.Instance.gameRewards[i].type == RewardType.Resource)
                {
                    if (IslandCreator.Instance.gameRewards[i].price != 0)
                    {
                        allResources.Add(IslandCreator.Instance.gameRewards[i]);
                    }
                }
                else
                {
                    allItems.Add(IslandCreator.Instance.gameRewards[i]);
                }
            }
        }

        public void InitializeShop()
        {
            InitializeRewardLists();
            
            for (int i = 0; i < shopIslands.Length; i++)
            {
                shopItems[i] = new List<Reward>();

                //Initialize shop stocked items
                Reward _reward = IslandCreator.Instance.FisherYates(allItems.ToArray())[0];
                shopItems[i].Add(_reward);
                allItems.Remove(_reward);

                //Initialize shop stocked ressources
                for (int j = 0; j < 2; j++)
                {
                    _reward = IslandCreator.Instance.FisherYates(allResources.ToArray())[0];
                    shopItems[i].Add(_reward);
                }
            }

            for (int i = 0; i < shopItems[0].Count; i++)
            {
                print(shopItems[0][i].name);
            }
        }

        private void LoadShopItems(int index)
        {
            loadedShopIndex = index;
            //Load images and price text
            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (shopItems[index][i] == null)
                {
                    shopItemImages[i].gameObject.SetActive(false);
                    itemPrices[i].gameObject.SetActive(false);
                }
                else
                {
                    shopItemImages[i].gameObject.SetActive(true);
                    itemPrices[i].gameObject.SetActive(true);
                    shopItemImages[i].sprite = shopItems[index][i].sprite;
                    itemPrices[i].text = shopItems[index][i].price.ToString();
                }
            }
        }

        public void ClearShops()
        {
            allItems.Clear();
            allResources.Clear();
            for (int i = 0; i < shopIslands.Length; i++)
            {
                shopItems[i].Clear();
            }
        }

        public void Show(Island shop)
        {
            UI.UICameraController.canSelect = false;

            SoundManager.Instance.ApplyAudioClip("Clicked", audioSource);
            audioSource.PlaySecured();

            Manager.Instance.macroUI.SetActive(false);

            for (int i = 0; i < shopIslands.Length; i++)
            {
                if(shop == shopIslands[i])
                    LoadShopItems(i);
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
            UI.UICameraController.canSelect = true;

        }

        public void BuyItem(Button clickedButton)
        {
            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (clickedButton == shopSlots[i] && shopItems[loadedShopIndex][i] != null)
                {
                    if(PlayerManager.Instance.beatcoins >= shopItems[loadedShopIndex][i].price)
                    {
                        SoundManager.Instance.ApplyAudioClip("CollectItem", audioSource);
                        audioSource.PlaySecured();

                        PlayerManager.Instance.GainCoins(-shopItems[loadedShopIndex][i].price);
                        if(shopItems[loadedShopIndex][i].type == RewardType.Resource)
                        {
                            shopItems[loadedShopIndex][i].ApplyPassiveEffect();
                        }
                        else
                        {
                            shopCanvas.SetActive(false);
                            PlayerInventory.Instance.SetItemToAdd(shopItems[loadedShopIndex][i]);
                        }

                        shopItems[loadedShopIndex][i] = null;
                        shopItemImages[i].gameObject.SetActive(false);
                        itemPrices[i].gameObject.SetActive(false);
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
                if (shopSlots[i] == selectedSlot && shopItems[loadedShopIndex][i] != null)
                {
                    itemDescription.text = shopItems[loadedShopIndex][i].GetDescription();
                    itemName.text = shopItems[loadedShopIndex][i].rewardName;
                    itemDescriptionContainer.SetActive(true);
                    
                    Quaternion q = new Quaternion(0, 0, 0, 0);

                    if (i == 0)
                    {
                        Vector3 rotate = new Vector3(0,0,10);
                        monkeyHand.rotation = q; 
                        monkeyHand.Rotate(rotate);
                    }
                    else if (i == 1)
                    {
                        Vector3 rotate = new Vector3(0, 0, 30);
                        monkeyHand.rotation = q;
                        monkeyHand.Rotate(rotate);
                    }
                    else if (i == 2)
                    {
                        Vector3 rotate = new Vector3(0, 0, 65);
                        monkeyHand.rotation = q;
                        monkeyHand.Rotate(rotate);
                    }
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
