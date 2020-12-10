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

    public abstract class Reward : ScriptableObject
    {
        public string rewardName;
        public RewardType type;
        public RewardRarity rarity;
        public int price;
        [Range(0, 100)] public int dropRateWeight;

        public abstract void ApplyPassiveEffect();
        public abstract void ApplyActiveEffect();
        public abstract void RemoveEffect();
        public abstract void GetDescription();
    }
}