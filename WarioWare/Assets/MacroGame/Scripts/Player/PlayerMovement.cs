﻿using UnityEngine;
using Islands;
using Caps;
using DG.Tweening;
using Shop;
using Rewards;
using NUnit.Framework;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Player
{
    public class PlayerMovement : Singleton<PlayerMovement>
    {
        public GameObject playerAvatar;
        
        [Header("Island Refs")]
        public Island playerIsland;
        [SerializeField] Island bossIsland;
        
        public Island[] islands;
        [HideInInspector] public Island selectedIsland;
        private Island lastSelectedIsland;
        private GameObject previousDescription;
        Clock transitionTimer;

        [Header("Island UI")]
        public Sprite resourceSprite;
        public Sprite shopSprite;
        public Sprite bossSprite;
        public Sprite treasureSprite;
        public Sprite keystoneSprite;

        public Sprite goldRessource;
        public Sprite foodRessource;
        public Sprite cannonball;
        public Sprite healthRessource;



        [SerializeField] int foodPrice = 10;
        [SerializeField] int damagesWhenNoFood = 10;

        [HideInInspector] public bool isMoving = false;

        [HideInInspector] public List<Island> farNeighbors = new List<Island>();

        private void Awake()
        {
            CreateSingleton();
        }

        void Start()
        {
            playerAvatar.transform.position = playerIsland.anchorPoint.position;

            transitionTimer = new Clock();
            //set up value from debug
            foodPrice = DebugToolManager.Instance.ChangeVariableValue("foodPrice");
            damagesWhenNoFood = DebugToolManager.Instance.ChangeVariableValue("damagesWhenNoFood");
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

        [HideInInspector] public int isMainSail = 0;

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
                ShopManager.Instance.Show(playerIsland);            
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
                    if(!(isMainSail > 0 && _isCapDone))
                        PlayerManager.Instance.GainFood(-foodPrice);
                }
                else
                {
                    if (!(isMainSail > 0 && _isCapDone))
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
                            Manager.Instance.eventSystem.firstSelectedGameObject = targetIsland.gameObject;
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
            selectedIsland = targetIsland;
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
                    /*switch (targetIsland.reward.type)
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
                    }*/
                    switch(targetIsland.difficulty)
                    {
                        case IslandDifficulty.Easy:
                            targetIsland.islandRewardImage.sprite = targetIsland.reward.sprite;
                            targetIsland.rewardDescription.text = "Gagnez quelques ressources !";
                            break;
                        case IslandDifficulty.Medium:
                            targetIsland.islandRewardImage.sprite = targetIsland.reward.sprite;
                            targetIsland.rewardDescription.text = "Gagnez beaucoup de ressources !";
                            break;
                        case IslandDifficulty.Hard:
                            targetIsland.islandRewardImage.sprite = treasureSprite;
                            targetIsland.rewardDescription.text = "Découvrez un coffre au trésor !";
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
                        if(targetIsland.type != IslandType.Boss)
                        { 
                            targetIsland.capLength.text = playerIsland.capList[i].length.ToString() + " mini-jeux.";
                        }
                        else
                        {
                            targetIsland.capLength.text = "";
                        }
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
            Manager.Instance.eventSystem.SetSelectedGameObject(playerIsland.gameObject);
        }

        private void GetFarNeighbours()
        {
            List<Island> tempList = new List<Island>();

            for (int i = 0; i < playerIsland.accessibleNeighbours.Length; i++)
            {
                tempList.Add(playerIsland.accessibleNeighbours[i]);

                for (int j = 0; j < playerIsland.accessibleNeighbours[i].accessibleNeighbours.Length; j++)
                {
                    tempList.Add(playerIsland.accessibleNeighbours[i].accessibleNeighbours[j]);

                    for (int k = 0; k < playerIsland.accessibleNeighbours[i].accessibleNeighbours[j].accessibleNeighbours.Length; k++)
                    {
                        tempList.Add(playerIsland.accessibleNeighbours[i].accessibleNeighbours[j].accessibleNeighbours[k]);
                    }
                }
            }

            //Remove duplicate and playerIsland from temp List and add to farNeighbors
            var noDupes = new HashSet<Island>(tempList);
            foreach (var item in noDupes)
            {
                if(item != playerIsland)
                    farNeighbors.Add(item);
            }
        }

        public void ShowFarNeighboursIcon()
        {
            GetFarNeighbours();

            for (int i = 0; i < farNeighbors.Count; i++)
            {
                farNeighbors[i].icon.gameObject.SetActive(true);
                if(farNeighbors[i].reward!=null)
                {
                    switch (farNeighbors[i].reward.rewardName)
                    {

                        case "Sac de Beatcoins":
                            farNeighbors[i].icon.sprite = goldRessource;
                            break;
                        case "Ration"://a changer avec rhum
                            farNeighbors[i].icon.sprite = foodRessource;
                            break;
                        case "Boulet de cannon":
                            farNeighbors[i].icon.sprite = cannonball;
                            break;
                        case "Planche de bois":
                            farNeighbors[i].icon.sprite = healthRessource;
                            break;
                        case "Black Captain-Card":
                            farNeighbors[i].icon.sprite = goldRessource;
                            break;
                        case "Kaisse en kit":
                            farNeighbors[i].icon.sprite = healthRessource;
                            break;
                        case "Pack de rations"://a changer avec rhum
                            farNeighbors[i].icon.sprite = foodRessource;
                            break;
                        case "Coffre au trésor"://a changer avec rhum
                            farNeighbors[i].icon.sprite = treasureSprite;
                            break;

                        default:
                            break;
                    }
                }
            }

            //Show persistent icons
            for (int i = 0; i < islands.Length; i++)
            {
                switch (islands[i].type)
                {
                    case IslandType.Shop:
                        //afficher icone shop
                        islands[i].icon.sprite = shopSprite;
                        islands[i].icon.gameObject.SetActive(true);
                        break;
                    case IslandType.Keystone:
                        //afficher icone keystone
                        islands[i].icon.sprite = keystoneSprite;
                        islands[i].icon.gameObject.SetActive(true);
                        break;
                    case IslandType.Boss:
                        //Afficher icone boss
                        islands[i].icon.sprite = bossSprite;
                        islands[i].icon.gameObject.SetActive(true);
                        break;
                    default:
                        //Debug.Log("Error : Island has no type !" + islands[i].gameObject.name);
                        break;
                }
            }

        }

        public void HideIcons()
        {
            for (int i = 0; i < islands.Length; i++)
            {
                islands[i].icon.gameObject.SetActive(false);
            }
        }
    }
}

