using UnityEngine;
using Caps;

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
            Manager.Instance.isMainSail++;
        }

        public override string GetDescription()
        {
            return $"Les caps déjà empruntés ne coûntent plus de moral.";
        }

        public override void RemovePassiveEffect()
        {
            Manager.Instance.isMainSail--;
        }
    }
}