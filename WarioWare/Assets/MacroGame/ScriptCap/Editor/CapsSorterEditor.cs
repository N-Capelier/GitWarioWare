using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CapsSorter))]
public class CapsSorterEditor : Editor
{

    private CapsSorter capsSorter;

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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("chalInput"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(capsSorter.idCards)));
        if (GUILayout.Button("SorteCards"))
        {
            Reset();
            Sorter();
        }
        if (capsSorter.sortedIdCards.Count !=0)
        {
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sortedIdCards"),true);
        }
        EditorUtility.SetDirty(capsSorter);
        Repaint();
        serializedObject.ApplyModifiedProperties();
    }

    private void Reset()
    {
        capsSorter.sortedIdCards = new List<IDCardList>((int)ChallengeHaptique.A10);
        for (int i = 0; i <(int) ChallengeHaptique.A10 +1; i++)
        {
            capsSorter.sortedIdCards.Add(new IDCardList()); ;
        }
    }

    private void Sorter()
    {

        for (int i = 0; i < capsSorter.idCards.Count; i++)
        {
            if(capsSorter.chalInput)
            capsSorter.sortedIdCards[(int)capsSorter.idCards[i].inputChal].IDCards.Add(capsSorter.idCards[i]);
            else
            capsSorter.sortedIdCards[(int)capsSorter.idCards[i].haptiqueChal].IDCards.Add(capsSorter.idCards[i]);
        }

    }
}
