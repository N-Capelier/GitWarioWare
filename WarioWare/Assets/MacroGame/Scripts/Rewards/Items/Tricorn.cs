using UnityEngine;
using Caps;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Tricorn", menuName = "Reward/Item/Tricorn", order = 50)]
    public class Tricorn : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            Manager.Instance.tricornBonus += 5;
        }

        public override string GetDescription()
        {
            return "Etre éxalté ou motivé vous fait gagner 5 d'or supplémentaire par cap.";
        }

        public override void RemovePassiveEffect()
        {
            Manager.Instance.tricornBonus -= 5;
        }
    }
}