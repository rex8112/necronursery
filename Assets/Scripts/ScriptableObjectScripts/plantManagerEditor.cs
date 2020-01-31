using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(plantManager))]
public class plantManagerEditor : Editor
{
    bool show = true;
    List<string> newName = new List<string>();
    List<bool> delete = new List<bool>();
    string newPlant = "";
    public override void OnInspectorGUI()
    {
        plantManager plantManager = (plantManager)target;

        if (newName.Count < plantManager.plants.Count)
        {
            int diff = plantManager.plants.Count - newName.Count;
            for (int i = 0; i < diff; i++)
            {
                newName.Add("");
            }
        }

        if (delete.Count < plantManager.plants.Count)
        {
            int diff = plantManager.plants.Count - delete.Count;
            for (int i = 0; i < diff; i++)
            {
                delete.Add(false);
            }
        }

        plantManager.resourceManager = (resourceManager) EditorGUILayout.ObjectField("Resource Manager: ", plantManager.resourceManager, typeof(resourceManager), false);
        show = EditorGUILayout.Foldout(show, "Plants");
        if (show)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.HelpBox(new GUIContent { text = "Be sure to set seed values in Resource Manager after adding." });
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("New Plant"))
            {
                if (newPlant != "")
                {
                    plantManager.plant nPlant = new plantManager.plant
                    {
                        name = newPlant
                    };
                    plantManager.plants.Add(nPlant);
                    resourceManager.Seed seed = new resourceManager.Seed
                    {
                        name = newPlant + " Seed",
                        plantName = newPlant,
                        defaultValue = 0,
                        teethValue = 10
                    };
                    plantManager.resourceManager.seeds.Add(seed);
                }
            }
            newPlant = EditorGUILayout.TextField(newPlant);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            int current = 0;
            foreach (plantManager.plant plant in plantManager.plants)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.LabelField("Name: ", plant.name);
                plant.type = EditorGUILayout.TextField("Type: ", plant.type);
                plant.level = EditorGUILayout.IntField("Level: ", plant.level);
                plant.xpToGive = EditorGUILayout.FloatField("XP To Give: ", plant.xpToGive);

                string path = "plants.Array.data[" + current.ToString() + "].stage1";
                EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
                path = "plants.Array.data[" + current.ToString() + "].stage2";
                EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);
                path = "plants.Array.data[" + current.ToString() + "].stage3";
                EditorGUILayout.PropertyField(serializedObject.FindProperty(path), true);

                plant.stages = (GameObject) EditorGUILayout.ObjectField("Stages: ", plant.stages, typeof(GameObject), true);
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Change Name"))
                {
                    if (newName[current] != "")
                    {
                        Debug.Log(newName);
                        plantManager.resourceManager.seeds.Find(s => s.plantName == plant.name).plantName = newName[current];
                        plant.name = newName[current];
                    }
                }
                newName[current] = EditorGUILayout.TextField(newName[current]);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                delete[current] = EditorGUILayout.ToggleLeft("Delete?", delete[current]);
                if (delete[current])
                {
                    if (GUILayout.Button("Confirm Deletion"))
                    {
                        resourceManager.Seed seed = plantManager.resourceManager.seeds.Find(s => s.plantName == plant.name);
                        plantManager.resourceManager.seeds.Remove(seed);
                        plantManager.plants.Remove(plant);

                    }
                }
                current++;
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
            EditorGUI.indentLevel--;
        }
    }
}
