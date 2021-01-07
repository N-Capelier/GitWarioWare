using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewards;
using Caps;
using TMPro;
using Shop;

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

        private bool isEndCap;
        private void Awake()
        {
            CreateSingleton();
        }

        void Start()
        {   
            //rewardToAdd = stockedRewards[0]; //-- feature testing
        }

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
                rewardToAddImage.sprite = rewardToAdd.sprite;
            }
        }

        public void Hide()
        {
            if(rewardToAdd == null)
            {
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
            }
        }

        public void UseActiveItem(Reward item)
        {
            bool _isApplied = item.ApplyActiveEffect();
            if(_isApplied)
            {
                //destroy item from inventory
            }
        }

        public void SearchAndDestroyItem(string _itemName)
        {
            for(int i = 0; i < stockedRewards.Length; i++)
            {
                if(stockedRewards[i].rewardName == _itemName)
                {
                    //destroy item at index i from inventory
                    return;
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
            ShowSelectedSlotInfo(slots[slot]);
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
            rewardImages[slot].sprite = newItem.sprite;
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
                            UseActiveItem(stockedRewards[i]);
                            stockedRewards[i] = null;
                            rewardImages[i].gameObject.SetActive(false);
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
            for (int i = 0; i < slots.Length; i++)
            {
                if(slots[i] == selectedSlot && stockedRewards[i] != null)
                {

                    //itemDescription.text = stockedRewards[i].GetDescription();
                    itemDescription.text = stockedRewards[i].name; //--feature testing
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

