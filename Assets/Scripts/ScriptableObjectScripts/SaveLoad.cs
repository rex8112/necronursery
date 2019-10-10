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
    public List<string> plants = new List<string>();
    public List<int> stageInts = new List<int>();
    Save mainSave = new Save();
    public void BuildSave()
    {
        Debug.Log("Building Save");
        mainSave.resources = resources;
        mainSave.plants = plants;
        mainSave.stageInts = stageInts;
        SaveToDisk(mainSave);
    }

    public void UnbuildSave()
    {
        mainSave = LoadFromDisk();
        if (mainSave.stageInts.Count >= 1)
        {
            resources = mainSave.resources;
            plants = mainSave.plants;
            stageInts = mainSave.stageInts;
        }
        else
        {
            resources.Clear();
            plants.Clear();
            stageInts.Clear();
        }
    }

    public void SaveToDisk(Save save)
    {
        Debug.Log("Saving to Disk");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/NNSave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public Save LoadFromDisk()
    {
        if (File.Exists(Application.persistentDataPath + "/NNSave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/NNSave.save", FileMode.Open);
            Debug.Log("Loading from disk");
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            return save;
        }
        Save Failed = new Save();
        return Failed;
    }
}

[System.Serializable]
public class Save
{
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<string> plants = new List<string>();
    public List<int> stageInts = new List<int>();
}