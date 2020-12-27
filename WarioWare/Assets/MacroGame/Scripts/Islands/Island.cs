using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;
using Rewards;
using UnityEngine.UI;
using Caps;
using TMPro;

namespace Islands
{
    public enum IslandDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
        Legendary = 3,
        Shop = 4,
        Start = 5,
        Boss
    }
    public class Island : MonoBehaviour
    {
        #region Variables

        [Header("Required")]
        public IslandDifficulty difficulty;
        [HideInInspector] public Reward reward;

        public Island[] accessibleNeighbours;
        public List<SpriteShapeRenderer> traiList = new List<SpriteShapeRenderer>();

        [HideInInspector] public List<Cap> capList = new List<Cap>();

        [Header("Special Islands")]
        [SerializeField] Sprite legendaryIslandSprite;
        [SerializeField] Sprite startIslandSprite;
        [SerializeField] Sprite shopIslandSprite;
        [SerializeField] Sprite bossIslandSprite;

        //Components
        Image image;
        [HideInInspector] public Button button;

        [Header("References")]
        public RectTransform anchorPoint;
        [SerializeField] float anchorRange = 64;

        [Header("UI")]
        public GameObject islandDescriptionContainer;
        public Image islandRewardImage;
        public TextMeshProUGUI rewardDescription;
        public TextMeshProUGUI capLength;

        #endregion

        #region Unity Messages

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
        }

        private void Start()
        {
            if (traiList.Count > 0)
            {
                for (int i = 0; i < traiList.Count; i++)
                {
                    //materials[1] pour le edge material et non le fill material
                    traiList[i].materials[1].SetInt("bool_Available", 0);
                    traiList[i].materials[1].SetInt("bool_Selected", 0);
                }
            }

            switch (difficulty)
            {
                case IslandDifficulty.Legendary:
                    image.sprite = legendaryIslandSprite;
                    break;
                case IslandDifficulty.Start:
                    image.sprite = startIslandSprite;
                    break;
                case IslandDifficulty.Shop:
                    image.sprite = shopIslandSprite;
                    break;
                case IslandDifficulty.Boss:
                    image.sprite = bossIslandSprite;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init the island with this method.
        /// </summary>
        /// <param name="_reward"></param>
        public void SetReward(Reward _reward, IslandSprite _sprite)
        {
            reward = _reward;
            switch (_reward.rarity)
            {
                case RewardRarity.Common:
                    difficulty = IslandDifficulty.Easy;
                    break;
                case RewardRarity.Rare:
                    difficulty = IslandDifficulty.Medium;
                    break;
                case RewardRarity.Epic:
                    difficulty = IslandDifficulty.Hard;
                    break;
                default:
                    throw new System.Exception("Rarity not linked to difficulty !");
            }

            image.sprite = _sprite.sprite;

            switch(_sprite.anchorPoint)
            {
                case IslandAnchorPoint.East:
                    anchorPoint.localPosition = new Vector2(anchorRange, 0);
                    break;
                case IslandAnchorPoint.South_East:
                    anchorPoint.localPosition = new Vector2(anchorRange, -anchorRange);
                    break;
                case IslandAnchorPoint.South:
                    anchorPoint.localPosition = new Vector2(0, -anchorRange);
                    break;
                case IslandAnchorPoint.South_West:
                    anchorPoint.localPosition = new Vector2(-anchorRange, -anchorRange);
                    break;
                case IslandAnchorPoint.West:
                    anchorPoint.localPosition = new Vector2(-anchorRange, 0);
                    break;
                case IslandAnchorPoint.North_West:
                    anchorPoint.localPosition = new Vector2(-anchorRange, anchorRange);
                    break;
                case IslandAnchorPoint.North:
                    anchorPoint.localPosition = new Vector2(0, -anchorRange);
                    break;
                case IslandAnchorPoint.North_East:
                    anchorPoint.localPosition = new Vector2(anchorRange, anchorRange);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}