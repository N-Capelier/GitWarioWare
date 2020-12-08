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
            int _commonRate, _epicRate;
            if (_totalWeight != 100)
            {
                _commonRate = commonRewardRateWeight * 100 / _totalWeight;
                _epicRate = _commonRate + (epicRewardRateWeight * 100 / _totalWeight);
            }
            else
            {
                _commonRate = commonRewardRateWeight;
                _epicRate = _commonRate + epicRewardRateWeight;
            }

            //Split rewards from rarity
            List<Reward> _commonRewards = new List<Reward>();
            List<Reward> _epicRewards = new List<Reward>();
            List<Reward> _legendaryRewards = new List<Reward>();

            for (int i = 0; i < gameRewards.Length; i++)
            {
                switch (gameRewards[i].rarity)
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
                float index = i / islands.Length * 100;
                if (index <= _commonRate)
                {
                    _generatedReward[i] = _commonRewards[Random.Range(0, _commonRewards.Count)];
                }
                else if (index <= _epicRate)
                {
                    _generatedReward[i] = _epicRewards[Random.Range(0, _epicRewards.Count)];
                }
                else
                {
                    _generatedReward[i] = _legendaryRewards[Random.Range(0, _legendaryRewards.Count)];
                }
            }

            //Shuffle the rewards
            _generatedReward = FisherYates(_generatedReward);

            //Spread the rewards to the islands
            for (int i = 0; i < islands.Length; i++)
            {
                Sprite _islandSprite;
                switch (_generatedReward[i].rarity)
                {
                    case RewardRarity.Common:
                        _islandSprite = islandSprites[0];
                        break;
                    case RewardRarity.Epic:
                        _islandSprite = islandSprites[1];
                        break;
                    case RewardRarity.Legendary:
                        _islandSprite = islandSprites[2];
                        break;
                    default:
                        throw new System.Exception("Sprite out of bounds !");
                }

                islands[i].SetReward(_generatedReward[i], _islandSprite);
            }
        }

        Reward[] FisherYates(Reward[] _rewards)
        {
            for (int i = _rewards.Length - 1; i > 0; i--)
            {
                int _rnd = Random.Range(0, i);

                Reward _temp = _rewards[i];

                _rewards[i] = _rewards[_rnd];
                _rewards[_rnd] = _temp;
            }
            return _rewards;
        }
    }
}