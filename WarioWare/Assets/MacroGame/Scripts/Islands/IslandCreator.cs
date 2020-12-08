using System.Collections.Generic;
using UnityEngine;
using Rewards;

namespace Islands
{
    [DisallowMultipleComponent]
    public class IslandCreator : Singleton<IslandCreator>
    {
        [Header("Game Elements")]

        [SerializeField] Island[] islands;
        [SerializeField] Sprite[] islandSprites;

        [SerializeField] Reward[] gameRewards;

        [Header("Procedural Generation")]

        [SerializeField] [Range(0, 100)] int commonRewardRateWeight;
        [SerializeField] [Range(0, 100)] int epicRewardRateWeight;
        [SerializeField] [Range(0, 100)] int legendaryRewardRateWeight;

        private void Awake()
        {
            CreateSingleton();
        }

        private void Start()
        {
            GenerateRewards();
        }

        void GenerateRewards()
        {
            //Convert weights to percentages
            int _totalWeight = commonRewardRateWeight + epicRewardRateWeight + legendaryRewardRateWeight;
            if(_totalWeight != 100)
            {
                int _commonRate = commonRewardRateWeight * 100 / _totalWeight;
                int _epicRate = epicRewardRateWeight * 100 / _totalWeight;
                int _legendaryRate = legendaryRewardRateWeight * 100 / _totalWeight;
            }

            //Split rewards from rarity
            List<Reward> _commonRewards = new List<Reward>();
            List<Reward> _epicRewards = new List<Reward>();
            List<Reward> _legendaryRewards = new List<Reward>();

            for(int i = 0; i < gameRewards.Length; i++)
            {
                switch(gameRewards[i].rarity)
                {
                    case RewardRarity.Common:
                        _commonRewards.Add(gameRewards[i]);
                        break;
                    case RewardRarity.Epic:
                        _epicRewards.Add(gameRewards[i]);
                        break;
                    case RewardRarity.Legendary:
                        _legendaryRewards.Add(gameRewards[i]);
                        break;
                    default:
                        throw new System.Exception("Missing reward rarity !");
                }
            }

            //Generate the rewards depending on their drop rate
            Reward[] _generatedReward = new Reward[islands.Length];

            for (int i = 0; i < islands.Length; i++)
            {

            }
        }
    }
}