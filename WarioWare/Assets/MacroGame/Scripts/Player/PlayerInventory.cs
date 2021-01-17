using UnityEngine;
using UnityEngine.UI;
using Rewards;
using Caps;
using TMPro;
using Shop;
using Sound;

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
        public TextMeshProUGUI itemName;

        [Header("Override Confirmation")]
        public GameObject overrideConfirmationPanel;
        public Button noButton;
        private int indexToOverride;

        [Header("Reward UI")]
        public GameObject rewardCanvas;
        public Image rewardImage;

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
            //rewardToAdd = stockedRewards[0]; //-- feature testing
        }

        void Update()
        {

        }
        
        public void Show()
        {
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
            }
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
            ShowSelectedSlotInfo(slots[slot]);
        }

        public void OverrideInventorySlot(Reward newItem, int slot)
        {
            stockedRewards[slot].RemovePassiveEffect();
            stockedRewards[slot] = newItem;
            rewardImages[slot].gameObject.SetActive(true);
            rewardImages[slot].sprite = newItem.sprite;
            rewardToAdd = null;
            rewardToAddImage.gameObject.SetActive(false);
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

    }
}