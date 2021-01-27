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
        public IEnumerator StartBoss(CapsSorter sorter, Cap currentCap)
        {
            currentCap.ChoseMiniGames(sorter.bossList, differentMiniGameNumber);
            bossLifeOnStartOfFight = BossLifeManager.currentLife;
            renderText.texture = bossTexture;
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
                SoundManager.Instance.ApplyAudioClip("victoryJingleBoss", transitionMusic, Manager.Instance.bpm);

                int _damageToBoss =Mathf.RoundToInt( damageToBoss / Mathf.Pow(damageMultiplier, (float) PlayerManager.Instance.keyStoneNumber));
                BossLifeManager.Instance.TakeDamage(_damageToBoss, bossLifeOnStartOfFight, true);
                phaseBossLife += _damageToBoss;
                transitionMusic.PlaySecured();
                yield return new WaitForSeconds(transitionMusic.clip.length);
            }
            else
            {
                int _damageToPlayer = Mathf.RoundToInt(damageToBoss * Mathf.Pow(damageMultiplier, (float)PlayerManager.Instance.keyStoneNumber));

                PlayerManager.Instance.TakeDamage(_damageToPlayer, true);
                transition.PlayAnimation((float)Manager.Instance.bpm, false);

                if (PlayerManager.Instance.playerHp > 0)
                {
                    SoundManager.Instance.ApplyAudioClip("loseJingleBoss", transitionMusic, Manager.Instance.bpm);
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

                SoundManager.Instance.ApplyAudioClip("speedUpJingleBoss", transitionMusic, Manager.Instance.bpm);
                transitionMusic.PlaySecured();
                transition.SpeedUp((float)Manager.Instance.bpm);
                if(phaseNumber == 5)
                {
                    Manager.Instance.speedUp.SetActive(true);
                    Manager.Instance.speedUp.GetComponentInChildren<TextMeshProUGUI>().text = "Victory!!!";
                }
                yield return new WaitForSeconds(transitionMusic.clip.length);
                if (phaseNumber == 5)
                {
                    int _sceneIndex = Manager.Instance.macroSceneIndex;
                    if (SceneManager.GetSceneByBuildIndex(_sceneIndex).name == "Zone1")
                    {
                        SceneManager.LoadScene("Menu");
                    }
                    
                }
                Manager.Instance.bpm = Manager.Instance.bpm.Next();
               
            }


            Manager.Instance.GlobalTransitionEnd(true);
            transitionCam.enabled = false;
        }
    }

}
