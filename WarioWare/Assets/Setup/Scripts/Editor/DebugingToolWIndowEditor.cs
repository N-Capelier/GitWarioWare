using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewards;
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
            else if (i == 13)
            {
                if(capsManagerFoldout)
                    EditorGUILayout.Space(20);
                playerManagerFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(playerManagerFoldout, "Player Manager");
            }
            else if(i==17)
            {
                if(playerManagerFoldout)
                    EditorGUILayout.Space(20);
                playerMovementFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(playerMovementFoldout, "Player Movement");
            }
            else if (i == 19)
            {
                if(playerMovementFoldout)
                    EditorGUILayout.Space(20);
                islandCreatorFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(islandCreatorFoldout, "Island Creator");
            }
            else if (i == 24)
            {
                if (islandCreatorFoldout)
                    EditorGUILayout.Space(20);
                islandFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(islandFoldout, "Island");
            }
            else if( i == 25)
            {
                if(islandFoldout)
                EditorGUILayout.Space(20);
                bossManagerFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(bossManagerFoldout, "Boss Manager");
            }

            if (capsManagerFoldout && i<13||
                playerManagerFoldout && i>=13 && i<17||
                playerMovementFoldout && i >= 17&& i < 19 ||
                islandCreatorFoldout && i >= 19 && i < 24 ||
                islandFoldout && i >= 24 && i < 25 || 
                bossManagerFoldout && i>=25)
            {

                if (i == 10)
                    EditorGUILayout.HelpBox("The number of mini game per cap is equal to = size of the island + number of the zone + miniGameNumberPerCap",MessageType.Warning);
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
