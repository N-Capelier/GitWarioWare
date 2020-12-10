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
        DisplaySoundBPM();
        DisplaySound();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(soundList);
        Repaint();

    }
    private void DisplaySoundBPM()
    {
        var _rect = EditorGUILayout.BeginVertical();
        GUI.Box(_rect, "Sound with bpm");
        EditorGUILayout.Space(20);
        foreach (var soundsBpm in soundList.soundBpms)
        {
            bpm = soundsBpm.bpm.ToArray();
            soundsBpm.name = EditorGUILayout.TextField(soundsBpm.name);
            for (int i = 0; i < soundsBpm.sounds.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(30);
                bpm[i] = (BPM)EditorGUILayout.EnumPopup(bpm[i]);
                soundsBpm.sounds[i].name = soundsBpm.name + "_" + i;
                EditorGUILayout.LabelField(soundsBpm.sounds[i].name,GUILayout.MaxWidth(150) );
                soundsBpm.sounds[i].clip = (AudioClip)EditorGUILayout.ObjectField(soundsBpm.sounds[i].clip, typeof(AudioClip), true);
                soundsBpm.bpm[i] = bpm[i];
                soundsBpm.sounds[i].volume = EditorGUILayout.Slider(soundsBpm.sounds[i].volume, 0, 1);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+")) { soundList.soundBpms.Add(new SoundBpm()); }
        if (GUILayout.Button("-")) { soundList.soundBpms.RemoveAt(soundList.soundBpms.Count - 1); }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    private void DisplaySound()
    {
        var _rect = EditorGUILayout.BeginVertical();
        GUI.Box(_rect, "Sound without bpm");
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
}
