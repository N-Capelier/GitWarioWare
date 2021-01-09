using UnityEngine;
using Islands;
using Caps;
using DG.Tweening;
using Shop;
using Rewards;
using NUnit.Framework;

namespace Player
{
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public GameObject playerAvatar;
        
        [Header("Island Refs")]
        public Island playerIsland;
        [SerializeField] Island bossIsland;
        
        [HideInInspector] public Island[] islands;
        private Island lastSelectedIsland;
        private GameObject previousDescription;
        Clock transitionTimer;

        [Header("Island UI")]
        public Sprite resourceSprite;
        public Sprite shopSprite;
        public Sprite bossSprite;
        public Sprite treasureSprite;
        public Sprite rareTreasureSprite;



        [SerializeField] int foodPrice = 10;
        [SerializeField] int damagesWhenNoFood = 10;

        [HideInInspector] public bool isMoving = false;

        private void Awake()
        {
            CreateSingleton();
        }

        void Start()
        {
            playerAvatar.transform.position = playerIsland.anchorPoint.position;

            transitionTimer = new Clock();
        }

        private void Update()
        {
            if(transitionTimer.onFinish)
                playerAvatar.transform.position = playerIsland.anchorPoint.position;

            /* Pre-Alpha zone transition
            if(playerIsland == bossIsland)
            {
                PlayerManager.Instance.TakeDamage(20);
            }
            */
        }

        /// <summary>
        /// Resets all Island UI buttons of the zone to not interactable.
        /// </summary>
        public void ClearConnections()
        {
            for (int i = 0; i < islands.Length; i++)
            {
                islands[i].button.interactable = false;
            }
        }

        /// <summary>
        /// Get the player's current island neighbors and set their UI to interactable.
        /// </summary>
        public void GetNeighbors()
        {
            playerIsland.button.interactable = true;
            for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
            {
                playerIsland.accessibleNeighbours[i].button.interactable = true;
            }
            playerIsland.button.Select(); 
        }

        [HideInInspector] public bool isMainSail = false;

        /// <summary>
        /// Move the player from his current island to the target island. Moving costs 1 food, if 0 food then it costs 1 hp.
        /// </summary>
        /// <param name="targetIsland">Which island is the player going to.</param>
        public void Move(Island targetIsland)
        {
            isMoving = true;
            Debug.Log(targetIsland.name);
            Debug.Log(playerIsland.name);
            if(targetIsland == playerIsland && playerIsland.type == IslandType.Shop)
            {
                ShopManager.Instance.Show();            
            }

            bool _isCapDone = false;

            if (targetIsland != playerIsland)
            {
                for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
                {
                    if (playerIsland.accessibleNeighbours[i] == targetIsland)
                    {
                        if (playerIsland.capList[i].isDone)
                        {
                            _isCapDone = true;
                        }
                    }
                }

                if (PlayerManager.Instance.food > 0)
                {
                    if(!isMainSail && !_isCapDone)
                        PlayerManager.Instance.GainFood(foodPrice);
                }
                else
                {
                    if(!isMainSail && !_isCapDone)
                        PlayerManager.Instance.TakeDamage(damagesWhenNoFood);
                }

                //Lancer le cap + check if not dead
                if(PlayerManager.Instance.playerHp > 0)
                {
                    for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
                    {
                        if (playerIsland.accessibleNeighbours[i] == targetIsland)
                        {
                            //COUPER LES INPUTS DE LA MACRO

                            StartCoroutine(Manager.Instance.StartMiniGame(playerIsland.capList[i], targetIsland));
                            if (playerIsland.capList[i].isDone)
                            {
                                _isCapDone = true;
                            }
                        }
                    }
                }
                playerIsland = targetIsland;
                ClearConnections();
                GetNeighbors();

                if (_isCapDone)
                    playerAvatar.transform.position = targetIsland.anchorPoint.position;
                else
                    transitionTimer.SetTime(3f);

                isMoving = false;
            }

        }


        /// <summary>
        /// Show the selected island's UI informations.
        /// </summary>
        /// <param name="targetIsland">Which island is the player selecting.</param>
        public void ShowSelectedIslandInfo(Island targetIsland)
        {
            //Selection security
            if (targetIsland.button.interactable)
            {
                lastSelectedIsland = targetIsland;
            }
            else
            {
                lastSelectedIsland.button.Select();
            }

            if(previousDescription != null)
                previousDescription.SetActive(false);

            if (targetIsland != playerIsland)
            {
                targetIsland.islandDescriptionContainer.transform.position = targetIsland.anchorPoint.transform.position; //replace description box

                if(targetIsland.reward != null)
                {
                    switch (targetIsland.reward.type)
                    {
                        case RewardType.Resource:
                            targetIsland.islandRewardImage.sprite = targetIsland.reward.sprite;
                            targetIsland.rewardDescription.text = "Récupérez des ressources!"; 
                            break;
                        case RewardType.Item:
                            targetIsland.islandRewardImage.sprite = treasureSprite;
                            targetIsland.rewardDescription.text = "Récupérez un coffre au trésor!"; 
                            break;
                        case RewardType.CursedItem:
                            targetIsland.islandRewardImage.sprite = rareTreasureSprite;
                            targetIsland.rewardDescription.text = "Récupérez un coffre maudit!";
                            break;
                    }
                }
                switch (targetIsland.type)
                {
                    case IslandType.Shop:
                        targetIsland.islandRewardImage.sprite = shopSprite;
                        targetIsland.rewardDescription.text = "Au bonheur des pirates";
                        break;

                    case IslandType.Boss:
                        targetIsland.islandRewardImage.sprite = bossSprite;
                        targetIsland.rewardDescription.text = "Affrontez le Galion Champion!";
                        break;
                }

                for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
                {
                    if (playerIsland.accessibleNeighbours[i] == targetIsland)
                    {
                        targetIsland.capLength.text = playerIsland.capList[i].length.ToString() + " mini-jeux.";
                        break;
                    }
                }
                targetIsland.islandDescriptionContainer.SetActive(true);
                previousDescription = targetIsland.islandDescriptionContainer;
            }
        }

        /// <summary>
        /// Select the playerIsland Button.
        /// </summary>
        public void ResetFocus()
        {
            playerIsland.button.Select();
        }
    }
}

