using System.Collections.Generic;
using UnityEngine;
using Rewards;
using Caps;
using Player;

namespace Islands
{
    [DisallowMultipleComponent]
    public class IslandCreator : Singleton<IslandCreator>
    {
        [Header("Game Elements")]

        public Island[] islands;
        [SerializeField] Sprite[] islandSprites;

        [SerializeField] Reward[] gameRewards;

        [Header("Procedural Generation")]

        [SerializeField] [Range(0, 100)] int commonRewardRateWeight;
        [SerializeField] [Range(0, 100)] int rareRewardRateWeight;
        [SerializeField] [Range(0, 100)] int epicRewardRateWeight;

        [Space]

        [SerializeField] [Range(0, 100)] [Tooltip("In percentage")] int commonRewardRandomness;
        [SerializeField] [Range(0, 100)] [Tooltip("In percentage")] int rareRewardRandomness;

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
            //Apply randomness to weights
            commonRewardRateWeight += Random.Range(commonRewardRateWeight - commonRewardRateWeight * commonRewardRandomness / 100, commonRewardRandomness + commonRewardRateWeight * commonRewardRandomness / 100);
            rareRewardRateWeight += Random.Range(rareRewardRateWeight - rareRewardRateWeight * rareRewardRandomness / 100, rareRewardRandomness + rareRewardRateWeight * rareRewardRandomness / 100);

            //Convert weights to percentages
            int _totalWeight = commonRewardRateWeight + rareRewardRateWeight + epicRewardRateWeight;
            int _commonRate, _rareRate;
            if (_totalWeight != 100)
            {
                _commonRate = commonRewardRateWeight * 100 / _totalWeight;
                _rareRate = _commonRate + (rareRewardRateWeight * 100 / _totalWeight);
            }
            else
            {
                _commonRate = commonRewardRateWeight;
                _rareRate = _commonRate + rareRewardRateWeight;
            }

            //Split rewards from rarity
            List<Reward> _commonRewards = new List<Reward>();
            List<Reward> _rareRewards = new List<Reward>();
            List<Reward> _epicRewards = new List<Reward>();

            for (int i = 0; i < gameRewards.Length; i++)
            {
                switch (gameRewards[i].rarity)
                {
                    case RewardRarity.Common:
                        _commonRewards.Add(gameRewards[i]);
                        break;
                    case RewardRarity.Rare:
                        _rareRewards.Add(gameRewards[i]);
                        break;
                    case RewardRarity.Epic:
                        _epicRewards.Add(gameRewards[i]);
                        break;
                    default:
                        break;
                }
            }

            //Filter and keep "classic" islands
            List<Island> _generatedIslandsList = new List<Island>();

            for (int i = 0; i < islands.Length; i++)
            {
                if (islands[i].difficulty == IslandDifficulty.Legendary || islands[i].difficulty == IslandDifficulty.Shop)
                    continue;
                _generatedIslandsList.Add(islands[i]);
            }

            Island[] _generatedIslands = _generatedIslandsList.ToArray();

            //Generate the rewards depending on their drop rate
            Reward[] _generatedReward = new Reward[_generatedIslands.Length];

            for (int i = 0; i < _generatedIslands.Length; i++)
            {
                float index = i / _generatedIslands.Length * 100;
                if (index <= _commonRate)
                {
                    _generatedReward[i] = _commonRewards[Random.Range(0, _commonRewards.Count)];
                }
                else if (index <= _rareRate)
                {
                    _generatedReward[i] = _rareRewards[Random.Range(0, _rareRewards.Count)];
                }
                else
                {
                    _generatedReward[i] = _epicRewards[Random.Range(0, _epicRewards.Count)];
                }
            }

            //Shuffle the rewards
            _generatedReward = FisherYates(_generatedReward);

            //Spread the rewards to the islands
            for (int i = 0; i < _generatedIslands.Length; i++)
            {
                Sprite _islandSprite;
                switch (_generatedReward[i].rarity)
                {
                    case RewardRarity.Common:
                        _islandSprite = islandSprites[0];
                        break;
                    case RewardRarity.Rare:
                        _islandSprite = islandSprites[1];
                        break;
                    case RewardRarity.Epic:
                        _islandSprite = islandSprites[2];
                        break;
                    default:
                        throw new System.Exception("Island difficulty not set!");
                }

                _generatedIslands[i].SetReward(_generatedReward[i], _islandSprite);
            }
            Manager.Instance.islandList = islands;
            Manager.Instance.CapAttribution();
            Manager.Instance.ResetIDCards();
            PlayerMovement.Instance.islands = islands;
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