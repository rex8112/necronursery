using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Save", menuName = "ScriptableObjects/SpawnSave", order = 1)]
public class SaveLoad : ScriptableObject
{
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<plantManager.plant> plants = new List<plantManager.plant>();
    public List<int> stageInts = new List<int>();
    Save mainSave = new Save();
    public void BuildSave()
    {
        mainSave.resources = resources;
        mainSave.plants = plants;
        mainSave.stageInts = stageInts;
        SaveToDisk(mainSave);
    }

    public void UnbuildSave()
    {
        mainSave = LoadFromDisk();
        resources = mainSave.resources;
        plants = mainSave.plants;
        stageInts = mainSave.stageInts;
    }

    public void SaveToDisk(Save save)
    { 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/NNSave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public Save LoadFromDisk()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/NNSave.save", FileMode.Open);
        Debug.Log(Application.persistentDataPath);
        Save save = (Save)bf.Deserialize(file);
        file.Close();

        return save;
    }
}

public class Save
{
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<plantManager.plant> plants = new List<plantManager.plant>();
    public List<int> stageInts = new List<int>();
}