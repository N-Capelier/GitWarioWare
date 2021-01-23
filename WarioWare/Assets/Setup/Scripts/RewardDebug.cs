using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class RewardDebug : ScriptableObject
{
    [SerializeField] public List<Rewards.Reward> rewardsList = new List<Rewards.Reward>();
}
