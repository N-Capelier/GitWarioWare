﻿using UnityEngine;
using UnityEngine.UI;
using Rewards;
using Caps;
using TMPro;
using Shop;
using Sound;
using Boss;

namespace Player
{
    public class PlayerInventory : Singleton<PlayerInventory>
    {
        [Header("Inventory UI")]
        public GameObject inventoryCanvas;
        public Button[] slots;
        public Image[] rewardImages;
        public Image rewardToAddImage; //
        public TextMeshProUGUI rewardToAddName;
        [HideInInspector] public Reward[] stockedRewards;
        public GameObject itemDescriptionContainer;
        public TextMeshProUGUI itemDescription;
        public TextMeshProUGUI itemName;
        public GameObject[] keystones;

        [Header("Override Confirmation")]
        public GameObject overrideConfirmationPanel;
        public Button noButton;
        private int indexToOverride;

        [Header("Reward UI")]
        public GameObject rewardCanvas;
        public Image rewardImage;
        public TextMeshProUGUI goldCompletion;
        public TextMeshProUGUI woodCompletion;
        public TextMeshProUGUI moralCompletion;
        private Reward rewardToAdd;

        private AudioSource audioSource;
        private bool useItem = false;

        private bool isEndCap;
        [HideInInspector] public bool fromInventory;
        private void Awake()
        {
            CreateSingleton();
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {   
            //rewardToAdd = rew; //-- feature testing
        }

        void Update()
        {

        }
        
        public void Show()
        {
            UI.UICameraController.canSelect = false;

            SoundManager.Instance.ApplyAudioClip("Clicked", audioSource);
            audioSource.PlaySecured();

            Manager.Instance.macroUI.SetActive(false);
            PlayerManager.Instance.inInventory = true;

            for (int i = 0; i < slots.Length; i++)
            {
                if(stockedRewards[i] == null)
                {
                    rewardImages[i].gameObject.SetActive(false);
                }
                else
                {
                    rewardImages[i].gameObject.SetActive(true);
                }
            }

            inventoryCanvas.SetActive(true);
            slots[0].Select();

            if(rewardToAdd != null)
            {
                rewardToAddImage.gameObject.SetActive(true);
                rewardToAddImage.sprite = rewardToAdd.sprite;
                rewardToAddName.gameObject.SetActive(true);
                rewardToAddName.text = rewardToAdd.rewardName;
            }

            ShowCollectedKeyStone();
        }

        public void Hide()
        {
            if(rewardToAdd == null)
            {
                fromInventory = true;
                SoundManager.Instance.ApplyAudioClip("Cancel", audioSource);
                audioSource.PlaySecured();

                inventoryCanvas.SetActive(false);
                if(!ShopManager.Instance.inShop)
                {
                    Manager.Instance.macroUI.SetActive(true);
                    PlayerMovement.Instance.ResetFocus();
                }
                else
                {
                    ShopManager.Instance.shopCanvas.SetActive(true);
                    ShopManager.Instance.shopSlots[0].Select();
                }
                PlayerManager.Instance.inInventory = false;
                if (isEndCap)
                {
                    StartCoroutine(Manager.Instance.UnzoomCam());
                    isEndCap = false;
                }
                if(Manager.Instance.zoomed)
                {
                    StartCoroutine(Manager.Instance.UnzoomCam());
                }
                UI.UICameraController.canSelect = true;
            }
        }

        public void UseActiveItem(Reward item)
        {
            bool _isApplied = item.ApplyActiveEffect();
            if(_isApplied)
            {
                SearchAndDestroyItem(item.rewardName);
            }
        }

        public void SearchAndDestroyItem(string _itemName)
        {
            for(int i = 0; i < stockedRewards.Length; i++)
            {
                if(stockedRewards[i]!=null)
                {
                    if (stockedRewards[i].rewardName == _itemName)
                    {
                        stockedRewards[i] = null;
                        rewardImages[i].gameObject.SetActive(false);
                        return;
                    }
                }
            }
            throw new System.Exception("Item not in inventory!");
        }

        public void AddToInventory(Reward item, int slot)
        {
            stockedRewards[slot] = item;
            rewardImages[slot].gameObject.SetActive(true);
            rewardImages[slot].sprite = item.sprite;
            rewardToAdd = null;
            rewardToAddImage.gameObject.SetActive(false);
            rewardToAddName.gameObject.SetActive(false);
            ShowSelectedSlotInfo(slots[slot]);
            if(stockedRewards[slot].effect == RewardEffect.Passive)
            {
                stockedRewards[slot].ApplyPassiveEffect();
            }
        }

        public void OverrideInventorySlot(Reward newItem, int slot)
        {
            stockedRewards[slot].RemovePassiveEffect();
            stockedRewards[slot] = newItem;
            rewardImages[slot].gameObject.SetActive(true);
            rewardImages[slot].sprite = newItem.sprite;
            rewardToAdd = null;
            rewardToAddImage.gameObject.SetActive(false);
            rewardToAddName.gameObject.SetActive(false);
            if (stockedRewards[slot].effect == RewardEffect.Passive)
            {
                stockedRewards[slot].ApplyPassiveEffect();
            }
        }

        public void SlotActivation(Button clickedButton)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(clickedButton == slots[i])
                {
                    if(stockedRewards[i] == null && rewardToAdd != null)
                    {
                        SoundManager.Instance.ApplyAudioClip("Clicked", audioSource);
                        audioSource.PlaySecured();
                        AddToInventory(rewardToAdd, i);
                        break;
                    }

                    if (stockedRewards[i] == null && rewardToAdd == null)
                    {
                        SoundManager.Instance.ApplyAudioClip("Cancel", audioSource);
                        audioSource.PlaySecured();
                        break;
                    }

                    if (stockedRewards[i] != null && rewardToAdd != null)
                    {
                        SoundManager.Instance.ApplyAudioClip("Clicked", audioSource);
                        audioSource.PlaySecured();
                        indexToOverride = i;
                        overrideConfirmationPanel.SetActive(true);
                        noButton.Select();
                        break;
                    }

                    if(stockedRewards[i] != null && rewardToAdd == null)
                    {
                        if(stockedRewards[i].effect == RewardEffect.Active)
                        {
                            SoundManager.Instance.ApplyAudioClip("UseItem", audioSource);
                            audioSource.PlaySecured();
                            UseActiveItem(stockedRewards[i]);
                            useItem = true;
                            ShowSelectedSlotInfo(slots[i]);
                            break;
                        }
                    }
                }
            }
        }

        public void SetItemToAdd(Reward item, bool _isEndCap = false)
        {
            isEndCap = _isEndCap;
            rewardToAdd = item;
            Show();
        }

        public void ShowSelectedSlotInfo(Button selectedSlot)
        {
            if(!useItem)
            {
                SoundManager.Instance.ApplyAudioClip("Selected", audioSource);
                audioSource.PlaySecured();
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if(slots[i] == selectedSlot && stockedRewards[i] != null)
                {

                    itemDescription.text = stockedRewards[i].GetDescription();
                    itemName.text = stockedRewards[i].rewardName;
                    itemDescriptionContainer.SetActive(true);
                    break;
                }
                else
                {
                    itemDescriptionContainer.SetActive(false);
                }
            }

            useItem = false;
        }

        public void NoOverride()
        {
            overrideConfirmationPanel.SetActive(false);
            slots[0].Select();
            indexToOverride = -1;
        }

        public void Override()
        {
            overrideConfirmationPanel.SetActive(false);
            OverrideInventorySlot(rewardToAdd, indexToOverride);
            slots[0].Select();
            indexToOverride = -1;
        }

        private void ShowCollectedKeyStone()
        {
            
        }


        public void GetKeyStone(string name)
        {
            for (int i = 0; i < keystones.Length; i++)
            {
                if(name == "Les voiles du Queen Anne’s Revenge")
                {
                    keystones[0].SetActive(true);
                }
                else if (name == "Les canons de l’Adventure Galley")
                {
                    keystones[1].SetActive(true);
                }
                else if (name == "Figure de proue du Sloop William")
                {
                    keystones[2].SetActive(true);
                }
                else if (name == "La barre du The William")
                {
                    keystones[3].SetActive(true);
                }
                else if (name == "La clef du magasin")
                {
                    keystones[4].SetActive(true);
                }
            }
        }
    }
}