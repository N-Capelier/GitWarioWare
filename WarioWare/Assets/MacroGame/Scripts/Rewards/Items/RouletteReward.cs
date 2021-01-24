using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shop;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Roulette", menuName = "Reward/Item/Roulette", order = 50)]
    public class RouletteReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            ShopManager.Instance.ClearShops();
            ShopManager.Instance.InitializeShop();
            return true;
        }

        public override void ApplyPassiveEffect()
        {
            
        }

        public override string GetDescription()
        {
            return $"Régénère le contenu de la boutique.";
        }

        public override void RemovePassiveEffect()
        {
            
        }
    }
}