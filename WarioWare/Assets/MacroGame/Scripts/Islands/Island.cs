using UnityEngine;
using Rewards;
using UnityEngine.UI;

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
        [HideInInspector] public IslandDifficulty difficulty;
        [HideInInspector] public Reward reward;

        public Island[] neighbours;

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
                case RewardRarity.Epic:
                    difficulty = IslandDifficulty.Medium;
                    image.sprite = _sprite;
                    break;
                case RewardRarity.Legendary:
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