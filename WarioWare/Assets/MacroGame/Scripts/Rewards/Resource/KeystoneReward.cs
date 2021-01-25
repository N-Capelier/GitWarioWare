using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Keystone", menuName = "Reward/Resource/Keystone", order = 50)]
    public class KeystoneReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            PlayerManager.Instance.GainKeyStone();
        }

        public override string GetDescription()
        {
            return "Affaibli le galion champion.";
        }

        public override void RemovePassiveEffect()
        {

        }
    }
}