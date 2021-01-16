using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New MainSail", menuName = "Reward/Item/Main Sail", order = 50)]
    public class MainSailReward : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            PlayerMovement.Instance.isMainSail++;
        }

        public override string GetDescription()
        {
            return $"Les caps déjà empruntés ne coûntent plus aucune ration.";
        }

        public override void RemovePassiveEffect()
        {
            PlayerMovement.Instance.isMainSail--;
        }
    }
}