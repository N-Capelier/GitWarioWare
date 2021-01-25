using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Empty", menuName = "Reward/Resource/Empty", order = 50)]
    public class EmptyReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {

        }

        public override string GetDescription()
        {
            return $"Il n'y a rien sur cette île !";
        }

        public override void RemovePassiveEffect()
        {

        }
    }
}