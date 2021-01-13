using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using UnityEngine.Audio;


namespace Soupe
{
    namespace DeterrageTresor
    {
        public class Script_Deterragetresor : TimedBehaviour
        {

            public Transform LeftShovel;    //Declatarion des pelles
            public Transform TopShovel;
            public Transform RightShovel;
            public Transform BottomShovel;

            public GameObject[] InputSigns; //indication des inputs sur lesquelles appuier

            public ParticleSystem[] SandFX;

            Transform Chest;         //Declaration du coffre
            public GameObject RustyChest;
            public GameObject IronChest;
            public GameObject GoldenChest;

            public GameObject Halo;

            public GameObject WarioMusic;
            public GameObject SoundMaker;
            public AudioClip[] musics;
            
            public AudioClip[] crounch;
            public AudioClip Win;

            bool gameEnd;
            bool gameWin;

            public AudioMixer Mixer;


            public int difficulty;          //Variable temporaire qui va gérer la difficulte

            string[] inputCurrent;          //Tableau dans lequel est renseinge les inputs associés aux différentes pelles (variables assignées dans SetDifficulty)
            int inputToPush;                //Variable associee au numero de la pelle avec laquelle le joueur doit interagir

            int currentInputNumber;         //Compte le nombre de fois que le joueur a creuse
            public int inputNumberToReach;  //Nombre de fois que le joueur doir creuser pour reussir

            float time;                             //Variables pour la gestion de l'appartition de la signalisation d'inputs
            public float timeBeforeInputAppears;
            bool mustAppear;

            public float yMaxChestPos = 1.50f;  //Position finale du coffre
            float yBaseChestPos;

            /// <summary>
            /// Alex LEPINE
            /// </summary>
            /// 
            public override void Start()
            {
                base.Start(); //Do not erase this line!

                inputToPush = 0;    //Le joueur devra appuier sur la pelle d'index 0

                InputSigns[inputToPush].SetActive(true);    //Active la signalisation de l'input sur lequel le joueur doit appuier


                SetMusic();
                SetDifficulty();    //Set la difficule du minijeu
            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {
                if (Tick == 8)
                {
                    if (gameWin)
                    {
                        Manager.Instance.Result(true);
                    }
                    else
                    {
                        Manager.Instance.Result(false);
                    }
                    gameEnd = true;
                }

            }

            private void Update()
            {
                //Check si le joueur appuie sur le bon Input

                if ((Input.GetButtonDown("A_Button") || Input.GetKeyDown(KeyCode.DownArrow)) && inputCurrent[inputToPush] == "A" && !gameEnd)   //
                {
                    StartCoroutine(BottomShovelAnim()); //Lance l'animation de la pelle associée à l'input A
                    NextInput();    //Le joueur devra appuier sur la pelle suivante
                }
                if ((Input.GetButtonDown("B_Button") || Input.GetKeyDown(KeyCode.RightArrow)) && inputCurrent[inputToPush] == "B" && !gameEnd)
                {
                    StartCoroutine(RightShovelAnim());
                    NextInput();
                }
                if ((Input.GetButtonDown("X_Button") || Input.GetKeyDown(KeyCode.LeftArrow)) && inputCurrent[inputToPush] == "X" && !gameEnd)
                {
                    StartCoroutine(LeftShovelAnim());
                    NextInput();
                }
                if ((Input.GetButtonDown("Y_Button") || Input.GetKeyDown(KeyCode.UpArrow)) && inputCurrent[inputToPush] == "Y" && !gameEnd)
                {
                    StartCoroutine(TopShovelAnim());
                    NextInput();
                }

                if (mustAppear)
                {
                    
                    time += Time.deltaTime;

                    if (time >= timeBeforeInputAppears)
                    {
                        mustAppear = false;
                        time = 0f;
                        InputSigns[inputToPush].SetActive(true);    //Active la signalisation de l'input sur lequel le joueur doit appuier
                    }
                }

                if (gameEnd && (mustAppear || InputSigns[inputToPush].activeSelf))
                {
                    mustAppear = false;
                    InputSigns[inputToPush].SetActive(false);
                }
            }

            void NextInput()
            {
                

                mustAppear = false; //Stop l'apparition de l'input
                time = 0f;

                InputSigns[inputToPush].SetActive(false); // désactive la signalisation de l'input sur lequel le joueur vient d'appuier

                if (inputToPush == inputCurrent.Length - 1) //Si le joueur a fait un tour de pelle
                {
                    inputToPush = 0;    //Le joueur devra appuier sur la pelle d'index 0
                }
                else
                {
                    inputToPush += 1;   //Le joueur devra appuier sur la prochaine pelle
                }

                mustAppear = true;


                AudioSource source = SoundMaker.AddComponent<AudioSource>();
                source.clip = crounch[Random.Range(0, crounch.Length - 1)];
                source.outputAudioMixerGroup = Mixer.outputAudioMixerGroup;
                source.Play();

                if(Chest.position.y < yMaxChestPos && !gameEnd)
                    Chest.DOMoveY((Chest.position.y + ((yMaxChestPos - yBaseChestPos) / (float)inputNumberToReach)), 0.1f); //Fait monter le coffre

                currentInputNumber += 1;    //Le joueur a appuié une fois de plus


                if (currentInputNumber == inputNumberToReach)   //Check la victoire
                {
                    gameEnd = true;
                    gameWin = true;
                    StartCoroutine(WinAnim());
                    
                }   
            }

            void SetDifficulty()
            {
                if (difficulty == 1)    //Il n'y a que deux pelles
                {
                    TopShovel.gameObject.SetActive(false);
                    LeftShovel.gameObject.SetActive(false);

                    inputCurrent = new string[2] { "B", "A" };

                    Chest = RustyChest.transform;
                    RustyChest.SetActive(true);
                }
                else if (difficulty == 2)   //trois pelles
                {
                    TopShovel.gameObject.SetActive(false);

                    inputCurrent = new string[3] { "B", "A", "X" };

                    Chest = IronChest.transform;
                    IronChest.SetActive(true);
                }
                else if (difficulty == 3)   //Quatre pelles
                {
                    inputCurrent = new string[4] { "B", "A", "X", "Y"};

                    Chest = GoldenChest.transform;
                    GoldenChest.SetActive(true);
                }

                


                switch(currentDifficulty)
                {
                    case Difficulty.EASY:

                        TopShovel.gameObject.SetActive(false);
                        LeftShovel.gameObject.SetActive(false);

                        inputCurrent = new string[2] { "B", "A" };

                        Chest = RustyChest.transform;
                        RustyChest.SetActive(true);

                        break;

                    case Difficulty.MEDIUM:

                        TopShovel.gameObject.SetActive(false);

                        inputCurrent = new string[3] { "B", "A", "X" };

                        Chest = IronChest.transform;
                        IronChest.SetActive(true);

                        break;

                    case Difficulty.HARD:

                        inputCurrent = new string[4] { "B", "A", "X", "Y" };

                        Chest = GoldenChest.transform;
                        GoldenChest.SetActive(true);

                        break;

                    default:
                        Debug.LogError("Difficulty not set");
                        break;
                }
                yBaseChestPos = Chest.position.y;
            }

            void SetMusic()
            {
                if (bpm == 60)
                {
                    WarioMusic.GetComponent<AudioSource>().clip = musics[0];
                }
                else if (bpm == 90)
                {
                    WarioMusic.GetComponent<AudioSource>().clip = musics[1];
                }
                else if (bpm == 120)
                {
                    WarioMusic.GetComponent<AudioSource>().clip = musics[2];
                }
                else if (bpm == 140)
                {
                    WarioMusic.GetComponent<AudioSource>().clip = musics[3];
                }
                else
                {
                    Debug.LogWarning("BPM not reconized : no music");
                }
                WarioMusic.GetComponent<AudioSource>().Play();
            }

            //Fonctions d'animation des pelles
            IEnumerator BottomShovelAnim()
            {
                BottomShovel.DOMoveY(-5f, 0.2f);
                BottomShovel.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.1f);
                yield return new WaitForSeconds(0.2f);
                SandFX[1].Play(); //joue le fx associé à la pelle
                BottomShovel.DOMoveY(-6.98f, 0.2f);
                BottomShovel.DOScale(new Vector3(1, 1, 1), 0.1f);
            }
            IEnumerator TopShovelAnim()
            {
                
                TopShovel.DOMoveY(3f, 0.2f);
                TopShovel.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.1f);
                yield return new WaitForSeconds(0.2f);
                SandFX[3].Play(); //joue le fx associé à la pelle
                TopShovel.DOMoveY(6.56f, 0.2f);
                TopShovel.DOScale(new Vector3(1, 1, 1), 0.1f);
            }
            IEnumerator RightShovelAnim()
            {
                RightShovel.DORotate(new Vector3(0f, 0f, 200f), 0.1f);
                RightShovel.DOMoveX(8f, 0.2f);
                RightShovel.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
                yield return new WaitForSeconds(0.1f);
                SandFX[0].Play(); //joue le fx associé à la pelle
                RightShovel.DORotate(new Vector3(0f, 0f, 170f), 0.1f);
                yield return new WaitForSeconds(0.1f);
                RightShovel.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
                RightShovel.DORotate(new Vector3(0, 0, 180), 0.1f);
                RightShovel.DOMoveX(10.22f, 0.2f);
            }
            IEnumerator LeftShovelAnim()
            {
                LeftShovel.DORotate(new Vector3(0f, 0f, -20f), 0.1f);
                LeftShovel.DOMoveX(-8f, 0.2f);
                LeftShovel.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f);
                yield return new WaitForSeconds(0.1f);
                SandFX[2].Play(); //joue le fx associé à la pelle
                LeftShovel.DORotate(new Vector3(0f, 0f, 10f), 0.1f);
                yield return new WaitForSeconds(0.1f);
                LeftShovel.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
                LeftShovel.DORotate(new Vector3(0, 0, 0), 0.1f);
                LeftShovel.DOMoveX(-10.24f, 0.2f);
            }

            IEnumerator WinAnim()
            {

                WarioMusic.GetComponent<AudioSource>().DOFade(0f, 0.5f);

                AudioSource source = SoundMaker.AddComponent<AudioSource>();
                source.clip = Win;
                source.outputAudioMixerGroup = Mixer.outputAudioMixerGroup;
                source.Play();

                Chest.DOMoveY(0f, 0.2f);
                Chest.DORotate(new Vector3(0f, 0f, 360f), 0.2f, RotateMode.FastBeyond360);
                Chest.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
                Chest.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 9;
                yield return new WaitForSeconds(0.2f);
                Halo.SetActive(true);
                Halo.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
                Halo.transform.DORotate(new Vector3(0, 0, 720f), 8f, RotateMode.FastBeyond360);

            }
        }
    }
}