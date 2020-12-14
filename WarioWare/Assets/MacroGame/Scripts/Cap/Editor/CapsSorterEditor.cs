using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CapsSorter))]
public class CapsSorterEditor : Editor
{

    private CapsSorter capsSorter;
    private int initialListWeight;
    private void OnEnable()
    {
        capsSorter = target as CapsSorter;
    }



    private void OnGUI()
    {

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(capsSorter.idCards)));
              
        EditorUtility.SetDirty(capsSorter);
        Repaint();
        serializedObject.ApplyModifiedProperties();
    }


    
}
