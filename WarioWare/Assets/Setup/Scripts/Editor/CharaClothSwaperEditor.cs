using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharaClothSwap))]
public class CharaClothSwaperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CharaClothSwap selected = (CharaClothSwap)target;
        if (GUILayout.Button("SwapCloth"))
        {
            selected.ChangeCloth(selected.ClothPack);
        }
    }
}
