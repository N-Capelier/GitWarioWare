﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DebugingTool))]
public class DebugingToolWIndowEditor : Editor {

    private bool capsManagerFoldout;
    private bool playerManagerFoldout;
    private bool playerMovementFoldout;
    private bool islandCreatorFoldout;
    private bool islandFoldout;
    private bool bossManagerFoldout;
    private DebugingTool debugingTool;
    private void OnEnable()
    {
        debugingTool = target as DebugingTool;
    }



    public override void OnInspectorGUI()
    {
        for (int i = 0; i < debugingTool.names.Count; i++)
        {
            if (i  ==0)
            {
                capsManagerFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(capsManagerFoldout, "Cap Manager");               

            }
            else if (i == 10)
            {
                if(capsManagerFoldout)
                    EditorGUILayout.Space(20);
                playerManagerFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(playerManagerFoldout, "Player Manager");
            }
            else if(i==14)
            {
                if(playerManagerFoldout)
                    EditorGUILayout.Space(20);
                playerMovementFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(playerMovementFoldout, "Player Movement");
            }
            else if (i == 16)
            {
                if(playerMovementFoldout)
                    EditorGUILayout.Space(20);
                islandCreatorFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(islandCreatorFoldout, "Island Creator");
            }
            else if (i == 21)
            {
                if (islandCreatorFoldout)
                    EditorGUILayout.Space(20);
                islandFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(islandFoldout, "Island");
            }
            else if( i == 22)
            {
                if(islandFoldout)
                EditorGUILayout.Space(20);
                bossManagerFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(bossManagerFoldout, "Boss Manager");
            }

            if (capsManagerFoldout && i<10||
                playerManagerFoldout && i>=10 && i<14||
                playerMovementFoldout && i >= 14 && i < 16 ||
                islandCreatorFoldout && i >= 16 && i < 21 ||
                islandFoldout && i >= 21 && i < 22 || 
                bossManagerFoldout && i>=22)
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(debugingTool.names[i]);

                    if(debugingTool.values[i] <10)
                        debugingTool.values[i] = EditorGUILayout.IntSlider( debugingTool.values[i],0,10);

                    else if(debugingTool.values[i] < 100 && debugingTool.values[i] >=10)
                        debugingTool.values[i] = EditorGUILayout.IntSlider(debugingTool.values[i], 0, 100);

                    else
                        debugingTool.values[i] = EditorGUILayout.IntSlider(debugingTool.values[i], 0, 1000);
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndFoldoutHeaderGroup();

        }

        EditorGUILayout.Space(20);
        if(capsManagerFoldout && playerManagerFoldout && playerMovementFoldout && islandCreatorFoldout && islandFoldout && bossManagerFoldout)
        {
            if (GUILayout.Button("CloseAll")){ ActiveAllFoldout(false); }
        }
        else
        {
            if (GUILayout.Button("OpenAll")) { ActiveAllFoldout(true); }
        }
      
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(debugingTool);
        Repaint();
    }

    private void ActiveAllFoldout(bool open)
    {
        capsManagerFoldout = open;
        playerManagerFoldout = open;
        playerMovementFoldout = open;
        islandCreatorFoldout = open;
        islandFoldout = open;
        bossManagerFoldout = open;
    }
}