using UnityEngine;
using Caps;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Harpoon", menuName = "Reward/Item/Harpoon", order = 50)]
    public class HarpoonReward : Reward
    {
        [SerializeField] int bonusBarrels;

        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            Manager.Instance.bonusBarrels += bonusBarrels;
        }

        public override string GetDescription()
        {
            return $"Augmente le nombre de tonneaux disponibles durant une série de mini-jeux.";
        }

        public override void RemovePassiveEffect()
        {
            Manager.Instance.bonusBarrels -= 0;
        }
    }
}