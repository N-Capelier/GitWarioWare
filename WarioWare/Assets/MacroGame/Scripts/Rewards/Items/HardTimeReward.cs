using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New HardTime", menuName = "Reward/Item/Hard Time", order = 50)]
    public class HardTimeReward : Reward
    {
        [SerializeField] int beatcoinAmmount = 2;
        [SerializeField] int foodAmmount = 1;

        public override bool ApplyActiveEffect()
        {
            if(PlayerManager.Instance.beatcoins >= beatcoinAmmount)
            {
                PlayerManager.Instance.GainCoins(-beatcoinAmmount);
                PlayerManager.Instance.GainFood(foodAmmount);
                return true;
            }
            return false;
        }

        public override void ApplyPassiveEffect()
        {

        }

        public override string GetDescription()
        {
            return $"Convertit {beatcoinAmmount} beatcoins en {foodAmmount} rations.";
        }

        public override void RemovePassiveEffect()
        {

        }
    }
}