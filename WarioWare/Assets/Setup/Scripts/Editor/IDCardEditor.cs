using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using Trisibo;

[CustomEditor(typeof(IDCard))]
public class IDCardEditor : Editor {

	private IDCard idCard;
	private TrioAurelien trioAurel;
	private TrioTheodore trioTheo;
	private TrioThibault trioThibault;

	private void OnEnable() {
		idCard = target as IDCard;
	}
	

	
	public override void OnInspectorGUI()
	{
		if (idCard.idName == null || idCard.idName == string.Empty)
			idCard.idName = idCard.name;
		idCard.idName = EditorGUILayout.TextField("Nom :" ,idCard.idName);
		idCard.cluster = (Cluster)EditorGUILayout.EnumPopup("Cluster ",idCard.cluster);
        switch (idCard.cluster)
        {
            case Cluster.Theodore:
				trioTheo = (TrioTheodore)EditorGUILayout.EnumPopup("Trio ", trioTheo);
				idCard.trio = trioTheo.ToString();
				break;
            case Cluster.Aurelien:
				trioAurel = (TrioAurelien)EditorGUILayout.EnumPopup("Trio ", trioAurel);
				idCard.trio = trioTheo.ToString();
				break;
            case Cluster.Thibault:
				trioThibault = (TrioThibault)EditorGUILayout.EnumPopup("Trio ", trioThibault);
				idCard.trio = trioTheo.ToString();
				break;
            default:
                break;
        }
		EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(idCard.microGameScene)));
		EditorUtility.SetDirty(idCard);
		Repaint();
		serializedObject.ApplyModifiedProperties();
    }
}
