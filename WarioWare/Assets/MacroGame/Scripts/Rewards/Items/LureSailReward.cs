using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New LureSail", menuName = "Reward/Item/Lure Sail", order = 50)]
    public class LureSailReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            Manager.Instance.isLureActive++;
            Manager.Instance.isLure = true;
        }

        public override string GetDescription()
        {
            return $"Esquive le premier dégât de chaque cap.";
        }

        public override void RemovePassiveEffect()
        {
            Manager.Instance.isLureActive--;
            Manager.Instance.isLure = false;
        }
    }
}