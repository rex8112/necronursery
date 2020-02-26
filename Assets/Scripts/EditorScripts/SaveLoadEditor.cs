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
        string path = "plantManager";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        saveLoad.tapToStart = EditorGUILayout.Toggle("Tap To Start", saveLoad.tapToStart);
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Player Variables", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("XP To Level Up", saveLoad.xpToLevel.ToString());
        saveLoad.level = EditorGUILayout.IntField("Level", saveLoad.level);
        saveLoad.xp = EditorGUILayout.FloatField("XP", saveLoad.xp);
        saveLoad.xpPerLevel = EditorGUILayout.FloatField("XP Per Level", saveLoad.xpPerLevel);
        EditorGUI.indentLevel++;
        path = "knowledge";
        EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
        EditorGUI.indentLevel--;

        if (GUILayout.Button("Refresh Knowledge"))
        {
            saveLoad.knowledge.Clear();
            foreach (plantManager.plant plant in saveLoad.plantManager.plants)
            {
                Knowledge knowledge = new Knowledge
                {
                    name = plant.name,
                };
                knowledge.plant = saveLoad.plantManager.plants.Find(x => x.name == plant.name);
                knowledge.stage1.Clear();
                foreach (plantManager.resource res in plant.stage1)
                {
                    KnowledgeResource knowledgeResource = new KnowledgeResource
                    {
                        name = res.name,
                        known = false
                    };
                    knowledge.stage1.Add(knowledgeResource);
                }
                knowledge.stage2.Clear();
                foreach (plantManager.resource res in plant.stage2)
                {
                    KnowledgeResource knowledgeResource = new KnowledgeResource
                    {
                        name = res.name,
                        known = false
                    };
                    knowledge.stage2.Add(knowledgeResource);
                }
                knowledge.stage3.Clear();
                foreach (plantManager.resource res in plant.stage3)
                {
                    KnowledgeResource knowledgeResource = new KnowledgeResource
                    {
                        name = res.name,
                        known = false
                    };
                    knowledge.stage3.Add(knowledgeResource);
                }
                saveLoad.knowledge.Add(knowledge);
            }
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("Save Variables", EditorStyles.boldLabel);
        path = "resources";
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
