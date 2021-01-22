using UnityEngine;

namespace Rewards
{
    public enum RewardRarity
    {
        Common = 0,
        Rare = 1,
        Epic = 2,
        Legendary = 3
    }

    public enum RewardType
    {
        Resource = 0,
        Item = 1,
        CursedItem = 2
    }

    public enum RewardEffect
    {
        Active,
        Passive
    }
    [System.Serializable]
    public abstract class Reward : ScriptableObject
    {
        [SerializeField] public string rewardName;
        [SerializeField] public Sprite sprite;
        [SerializeField] public RewardType type;
        [SerializeField] public RewardRarity rarity;
        [SerializeField] public RewardEffect effect;
        [SerializeField] public int price;
        [SerializeField] [Range(0, 100)] public int dropRateWeight;

        /// <summary>
        /// Use this method when the player gets the item.
        /// </summary>
        public abstract void ApplyPassiveEffect();

        /// <summary>
        /// Use this method when the player decides to use the item.
        /// </summary>
        public abstract bool ApplyActiveEffect();

        /// <summary>
        /// Use this method when the player uses or deletes the item.
        /// </summary>
        public abstract void RemovePassiveEffect();

        /// <summary>
        /// Use this method to get the description and stats of the item.
        /// </summary>
        public abstract string GetDescription();
    }
}