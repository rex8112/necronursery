﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Save", menuName = "ScriptableObjects/SpawnSave", order = 1)]
public class SaveLoad : ScriptableObject
{
    [SerializeField] List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    [SerializeField] List<plantManager.plant> plants = new List<plantManager.plant>();
    [SerializeField] List<int> stageInts = new List<int>();
    public void SaveToDisk(Save save)
    { 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public Save LoadFromDisk()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        Debug.Log(Application.persistentDataPath);
        Save save = (Save)bf.Deserialize(file);
        file.Close();

        return save;
    }
}

public class Save
{
    List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    List<plantManager.plant> plants = new List<plantManager.plant>();
    List<int> stageInts = new List<int>();
}