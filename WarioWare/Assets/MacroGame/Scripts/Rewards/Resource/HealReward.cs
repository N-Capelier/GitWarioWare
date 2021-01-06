﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Heal", menuName = "Reward/Resource/Heal", order = 50)]
    public class HealReward : Reward
    {
        [SerializeField] int healAmount;
        public override void ApplyActiveEffect()
        {
            
        }

        public override void ApplyPassiveEffect()
        {
            PlayerManager.Instance.Heal(healAmount);
        }

        public override string GetDescription()
        {
            return "Gagnez " + healAmount + " planches";
        }

        public override void RemovePassiveEffect()
        {
            
        }
    }
}