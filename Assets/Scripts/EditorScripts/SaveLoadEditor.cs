using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveLoad))]
public class SaveLoadEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SaveLoad saveLoad = (SaveLoad)target;
        saveLoad.tapToStart = EditorGUILayout.Toggle("Tap To Start", saveLoad.tapToStart);
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Player Variables", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("XP To Level Up", saveLoad.xpToLevel.ToString());
        saveLoad.level = EditorGUILayout.IntField("Level", saveLoad.level);
        saveLoad.xp = EditorGUILayout.FloatField("XP", saveLoad.xp);
        saveLoad.xpPerLevel = EditorGUILayout.FloatField("XP Per Level", saveLoad.xpPerLevel);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("Save Variables", EditorStyles.boldLabel);
        string path = "resources";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        path = "seeds";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        path = "plants";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        path = "stageInts";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
