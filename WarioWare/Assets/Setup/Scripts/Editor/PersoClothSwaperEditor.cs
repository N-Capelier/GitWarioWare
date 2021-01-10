using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PersoClothSwaper))]
public class PersoClothSwaperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PersoClothSwaper selected = (PersoClothSwaper)target;
        if (GUILayout.Button("SwapCloth"))
        {
            selected.ChangeCloth(selected.ClothPack);
        }
    }
}
