using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New HardTime", menuName = "Reward/Item/Hard Time", order = 50)]
    public class HardTimeReward : Reward
    {
        [SerializeField] public int beatcoinAmount = 2;
        [SerializeField] public int foodAmount = 1;

        public override bool ApplyActiveEffect()
        {
            if(PlayerManager.Instance.beatcoins >= beatcoinAmount)
            {
                PlayerManager.Instance.GainCoins(-beatcoinAmount);
                PlayerManager.Instance.GainFood(foodAmount);
                return true;
            }
            return false;
        }

        public override void ApplyPassiveEffect()
        {

        }

        public override string GetDescription()
        {
            return $"Cuisinez {beatcoinAmount} beatcoins pour gagner {foodAmount} points en moral.";
        }

        public override void RemovePassiveEffect()
        {

        }
    }
}