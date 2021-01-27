using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;
using Rewards;
using UnityEngine.UI;
using Caps;
using TMPro;
using Player;
using UnityEngine.EventSystems;
using Sound;



namespace Islands
{
    public enum IslandDifficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
    public enum IslandType
    {
        Common,
        Shop,
        Keystone,
        Start,
        Boss
    }


    public class Island : MonoBehaviour
    {
        #region Variables

        [Header("Required")]
        public IslandDifficulty difficulty;
        public IslandType type;
        [HideInInspector] public Reward reward;

        public Island[] accessibleNeighbours;
        public List<SpriteShapeRenderer> traiList = new List<SpriteShapeRenderer>();

        [HideInInspector] public List<Cap> capList = new List<Cap>();
        [HideInInspector] public bool isDone;

        [Header("Special Islands")]
        [SerializeField] Sprite legendaryIslandSprite;
        [SerializeField] Sprite startIslandSprite;
        [SerializeField] Sprite shopIslandSprite;
        [SerializeField] Sprite bossIslandSprite;

        //Components
        Image image;
        public Button button;
        [HideInInspector] public EventTrigger eventTrigger;

        [Header("References")]
        public RectTransform anchorPoint;
        [SerializeField] float anchorRange = 64;

        [Header("UI")]
        public GameObject islandDescriptionContainer;
        public Image islandRewardImage;
        public TextMeshProUGUI rewardDescription;
        public TextMeshProUGUI capLength;
        private AudioSource audioSource;
        public Image icon;
        public IslandIcone_Showcase iconTransition;
        public Image redCross;

        #endregion

        #region Unity Messages

        private void Awake()
        {
            image = GetComponent<Image>();
            eventTrigger = GetComponent<EventTrigger>();

            audioSource = GetComponent<AudioSource>();

            button = GetComponent<Button>();
        }



        private void Start()
        {
            //set up value from debug
            anchorRange = DebugToolManager.Instance.ChangeVariableValue("anchorRange");

            //Set button event click & select
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((data) => { OnSelect(); });

            button.onClick.AddListener(OnClick);
            eventTrigger.triggers.Add(entry);

            if(type == IslandType.Keystone)
            {
                reward = IslandCreator.Instance.keystone;
            }
        }

        private void Update()
        {
            UpdateTrails();
        }

        #endregion

        #region Private Methods

        void UpdateTrails()
        {
            if (traiList.Count > 0)
            {
                if (PlayerMovement.Instance.playerIsland == this)
                {
                    foreach (Island island in IslandCreator.Instance.islands)
                    {
                        island.CleanTrails();
                    }

                    foreach (SpriteShapeRenderer renderer in traiList)
                    {
                        renderer.materials[1].SetInt("bool_Available", 1);
                    }

                    if (Manager.Instance.eventSystem != null && Manager.Instance.eventSystem.enabled)
                    {

                        Island targetIsland = IslandCreator.Instance.eventSystem.currentSelectedGameObject.GetComponent<Island>();
                        for (int i = 0; i < accessibleNeighbours.Length; i++)
                        {
                            if (accessibleNeighbours[i] == targetIsland)
                            {
                                traiList[i].materials[1].SetInt("bool_Selected", 1);
                            }
                        }
                    }
                }
            }
        }

        public void CleanTrails()
        {
            if (traiList.Count > 0)
            {
                foreach (SpriteShapeRenderer renderer in traiList)
                {
                    renderer.materials[1].SetInt("bool_Available", 0);
                    renderer.materials[1].SetInt("bool_Selected", 0);
                }
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

            SetAnchor(_sprite);
        }

        public void SetSprite(IslandSprite _sprite)
        {
            difficulty = IslandDifficulty.Hard;
            image.sprite = _sprite.sprite;
            SetAnchor(_sprite);
        }

        void SetAnchor(IslandSprite _sprite)
        {
            switch (_sprite.anchorPoint)
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

        public void OnClick()
        {
            if(this != PlayerMovement.Instance.playerIsland)
            {
                SoundManager.Instance.ApplyAudioClip("StartCap", audioSource);
                Manager.Instance.eventSystem.enabled = false;
                islandDescriptionContainer.SetActive(false);
                PlayerMovement.Instance.Move(this);
                audioSource.PlaySecured();
            }
            else
            {
                if (PlayerMovement.Instance.playerIsland.type != IslandType.Shop)
                {
                    SoundManager.Instance.ApplyAudioClip("ClickedImpossible", audioSource);
                    audioSource.PlaySecured();          
                }
                else
                {
                    PlayerMovement.Instance.Move(this);
                }
            }          
        }
        public void OnSelect()
        {

            if (!PlayerMovement.Instance.isMoving )
            {
                SoundManager.Instance.ApplyAudioClip("Selected", audioSource);
                audioSource.PlaySecured();
                PlayerMovement.Instance.ShowSelectedIslandInfo(this);
                PlayerInventory.Instance.fromInventory = false;
            }

        }

        #endregion


    }
}