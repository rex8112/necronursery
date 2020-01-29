using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(graveController))]
public class GraveyardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        graveController gc = (graveController)target;
        EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel);
        if (GUILayout.Button("Toggle Grave Info"))
        {
            gc.toggle(gc.resourceInformation);
        }
        int index = 0;
        List<string> plantStrings = new List<string>();
        foreach (plantManager.plant plant in gc.plantManager.plants)
        {
            plantStrings.Add(plant.name);
        }
        string[] plants = plantStrings.ToArray();
        Rect r = EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Force Plant"))
        {
            gc.Plant(plantStrings[index], 0);
        }
        index = EditorGUILayout.Popup(index, plants);
        EditorGUILayout.EndHorizontal();

    }
}
