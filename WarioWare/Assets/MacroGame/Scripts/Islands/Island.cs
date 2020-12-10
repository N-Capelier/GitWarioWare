using UnityEngine;
using System.Collections.Generic;
using Rewards;
using UnityEngine.UI;
using Caps;

namespace Islands
{
    public enum IslandDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
        Legendary = 3,
        Shop = 4
    }
    public class Island : MonoBehaviour
    {
        #region Variables

        //Variables
        public IslandDifficulty difficulty;
        [HideInInspector] public Reward reward;

        public Island[] neighbours;
        public Island[] accessibleNeighbours;
        [HideInInspector] public List<Cap> capList = new List<Cap>();

        //Components
        Image image;
        [HideInInspector] public Button button;

        #endregion

        #region Unity Messages

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init the island with this method.
        /// </summary>
        /// <param name="_reward"></param>
        public void SetReward(Reward _reward, Sprite _sprite)
        {
            reward = _reward;
            switch (_reward.rarity)
            {
                case RewardRarity.Common:
                    difficulty = IslandDifficulty.Easy;
                    image.sprite = _sprite;
                    break;
                case RewardRarity.Rare:
                    difficulty = IslandDifficulty.Medium;
                    image.sprite = _sprite;
                    break;
                case RewardRarity.Epic:
                    difficulty = IslandDifficulty.Hard;
                    image.sprite = _sprite;
                    break;
                default:
                    throw new System.Exception("Rarity not linked to difficulty !");
            }
        }

        #endregion
    }
}