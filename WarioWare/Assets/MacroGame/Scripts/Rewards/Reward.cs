﻿using UnityEngine;

namespace Rewards
{
    public enum RewardRarity
    {
        Common = 0,
        Epic = 1,
        Legendary = 2
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

        public abstract void GiveEffect();
        public abstract void RemoveEffect();
        public abstract void GetDescription();
    }
}