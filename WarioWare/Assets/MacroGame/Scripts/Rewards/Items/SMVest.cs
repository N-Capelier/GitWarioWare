using UnityEngine;
using Player;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New SM vest", menuName = "Reward/Item/SM vest", order = 50)]
    public class SMVest : Reward
    {
        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            PlayerManager.Instance.SMVestCount++;
        }

        public override string GetDescription()
        {
            return "Prendre des dégâts vous fait gagner 10 de moral.";
        }

        public override void RemovePassiveEffect()
        {
            PlayerManager.Instance.SMVestCount--;

        }
    }
}