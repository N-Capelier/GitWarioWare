using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shop;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New ArghmazonPrime", menuName = "Reward/Item/Arghmazon Prime", order = 50)]
    public class ArghmazonPrimeReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            int r = Random.Range(0, ShopManager.Instance.shopIslands.Length - 1);
            ShopManager.Instance.Show(ShopManager.Instance.shopIslands[r]);
            return true;
        }

        public override void ApplyPassiveEffect()
        {
            
        }

        public override string GetDescription()
        {
            return $"Accédez instantanément à une boutique.";
        }

        public override void RemovePassiveEffect()
        {
            
        }
    }
}