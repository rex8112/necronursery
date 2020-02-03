using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveLoad))]
public class SaveLoadEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveLoad saveLoad = (SaveLoad)target;

        EditorGUILayout.LabelField("XP To Level Up", saveLoad.xpToLevel.ToString());
    }
}
