using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PersoClothSwaper))]
public class CharaFaceSwaperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PersoFaceSwaper selected = (PersoFaceSwaper)target;
        if (GUILayout.Button("SwapFace"))
        {
            selected.ChangeFace(selected.FacePack);
        }
    }
}
