using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caps;
using UnityEngine.UI;
using DG.Tweening;

namespace Fleebos
{
    namespace VielleOfTime
    {
        /// <summary>
        /// Arthur SPELLANI
        /// </summary>

        public class SequenceManager : TimedBehaviour
        {
            public GameObject particleWin;

            public GameObject[] Inputs;
            public GameObject[] NotesUI;
            public List<GameObject> MusicNotes;
            public GameObject solo;

            public Image turnSignal;
            public GameObject winrgParticle;

            public GameObject fire;
            bool isOnFire = false;

            public GameObject[] VielleTouches;
            
            int ListOfNotesLength;
            int Easy = 3;
            int Medium = 4;
            int Hard = 5;

            [HideInInspector] public int NoteOrder = 0;

            VielleManager VL;

            public override void Start()
            {
                base.Start(); //Do not erase this line!

                VL = GetComponent<VielleManager>();

                switch (currentDifficulty)
                {
                    case Difficulty.EASY:
                        ListOfNotesLength = Easy;
                        break;

                    case Difficulty.MEDIUM:
                        ListOfNotesLength = Medium;
                        break;

                    case Difficulty.HARD:
                        ListOfNotesLength = Hard;
                        break;
                }

                for (int i = 0; i < ListOfNotesLength; i++)
                {
                    MusicNotes.Add(Inputs[Random.Range(0, 4)]);
                }

                for (int i = 0; i < MusicNotes.Count; i++)
                {
                    Instantiate(MusicNotes[i], NotesUI[i].transform);
                }

            }

            private void Update()
            {
                if (VL.turnActivation == true && NoteOrder < MusicNotes.Count)
                {
                    if (Input.GetButtonDown("A_Button"))
                    {
                        if (MusicNotes[NoteOrder].name == "A_Button_Sequence")
                        {
                            PlayNote();
                        }
                        else if (MusicNotes[NoteOrder].name == "B_Button_Sequence" || MusicNotes[NoteOrder].name == "X_Button_Sequence" || MusicNotes[NoteOrder].name == "Y_Button_Sequence")
                        {
                            WrongNote();
                        }
                    }
                    if (Input.GetButtonDown("B_Button"))
                    {
                        if (MusicNotes[NoteOrder].name == "B_Button_Sequence")
                        {
                            PlayNote();
                        }
                        else if (MusicNotes[NoteOrder].name == "A_Button_Sequence" || MusicNotes[NoteOrder].name == "X_Button_Sequence" || MusicNotes[NoteOrder].name == "Y_Button_Sequence")
                        {
                            WrongNote();
                        }
                    }
                    if (Input.GetButtonDown("X_Button"))
                    {
                        if (MusicNotes[NoteOrder].name == "X_Button_Sequence")
                        {
                            PlayNote();
                        }
                        else if (MusicNotes[NoteOrder].name == "B_Button_Sequence" || MusicNotes[NoteOrder].name == "A_Button_Sequence" || MusicNotes[NoteOrder].name == "Y_Button_Sequence")
                        {
                            WrongNote();
                        }
                    }
                    if (Input.GetButtonDown("Y_Button"))
                    {
                        if (MusicNotes[NoteOrder].name == "Y_Button_Sequence")
                        {
                            PlayNote();
                        }
                        else if (MusicNotes[NoteOrder].name == "B_Button_Sequence" || MusicNotes[NoteOrder].name == "X_Button_Sequence" || MusicNotes[NoteOrder].name == "A_Button_Sequence")
                        {
                            WrongNote();
                        }
                    }
                }

                if (NoteOrder >= MusicNotes.Count && isOnFire == false)
                {
                    isOnFire = true;
                    StartCoroutine(SoloPop(solo));
                    foreach (GameObject go in NotesUI)
                    {
                        StartCoroutine(NotesVanish(go));                        
                    }
                    foreach (Transform child in fire.transform)
                    {
                        StartCoroutine(FirePop(child.gameObject));
                    }
                }

                else if (VL.turnActivation == true && NoteOrder >= MusicNotes.Count)
                {
                    if (Input.GetButtonDown("A_Button"))
                    {
                        NotesUI[0].GetComponent<AudioSource>().Play();
                        StartCoroutine(AnimateTheNote(0));
                    }
                    if (Input.GetButtonDown("B_Button"))
                    {
                        NotesUI[1].GetComponent<AudioSource>().Play();
                        StartCoroutine(AnimateTheNote(1));
                    }
                    if (Input.GetButtonDown("X_Button"))
                    {
                        NotesUI[2].GetComponent<AudioSource>().Play();
                        StartCoroutine(AnimateTheNote(2));
                    }
                    if (Input.GetButtonDown("Y_Button"))
                    {
                        NotesUI[3].GetComponent<AudioSource>().Play();
                        StartCoroutine(AnimateTheNote(3));
                    }
                    if (Input.GetButtonDown("Right_Bumper"))
                    {
                        NotesUI[4].GetComponent<AudioSource>().Play();
                        StartCoroutine(AnimateTheNote(4));
                    }
                }

            }

            //FixedUpdate is called on a fixed time.
            public override void FixedUpdate()
            {
                base.FixedUpdate(); //Do not erase this line!

                
            }

            //TimedUpdate is called once every tick.
            public override void TimedUpdate()
            {

            }

            public void PlayNote()
            {
                NotesUI[NoteOrder].GetComponent<AudioSource>().Play();
                AnimateNote();
                Instantiate(particleWin, NotesUI[NoteOrder].transform);
                NotesUI[NoteOrder].GetComponentInChildren<Image>().color = Color.black;
                NoteOrder++;
            }

            public void WrongNote()
            {
                turnSignal.fillAmount -= 0.8f;
                Instantiate(winrgParticle, turnSignal.transform);
            }

            public void AnimateNote()
            {
                switch (NoteOrder)
                {
                    case 0:
                        StartCoroutine(AnimateTheNote(0));                    
                        break;

                    case 1:
                        StartCoroutine(AnimateTheNote(1));
                        break;

                    case 2:
                        StartCoroutine(AnimateTheNote(2));
                        break;

                    case 3:
                        StartCoroutine(AnimateTheNote(4));
                        break;

                    case 4:
                        StartCoroutine(AnimateTheNote(3));
                        break;
                }
            }

            IEnumerator AnimateTheNote(int Order)
            {
                VielleTouches[Order].transform.DOMoveY(-2.6f, 0.2f);
                yield return new WaitForSeconds(0.2f);
                VielleTouches[Order].transform.DOMoveY(-2.9f, 0.2f);
                yield return new WaitForSeconds(0.2f);
            }

            IEnumerator NotesVanish(GameObject go)
            {
                if(go.GetComponentInChildren<Image>())
                {
                    Image sr = go.GetComponentInChildren<Image>();

                    for (float i = 1; i > 0; i -= 0.01f)
                    {
                        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
                        yield return new WaitForSeconds(0.01f);
                    }
                    sr.color = new Color(0, 0, 0, 0);
                }
            }

            IEnumerator SoloPop(GameObject go)
            {
                Image im = go.GetComponent<Image>();

                for (float i = 0; i < 1; i += 0.05f)
                {
                    im.color = new Color(im.color.r, im.color.g, im.color.b, i);
                    yield return new WaitForSeconds(0.01f);
                }
            }

            IEnumerator FirePop(GameObject child)
            {
                Material mr = child.GetComponent<MeshRenderer>().materials[0];

                for (float i = 0; i < 1; i += 0.05f)
                {
                    mr.SetFloat(Shader.PropertyToID("_Magic"), i);
                    yield return new WaitForSeconds(0.01f);
                }               
            }
        }
    }
}