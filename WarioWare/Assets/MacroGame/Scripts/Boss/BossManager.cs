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
        public int damageToPlayer = 10;
        public int damageToBoss = 10;
        private int bossLifeOnStartOfFight;
        private int phaseBossLife;
        private int phaseNumber = 1;
        public Malediction[] maledictionArray;
        private Malediction currentMalediction;
        public RenderTexture bossTexture;
        public int differentMiniGameNumber =4;
        public bool isFinalBoss;


        private void Awake()
        {
            CreateSingleton();
        }
        private void Start()
        {
            //set up value from debug
            damageToPlayer = DebugToolManager.Instance.ChangeVariableValue("damageToPlayer");
            damageToBoss = DebugToolManager.Instance.ChangeVariableValue("damageToBoss");
            differentMiniGameNumber = DebugToolManager.Instance.ChangeVariableValue("differentMiniGameNumber");
        }
        public IEnumerator StartBoss( )
        {
            if(maledictionArray!= null && maledictionArray.Length !=0)
            currentMalediction = maledictionArray[Random.Range(0,maledictionArray.Length)];
            bossLifeOnStartOfFight = BossLifeManager.currentLife;
            renderText.texture = bossTexture;
            shipOpening.gameObject.SetActive(true);
            StartCoroutine( Manager.Instance.ZoomCam(shipOpening.openingTime));
            yield return new WaitForSeconds(shipOpening.openingTime * 2);
            StartCoroutine(Manager.Instance.PlayMiniGame(transitionCam, currentMalediction));
        }
        public IEnumerator TransitionBoss(bool win)
        {
            transitionCam.enabled = true;

            if (win)
            {
                transition.PlayAnimation((float)Manager.Instance.bpm, true);
                SoundManager.Instance.ApplyAudioClip("victoryJingle", transitionMusic, Manager.Instance.bpm);
                BossLifeManager.Instance.TakeDamage(damageToBoss,bossLifeOnStartOfFight, true);
                phaseBossLife += damageToBoss;
            }
            else
            {
                PlayerManager.Instance.TakeDamage(damageToPlayer);
                transition.PlayAnimation((float)Manager.Instance.bpm, false);
                SoundManager.Instance.ApplyAudioClip("loseJingle", transitionMusic, Manager.Instance.bpm);

            }

            if (PlayerManager.Instance.playerHp > 0)
            {
                transitionMusic.PlaySecured();
                yield return new WaitForSeconds(transitionMusic.clip.length);
            }

            if(phaseBossLife>= bossLifeOnStartOfFight *phaseNumber/ 4)
            {
                phaseNumber++;
                if (phaseNumber == 5)
                {
                    int _sceneIndex = Manager.Instance.macroSceneIndex;
                    if(SceneManager.GetSceneByBuildIndex(_sceneIndex).name == "Zone1")
                    {
                        SceneManager.LoadScene("Zone2");
                    }
                   else if (SceneManager.GetSceneByBuildIndex(_sceneIndex).name == "Zone2")
                    {
                        SceneManager.LoadScene("Zone3");
                    }
                   else if (SceneManager.GetSceneByBuildIndex(_sceneIndex).name == "Zone3")
                    {
                        SceneManager.LoadScene("Menu");
                    }
                    yield break;
                }
                SoundManager.Instance.ApplyAudioClip("speedUpJingle", transitionMusic, Manager.Instance.bpm);
                transitionMusic.PlaySecured();
                yield return new WaitForSeconds(transitionMusic.clip.length);

                Manager.Instance.bpm = Manager.Instance.bpm.Next();
                currentMalediction = maledictionArray[phaseNumber-1];
            }
            if (isFinalBoss)
            {
                currentMalediction.StopMalediction();
                var _malediction = maledictionArray[Random.Range(0, maledictionArray.Length)];
               while (_malediction == currentMalediction)
                {
                    _malediction = maledictionArray[Random.Range(0, maledictionArray.Length)];
                }
                currentMalediction = _malediction;
            }
            else
            {
                currentMalediction = null;
            }

            
            Manager.Instance.GlobalTransitionEnd(currentMalediction);
            transitionCam.enabled = false;
        }
    }

}
