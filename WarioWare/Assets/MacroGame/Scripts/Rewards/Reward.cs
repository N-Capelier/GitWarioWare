using UnityEngine;

namespace Rewards
{
    public enum Rarity
    {
        Common = 0,
        Epic = 1,
        Legendary = 2
    }

    public enum Type
    {
        Resource = 0,
        Item = 1,
        CursedItem = 2
    }

    public abstract class Reward : ScriptableObject
    {
        public string rewardName;
        public Type type;
        public Rarity rarity;
        public int price;

        public abstract void GiveEffect();
        public abstract void RemoveEffect();
        public abstract void GetDescription();
    }
}