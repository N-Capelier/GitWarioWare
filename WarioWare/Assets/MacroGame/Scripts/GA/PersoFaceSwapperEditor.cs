using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PersoFaceSwaper))]
public class PersoFaceSwaperEditor : Editor
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
