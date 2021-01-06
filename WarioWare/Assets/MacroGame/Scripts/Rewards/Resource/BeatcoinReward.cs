using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Beatcoin", menuName = "Reward/Resource/Beatcoin", order = 50)]
    public class BeatcoinReward : Reward
    {
        [SerializeField] int beatcoinAmount;
        public override void ApplyActiveEffect()
        {

        }

        public override void ApplyPassiveEffect()
        {
            PlayerManager.Instance.GainCoins(beatcoinAmount);
        }

        public override string GetDescription()
        {
            return "Gagnez " + beatcoinAmount + " Beatcoins";
        }

        public override void RemovePassiveEffect()
        {

        }
    }
}