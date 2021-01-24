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
            float r = Random.Range(0, ShopManager.Instance.shopIslands.Length);
            ShopManager.Instance.Show(ShopManager.Instance.shopIslands[(int)Mathf.Floor(r)]);
            return true;
        }

        public override void ApplyPassiveEffect()
        {
            
        }

        public override string GetDescription()
        {
            return $"Accédez instantanément à la boutique.";
        }

        public override void RemovePassiveEffect()
        {
            
        }
    }
}