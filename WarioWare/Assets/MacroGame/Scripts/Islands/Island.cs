using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;

namespace Islands
{
    public enum IslandDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
    public class Island : MonoBehaviour
    {
        #region Variables

        //Variables
        IslandDifficulty difficulty;
        Reward reward;

        [SerializeField] Island[] neighbours;

        //Components
        SpriteRenderer spriteRenderer;

        #endregion

        #region Unity Messages

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
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
                    spriteRenderer.sprite = _sprite;
                    break;
                case RewardRarity.Epic:
                    difficulty = IslandDifficulty.Medium;
                    spriteRenderer.sprite = _sprite;
                    break;
                case RewardRarity.Legendary:
                    difficulty = IslandDifficulty.Hard;
                    spriteRenderer.sprite = _sprite;
                    break;
                default:
                    throw new System.Exception("Rarity not linked to difficulty !");
            }
        }

        #endregion
    }
}