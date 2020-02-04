using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(resourceManager))]
public class resourceManagerEditor : Editor
{
    bool show = false;
    public override void OnInspectorGUI()
    {
        resourceManager resourceManager = (resourceManager)target;

        string path = "sl";
        //EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        path = "resources";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        path = "images";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);

        show = EditorGUILayout.Foldout(show, "Seeds");
        if (show)
        {
            foreach (resourceManager.Seed seed in resourceManager.seeds)
            {
                EditorGUILayout.BeginVertical("Box");
                seed.name = EditorGUILayout.TextField("Seed Name:", seed.name);
                EditorGUILayout.LabelField("Plant Name:", seed.plantName);
                seed.level = EditorGUILayout.IntField("Level: ", seed.level);
                seed.value = EditorGUILayout.IntField("Value:", seed.value);
                seed.defaultValue = EditorGUILayout.IntField("Default Value:", seed.defaultValue);
                seed.teethValue = EditorGUILayout.IntField("Teeth Value:", seed.teethValue);
                EditorGUILayout.EndVertical();
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
