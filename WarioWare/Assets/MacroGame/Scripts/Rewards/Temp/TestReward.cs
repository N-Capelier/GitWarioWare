using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Test Reward", menuName = "Reward/Test Reward", order = 50)]
    public class TestReward : Reward
    {
        public override string GetDescription()
        {
            Debug.Log("This is a reward.");
            return rewardName;
        }

        public override void ApplyPassiveEffect()
        {
            Debug.Log("Got the reward!");
        }

        public override void RemovePassiveEffect()
        {
            Debug.Log("Lost my reward!");
        }

        public override bool ApplyActiveEffect()
        {
            Debug.Log("Reward used!");
            return true;
        }
    }
}