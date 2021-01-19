using UnityEngine;
using Boss;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Cannonball", menuName = "Reward/Resource/Cannonball", order = 50)]
    public class CannonballReward : Reward
    {
        [SerializeField] public int cannonDamage;
        [HideInInspector] public static int bonusDamages = 1;

        public override bool ApplyActiveEffect()
        {
            return false;
        }

        public override void ApplyPassiveEffect()
        {
            BossLifeManager.Instance.TakeDamage(cannonDamage + bonusDamages);
        }

        public override string GetDescription()
        {
            return $"Infligez {cannonDamage + bonusDamages} dégâts au boss de fin de zone.";
        }

        public override void RemovePassiveEffect()
        {
            
        }
    }
}