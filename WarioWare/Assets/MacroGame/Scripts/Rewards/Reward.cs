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

    public abstract class Reward : ScriptableObject
    {
        public string rewardName;
        public Sprite sprite;
        public RewardType type;
        public RewardRarity rarity;
        public RewardEffect effect;
        public int price;
        [Range(0, 100)] public int dropRateWeight;

        /// <summary>
        /// Use this method when the player gets the item.
        /// </summary>
        public abstract void ApplyPassiveEffect();

        /// <summary>
        /// Use this method when the player decides to use the item.
        /// </summary>
        public abstract void ApplyActiveEffect();

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