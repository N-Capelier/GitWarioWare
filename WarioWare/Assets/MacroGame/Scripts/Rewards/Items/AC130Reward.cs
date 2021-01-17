using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New AC-130", menuName = "Reward/Item/AC-130", order = 50)]
    public class AC130Reward : Reward
    {
        [SerializeField] int damages = 10;

        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            CannonballReward.bonusDamages += damages;
        }

        public override string GetDescription()
        {
            return $"Augmente les dégâts du boulet de cannon de {damages}.";
        }

        public override void RemovePassiveEffect()
        {
            CannonballReward.bonusDamages -= damages;
        }
    }
}