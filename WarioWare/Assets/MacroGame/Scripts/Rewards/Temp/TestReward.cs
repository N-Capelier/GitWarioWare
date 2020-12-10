﻿using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = "New Test Reward", menuName = "Reward/Test Reward", order = 50)]
    public class TestReward : Reward
    {
        public override void GetDescription()
        {
            Debug.Log("This is a reward.");
        }

        public override void GiveEffect()
        {
            Debug.Log("Got the reward!");
        }

        public override void RemoveEffect()
        {
            Debug.Log("Lost my reward!");
        }
    }
}