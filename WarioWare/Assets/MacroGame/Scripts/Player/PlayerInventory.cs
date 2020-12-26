﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewards;
using Caps;
using TMPro;

namespace Player
{
    public class PlayerInventory : Singleton<PlayerInventory>
    {
        [Header("Inventory UI")]
        public GameObject inventoryCanvas;
        public Button[] slots;
        public Image[] rewardImages;
        public Image rewardToAddImage;
        [HideInInspector] public Reward[] stockedRewards;
        public GameObject itemDescriptionContainer;
        public TextMeshProUGUI itemDescription;

        [Header("Override Confirmation")]
        public GameObject overrideConfirmationPanel;
        public Button noButton;
        private int indexToOverride;

        [Header("Reward UI")]
        public GameObject rewardCanvas;
        public Image rewardImage;

        private Reward rewardToAdd;

        private void Awake()
        {
            CreateSingleton();
        }

        // Start is called before the first frame update
        void Start()
        {   
            //rewardToAdd = stockedRewards[0]; //-- feature testing
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void Show()
        {
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
                //rewardToAddImage.sprite = rewardToAdd.sprite;
            }
        }

        public void Hide()
        {
            if(rewardToAdd == null)
            {
                inventoryCanvas.SetActive(false);
                PlayerMovement.Instance.ResetFocus();
                Manager.Instance.macroUI.SetActive(true);
                PlayerManager.Instance.inInventory = false;
            }
        }

        public void UseItem(Reward item)
        {
            //item.GiveEffect();
            //destroy item from inventory

        }

        public void AddToInventory(Reward item, int slot)
        {
            stockedRewards[slot] = item;
            rewardImages[slot].gameObject.SetActive(true);
            //rewardImages[slot].sprite = item.sprite;
            rewardToAdd = null;
            rewardToAddImage.gameObject.SetActive(false);
            /*if(item =passive)
            {
             apply passive effect
            }
            */
        }

        public void OverrideInventorySlot(Reward newItem, int slot)
        {
            stockedRewards[slot].RemovePassiveEffect();
            stockedRewards[slot] = newItem;
            rewardImages[slot].gameObject.SetActive(true);
            //rewardImages[slot].sprite = item.sprite;
            rewardToAdd = null;
            rewardToAddImage.gameObject.SetActive(false);
            /*if(item =passive)
            {
             apply passive effect
            }
            */
        }

        public void SlotActivation(Button clickedButton)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(clickedButton == slots[i])
                {
                    if(stockedRewards[i] == null && rewardToAdd != null)
                    {
                        AddToInventory(rewardToAdd, i);
                        break;
                    }
                    
                    if(stockedRewards[i] != null && rewardToAdd != null)
                    {
                        indexToOverride = i;
                        overrideConfirmationPanel.SetActive(true);
                        noButton.Select();
                        break;
                    }

                    if(stockedRewards[i] != null && rewardToAdd == null)
                    {
                        if(stockedRewards[i].effect == RewardEffect.Active)
                        {
                            UseItem(stockedRewards[i]);
                            stockedRewards[i] = null;
                            rewardImages[i].gameObject.SetActive(false);
                            break;
                        }
                    }
                }
            }
        }

        public void SetItemToAdd(Reward item)
        {
            rewardToAdd = item;
            Show();
        }

        public void ShowSelectedSlotInfo(Button selectedSlot)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if(slots[i] == selectedSlot && stockedRewards[i] != null)
                {

                    //itemDescription.text = stockedRewards[i].GetDescription();
                    itemDescriptionContainer.SetActive(true);
                    break;
                }
                else
                {
                    itemDescriptionContainer.SetActive(false);
                }
            }   
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

    }
}

