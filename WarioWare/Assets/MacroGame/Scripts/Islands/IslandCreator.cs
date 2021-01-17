using System.Collections.Generic;
using UnityEngine;
using Rewards;
using Caps;
using Player;
using UnityEngine.EventSystems;

namespace Islands
{
    [DisallowMultipleComponent]
    public class IslandCreator : Singleton<IslandCreator>
    {
        [Header("Game Elements")]

        public Island[] islands;

        [Space]

        [SerializeField] IslandSprite[] easyIslandSprites;
        [SerializeField] IslandSprite[] mediumIslandSprites;
        [SerializeField] IslandSprite[] hardIslandSprites;

        [Space]

        public Reward[] gameRewards;

        public Reward[] islandRewards;

        public Reward[] treasureItems;

        [Header("Procedural Generation")]

        [SerializeField] [Range(0, 100)] int commonRewardRateWeight;
        [SerializeField] [Range(0, 100)] int rareRewardRateWeight;
        [SerializeField] [Range(0, 100)] int epicRewardRateWeight;

        [Space]

        [SerializeField] [Range(0, 100)] [Tooltip("In percentage")] int commonRewardRandomness;
        [SerializeField] [Range(0, 100)] [Tooltip("In percentage")] int rareRewardRandomness;

        public EventSystem eventSystem = null;

        private void Awake()
        {
            CreateSingleton();
        }

        private void Start()
        {
            //set up value from debug
            commonRewardRateWeight = DebugToolManager.Instance.ChangeVariableValue("commonRewardRateWeight");
            rareRewardRateWeight = DebugToolManager.Instance.ChangeVariableValue("rareRewardRateWeight");
            epicRewardRateWeight = DebugToolManager.Instance.ChangeVariableValue("epicRewardRateWeight");
            commonRewardRandomness = DebugToolManager.Instance.ChangeVariableValue("commonRewardRandomness");
            rareRewardRandomness = DebugToolManager.Instance.ChangeVariableValue("rareRewardRandomness");
            GenerateIslands();
        }

        void GenerateIslands()
        {
            //Apply randomness to weights
            commonRewardRateWeight += Random.Range(commonRewardRateWeight * commonRewardRandomness / 100, commonRewardRateWeight * commonRewardRandomness / 100);
            rareRewardRateWeight += Random.Range(rareRewardRateWeight * rareRewardRandomness / 100, rareRewardRateWeight * rareRewardRandomness / 100);

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

            //Split rewards from their rarity
            List<Reward> _commonRewards = new List<Reward>();
            List<Reward> _rareRewards = new List<Reward>();
            List<Reward> _epicRewards = new List<Reward>();

            for (int i = 0; i < islandRewards.Length; i++)
            {
                switch (islandRewards[i].rarity)
                {
                    case RewardRarity.Common:
                        _commonRewards.Add(islandRewards[i]);
                        break;
                    case RewardRarity.Rare:
                        _rareRewards.Add(islandRewards[i]);
                        break;
                    case RewardRarity.Epic:
                        _epicRewards.Add(islandRewards[i]);
                        break;
                    default:
                        break;
                }
            }

            //Filter and keep "classic" islands
            List<Island> _generatedIslandsList = new List<Island>();

            for (int i = 0; i < islands.Length; i++)
            {
                if(islands[i].type == IslandType.Shop || islands[i].type == IslandType.Start)
                {
                    continue;
                }
                if (islands[i].difficulty == IslandDifficulty.Easy || islands[i].difficulty == IslandDifficulty.Medium || islands[i].difficulty == IslandDifficulty.Hard)
                    _generatedIslandsList.Add(islands[i]);
            }

            Island[] _generatedIslands = _generatedIslandsList.ToArray();

            //Generate the rewards depending on their drop rate
            Reward[] _generatedReward = new Reward[_generatedIslands.Length];

            for (int i = 0; i < _generatedIslands.Length; i++)
            {
                float index = i * 100 / _generatedIslands.Length;
                if (index <= _commonRate)
                {
                    _generatedReward[i] = GetRandomReward(_commonRewards.ToArray());
                }
                else if (index <= _rareRate)
                {
                    _generatedReward[i] = GetRandomReward(_rareRewards.ToArray());
                }
                else
                {
                    _generatedReward[i] = GetRandomReward(_epicRewards.ToArray());
                }
            }

            //Shuffle the rewards
            _generatedReward = FisherYates(_generatedReward);

            //Spread the rewards through the islands
            for (int i = 0; i < _generatedIslands.Length; i++)
            {
                IslandSprite _islandSprite;
                switch (_generatedReward[i].rarity)
                {
                    case RewardRarity.Common:
                        _islandSprite = easyIslandSprites[Random.Range(0, easyIslandSprites.Length)];
                        break;
                    case RewardRarity.Rare:
                        _islandSprite = mediumIslandSprites[Random.Range(0, mediumIslandSprites.Length)];
                        break;
                    case RewardRarity.Epic:
                        _islandSprite = hardIslandSprites[Random.Range(0, hardIslandSprites.Length)];
                        break;
                    default:
                        throw new System.Exception("Island difficulty not set!");
                }

                _generatedIslands[i].SetReward(_generatedReward[i], _islandSprite);
            }
            Manager.Instance.allIslands = islands;
            Manager.Instance.ResetIDCards();
            Manager.Instance.CapAttribution();
            PlayerMovement.Instance.islands = islands;
            PlayerMovement.Instance.ClearConnections();
            PlayerMovement.Instance.GetNeighbors();
        }

        private Reward GetRandomReward(Reward[] _rewards)
        {
            //Convert weights to percentages
            int _totalWeight = 0;
            foreach(Reward _reward in _rewards)
            {
                _totalWeight += _reward.dropRateWeight;
            }

            int[] _percentages = new int[_rewards.Length];

            if(_totalWeight != 100)
            {
                for (int i = 0; i < _rewards.Length; i++)
                {
                    int _push = 0;
                    if(i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            _push += _percentages[j];
                        }
                    }
                    _percentages[i] = _push + (_rewards[i].dropRateWeight * 100 / _totalWeight);
                }
            }
            else
            {
                for (int i = 0; i < _rewards.Length; i++)
                {
                    int _push = 0;
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            _push += _percentages[j];
                        }
                    }
                    _percentages[i] = _push + _rewards[i].dropRateWeight;
                }
            }

            //Generate the rewards depending on their drop weight
            int index = Random.Range(0, 100);

            for (int i = 0; i < _rewards.Length; i++)
            {
                if (index <= _percentages[i])
                {
                    return _rewards[i];
                }
            }
            throw new System.Exception("Error on percentages calculation");
        }

        public Reward[] FisherYates(Reward[] _rewards)
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