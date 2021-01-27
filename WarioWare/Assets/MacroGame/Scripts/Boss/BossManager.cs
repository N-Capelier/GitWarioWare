using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Cinemachine;
using UnityEngine.UI;
using Caps;
using Sound;
using Player;
using SD_UsualAction;
using UnityEngine.SceneManagement;
using TMPro;
using Islands;
using Rewards;

namespace Boss
{
    public class BossManager : Singleton<BossManager>
    {
        [Header("Transition")]
        public TransitionAnimations transition;
        public Camera transitionCam;
        public AudioSource transitionMusic;
        private bool cantDoTransition;
        private Transform initalCamTransform;
        public Visual_IslandDescriptionOpening shipOpening;
        public RawImage renderText;
        public GameObject VcamTarget;
        public CinemachineVirtualCamera cinemachine;
        private IslandType currentType;
        public TextMeshProUGUI keyStoneNumber;

        [Header("Boss parameters")]
        public float damageToPlayer = 10;
        public float damageToBoss = 10;
        private int bossLifeOnStartOfFight;
        private int phaseBossLife;
        private int phaseNumber = 1;
        public RenderTexture bossTexture;
        public int differentMiniGameNumber = 4;
        public bool isFinalBoss;
        public float damageMultiplier = 1.2f;
        private void Awake()
        {
            CreateSingleton();
        }
        private void Start()
        {
            //set up value from debug
            damageToPlayer = (float)DebugToolManager.Instance.ChangeVariableValue("damageToPlayer");
            damageToBoss = (float)DebugToolManager.Instance.ChangeVariableValue("damageToBoss");
            differentMiniGameNumber = (int)DebugToolManager.Instance.ChangeVariableValue("differentMiniGameNumber");
            damageMultiplier = DebugToolManager.Instance.ChangeVariableValue("damageMultiplier");
            
            
        }
        public IEnumerator StartBoss(CapsSorter sorter, Cap currentCap, IslandType islandType)
        {
            keyStoneNumber.text = KeystoneReward.keystoneCount.ToString();
            currentType = islandType;
            currentCap.ChoseMiniGames(sorter.bossList, differentMiniGameNumber);
            renderText.texture = bossTexture;
            if(islandType == IslandType.Boss)
            {
                bossLifeOnStartOfFight = BossLifeManager.currentLife;
                BossLifeManager.Instance.InitialLife();

            }
            else
            {
                bossLifeOnStartOfFight = 150;
                BossLifeManager.Instance.InitialLife(bossLifeOnStartOfFight);

            }
            shipOpening.gameObject.SetActive(true);
            StartCoroutine(Manager.Instance.ZoomCam(shipOpening.openingTime));
            yield return new WaitForSeconds(shipOpening.openingTime * 2);
            StartCoroutine(Manager.Instance.PlayMiniGame(transitionCam,true));
        }
        public IEnumerator TransitionBoss(bool win)
        {
            transitionCam.enabled = true;
            
            if (win)
            {
                transition.PlayAnimation((float)Manager.Instance.bpm, true);
                if(currentType == IslandType.Boss)
                    SoundManager.Instance.ApplyAudioClip("victoryJingleBoss", transitionMusic, Manager.Instance.bpm);
                else
                    SoundManager.Instance.ApplyAudioClip("victoryJingleMiniBoss", transitionMusic, Manager.Instance.bpm);

                int _damageToBoss =Mathf.RoundToInt( damageToBoss / Mathf.Pow(damageMultiplier, 5- (float) KeystoneReward.keystoneCount));
                if (currentType == IslandType.Keystone)
                {
                    _damageToBoss = (int)damageToBoss;
                    BossLifeManager.Instance.TakeDamage(_damageToBoss, bossLifeOnStartOfFight, true, true);
                }
                else
                {
                    BossLifeManager.Instance.TakeDamage(_damageToBoss, bossLifeOnStartOfFight, true);
                }           
                
                phaseBossLife += _damageToBoss;
                transitionMusic.PlaySecured();
                yield return new WaitForSeconds(transitionMusic.clip.length);
            }
            else
            {
                int _damageToPlayer = Mathf.RoundToInt(damageToPlayer * Mathf.Pow(damageMultiplier,5- (float)KeystoneReward.keystoneCount));
                if(currentType != IslandType.Boss)
                {
                    _damageToPlayer = (int)damageToBoss;
                }
                PlayerManager.Instance.TakeDamage(_damageToPlayer, true, currentType == IslandType.Keystone);
                transition.PlayAnimation((float)Manager.Instance.bpm, false);

                if (PlayerManager.Instance.playerHp > 0)
                {
                    if (currentType == IslandType.Boss)
                        SoundManager.Instance.ApplyAudioClip("loseJingleBoss", transitionMusic, Manager.Instance.bpm);
                   else
                        SoundManager.Instance.ApplyAudioClip("loseJingleMiniBoss", transitionMusic, Manager.Instance.bpm);
                        transitionMusic.PlaySecured();
                    yield return new WaitForSeconds(transitionMusic.clip.length);
                }
                else
                {
                    yield break;

                }
            }

          
            if (phaseBossLife >= bossLifeOnStartOfFight * phaseNumber / 4)
            {
                phaseNumber++;
                if(phaseNumber != 5)
                {
                    if (currentType == IslandType.Boss)
                        SoundManager.Instance.ApplyAudioClip("speedUpJingleBoss", transitionMusic, Manager.Instance.bpm);
                    else
                        SoundManager.Instance.ApplyAudioClip("speedUpJingleMiniBoss", transitionMusic, Manager.Instance.bpm);
                    transitionMusic.PlaySecured();
                    Manager.Instance.speedUp.SetActive(true);
                    transition.SpeedUp((float)Manager.Instance.bpm);

                }
                else
                {
                    Manager.Instance.victory.SetActive(true);
                }
                yield return new WaitForSeconds(transitionMusic.clip.length);
                if (phaseNumber == 5)
                {
                    int _sceneIndex = Manager.Instance.macroSceneIndex;
                    if(currentType == IslandType.Boss)
                    {
                        if (SceneManager.GetSceneByBuildIndex(_sceneIndex).name == "WorldMap")
                        {
                            SceneManager.LoadScene("Menu");
                        }
                    }
                    else
                    {
                        StartCoroutine(Manager.Instance.CapEnd());
                        Manager.Instance.victory.SetActive(false);
                        transitionCam.enabled = false;

                        yield break;
                    }
                    
                }
                Manager.Instance.bpm = Manager.Instance.bpm.Next();
               
            }


            Manager.Instance.GlobalTransitionEnd ( transitionCam, true);
            
        }
    }

}
