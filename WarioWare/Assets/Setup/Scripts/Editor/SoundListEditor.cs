using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Caps;
[CustomEditor(typeof(SoundList))]
public class SoundListEditor : Editor
{

    private SoundList soundList;
    private BPM[] bpm;
    private Vector2 scrollPosition;
    private bool bpmFoldout;
    private bool classicFoldout;
    private bool musicFoldout;
    private void OnEnable()
    {
        soundList = target as SoundList;
    }



    private void OnGUI()
    {

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var _rect = EditorGUILayout.BeginHorizontal();
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        DisplaySoundBPM();
        DisplaySound();
        DisplayMusic();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(soundList);
        Repaint();

    }
    private void DisplaySoundBPM()
    {
        bpmFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(bpmFoldout, new GUIContent("Music"));

        if (bpmFoldout)
        {
            var _rect = EditorGUILayout.BeginVertical();
                GUI.Box(_rect, "");
                EditorGUILayout.Space(20);
                foreach (var soundsBpm in soundList.soundBpms)
                {
                    soundsBpm.foldout = EditorGUILayout.BeginFoldoutHeaderGroup(soundsBpm.foldout, soundsBpm.name);
                        if (soundsBpm.foldout)
                        {
                            bpm = soundsBpm.bpm.ToArray();
                            soundsBpm.name = EditorGUILayout.TextField(soundsBpm.name);
                            for (int i = 0; i < soundsBpm.sounds.Count; i++)
                            {
                                EditorGUILayout.BeginHorizontal();
                                    EditorGUILayout.Space(30);
                                    bpm[i] = (BPM)EditorGUILayout.EnumPopup(bpm[i]);
                                    soundsBpm.sounds[i].name = soundsBpm.name + "_" + i;
                                    EditorGUILayout.LabelField(soundsBpm.sounds[i].name, GUILayout.MaxWidth(100));
                                    soundsBpm.sounds[i].clip = (AudioClip)EditorGUILayout.ObjectField(soundsBpm.sounds[i].clip, typeof(AudioClip), true, GUILayout.MinWidth(200));
                                    soundsBpm.bpm[i] = bpm[i];
                                    soundsBpm.sounds[i].volume = EditorGUILayout.Slider(soundsBpm.sounds[i].volume, 0, 1, GUILayout.MinWidth(150));
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                }
                EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("+")) { soundList.soundBpms.Add(new SoundBpm()); }
                    if (GUILayout.Button("-")) { soundList.soundBpms.RemoveAt(soundList.soundBpms.Count - 1); }
                EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    private void DisplaySound()
    {
        classicFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(classicFoldout, new GUIContent("Sound without bpm"));

            if (classicFoldout)
            {
                var _rect = EditorGUILayout.BeginVertical();
                    GUI.Box(_rect," ");
                    EditorGUILayout.Space(20);
                    foreach (var soundClassic in soundList.soundClassic)
                    {
                        EditorGUILayout.BeginHorizontal();
                            soundClassic.name = EditorGUILayout.TextField(soundClassic.name);
                            soundClassic.clip = (AudioClip)EditorGUILayout.ObjectField(soundClassic.clip, typeof(AudioClip), true);
                            soundClassic.volume = EditorGUILayout.Slider(soundClassic.volume, 0, 1);
                        EditorGUILayout.EndHorizontal();

                    }
                    EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button("+")) { soundList.soundClassic.Add(new SoundClassic()); }
                        if (GUILayout.Button("-")) { soundList.soundClassic.RemoveAt(soundList.soundClassic.Count - 1); }
                    EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    private void DisplayMusic()
    {
        musicFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(musicFoldout, new GUIContent("Music"));

        if (musicFoldout)
        {
            var _rect = EditorGUILayout.BeginVertical();
            GUI.Box(_rect, " ");
            EditorGUILayout.Space(20);
            foreach (var soundClassic in soundList.music)
            {
                EditorGUILayout.BeginHorizontal();
                soundClassic.name = EditorGUILayout.TextField(soundClassic.name);
                soundClassic.clip = (AudioClip)EditorGUILayout.ObjectField(soundClassic.clip, typeof(AudioClip), true);
                soundClassic.author = EditorGUILayout.TextField(soundClassic.author);
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Actualise music")) { ActualiseMusicList(); }
            if (GUILayout.Button("AddMusic")) { soundList.music.Add(new Music()); } 
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    private void ActualiseMusicList()
    {
        
        foreach (AudioClip soundFile in Resources.LoadAll("Music",typeof(AudioClip) ))
        {
            bool cantAdd = false;
            for (int i = 0; i < soundList.music.Count; i++)
            {
                if(soundList.music[i].clip == soundFile)
                {
                    cantAdd = true;
                    break;
                }
            }
            if (!cantAdd)
            {
                soundList.music.Add(new Music());
                soundList.music[soundList.music.Count - 1].clip = soundFile;
            }
        }
    }
}
