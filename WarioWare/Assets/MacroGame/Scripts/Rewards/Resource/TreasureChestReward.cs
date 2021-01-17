using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Islands;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Treasure Chest", menuName = "Reward/Resource/Treasure", order = 50)]
    public class TreasureChestReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            Reward _reward = IslandCreator.Instance.treasureItems[Random.Range(0, IslandCreator.Instance.treasureItems.Length)];
            //Add treasure animation here
            PlayerInventory.Instance.SetItemToAdd(_reward);
        }

        public override string GetDescription()
        {
            return "Découvrez un objet dans un coffre !";
        }

        public override void RemovePassiveEffect()
        {
            
        }
    }
}