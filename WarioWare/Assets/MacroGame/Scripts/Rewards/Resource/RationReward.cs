﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Ration", menuName = "Reward/Resource/Ration", order = 50)]
    public class RationReward : Reward
    {
        [SerializeField] int foodAmount;
        public override void ApplyActiveEffect()
        {

        }

        public override void ApplyPassiveEffect()
        {
            PlayerManager.Instance.GainFood(foodAmount);
        }

        public override string GetDescription()
        {
            return "Gagnez " + foodAmount + " rations";
        }

        public override void RemovePassiveEffect()
        {

        }
    }
}