using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Sturdy", menuName = "Reward/Item/Sturdy", order = 50)]
    public class SturdyReward : Reward
    {
        [SerializeField] int healAmmount = 3;
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            PlayerManager.Instance.isSturdy++;
            PlayerManager.Instance.sturdyHealAmmount = healAmmount;
        }

        public override string GetDescription()
        {
            return $"Si vous perdez toutes vos planches, récupérez {healAmmount} planches.";
        }

        public override void RemovePassiveEffect()
        {
            PlayerManager.Instance.isSturdy--;
        }
    }
}