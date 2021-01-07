using UnityEngine;
using Islands;
using Caps;
using DG.Tweening;
using Shop;

namespace Player
{
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public GameObject playerAvatar;
        [HideInInspector] public Island[] islands;
        public Island playerIsland;

        private Island lastSelectedIsland;
        private GameObject previousDescription;

        Clock transitionTimer;

        [SerializeField] Island bossIsland;

        private void Awake()
        {
            CreateSingleton();
        }

        void Start()
        {
            //Initialize Connections
            ClearConnections();
            GetNeighbors();
            playerAvatar.transform.position = playerIsland.anchorPoint.position;

            transitionTimer = new Clock();
        }

        private void Update()
        {
            if(transitionTimer.onFinish)
                playerAvatar.transform.position = playerIsland.anchorPoint.position;

            if(playerIsland == bossIsland)
            {
                PlayerManager.Instance.TakeDamage(20);
            }
        }

        /// <summary>
        /// Resets all Island UI buttons of the zone to not interactable.
        /// </summary>
        private void ClearConnections()
        {
            for (int i = 0; i < islands.Length; i++)
            {
                islands[i].button.interactable = false;
            }
        }

        /// <summary>
        /// Get the player's current island neighbors and set their UI to interactable.
        /// </summary>
        private void GetNeighbors()
        {
            playerIsland.button.interactable = true;
            for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
            {
                playerIsland.accessibleNeighbours[i].button.interactable = true;
            }
            playerIsland.button.Select();
        }

        /// <summary>
        /// Move the player from his current island to the target island. Moving costs 1 food, if 0 food then it costs 1 hp.
        /// </summary>
        /// <param name="targetIsland">Which island is the player going to.</param>
        public void Move(Island targetIsland)
        {
            Debug.Log(targetIsland.name);
            Debug.Log(playerIsland.name);
            if(targetIsland == playerIsland && playerIsland.type == IslandType.Shop)
            {
                ShopManager.Instance.Show();            
            }

            bool _isCapDone = false;

            if (targetIsland != playerIsland)
            {
                if (PlayerManager.Instance.food > 0)
                {
                    PlayerManager.Instance.GainFood(-1);
                }
                else
                {
                    PlayerManager.Instance.TakeDamage(1);
                }

                //Lancer le cap + check if not dead
                if(PlayerManager.Instance.playerHp > 0)
                {
                    for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
                    {
                        if (playerIsland.accessibleNeighbours[i] == targetIsland)
                        {
                            //COUPER LES INPUTS DE LA MACRO

                            StartCoroutine(Manager.Instance.StartMiniGame(playerIsland.capList[i]));
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
                
                //Debug.Log(targetIsland.gameObject.name + " has " + targetIsland.accessibleNeighbours.Length + " neighbor(s).");
                
                //show UI here
                targetIsland.islandDescriptionContainer.transform.position = targetIsland.anchorPoint.transform.position; //replace description box
                targetIsland.rewardDescription.text = targetIsland.reward.GetDescription();
                //targetIsland.islandRewardImage.sprite = targetIsland.reward.sprite;
                 for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
                 {
                    if (playerIsland.accessibleNeighbours[i] == targetIsland)
                    {
                        targetIsland.capLength.text = playerIsland.capList[i].length.ToString();
                        break;
                    }
                 }
                targetIsland.islandDescriptionContainer.SetActive(true);
                previousDescription = targetIsland.islandDescriptionContainer;
            }
        }

        public void ResetFocus()
        {
            playerIsland.button.Select();
        }
    }
}

