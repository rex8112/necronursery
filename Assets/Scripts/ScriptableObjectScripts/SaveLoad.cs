using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Save", menuName = "ScriptableObjects/SpawnSave", order = 1)]
public class SaveLoad : ScriptableObject
{
    public bool tapToStart = true;
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<resourceManager.Seed> seeds = new List<resourceManager.Seed>();
    public List<string> plants = new List<string>();
    public List<int> stageInts = new List<int>();
    public List<List<graveController.graveResource>> gResources = new List<List<graveController.graveResource>>();
    Save mainSave = new Save();
    public void BuildSave() //Takes the variables above and loads them into the Save object to then be saved to the disk
    {
        Debug.Log("Building Save");
        mainSave.resources = resources;
        mainSave.seeds = seeds;
        mainSave.plants = plants;
        mainSave.stageInts = stageInts;
        mainSave.gResources = gResources;
        SaveToDisk(mainSave);
    }

    public bool UnbuildSave() //Takes the loaded Save and populates the Scriptable Object's values
    {
        mainSave = LoadFromDisk();
        if (mainSave.stageInts.Count >= 1) //Checks if the returned save has content
        {
            resources = mainSave.resources;
            seeds = mainSave.seeds;
            plants = mainSave.plants;
            stageInts = mainSave.stageInts;
            gResources = mainSave.gResources;
            return true;
        }
        else //Clears everything otherwise so it doesn't get loaded
        {
            resources.Clear();
            seeds.Clear();
            plants.Clear();
            stageInts.Clear();
            gResources.Clear();
            return false;
        }
    }

    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/NNSave.save");
        resources.Clear();
        seeds.Clear();
        plants.Clear();
        stageInts.Clear();
        gResources.Clear();
    }

    public void SaveToDisk(Save save) //Handles converting the Save class into binary and saving it to a file
    {
        Debug.Log("Saving to Disk");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/NNSave.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, save);
        file.Close();
    }

    public Save LoadFromDisk() //Handles taking the binary from the file and returning it to it's Save class form
    {
        if (File.Exists(Application.persistentDataPath + "/NNSave.save")) //Checks if the save file exists to prevent an error
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/NNSave.save", FileMode.Open);
            Debug.Log("Loading from disk");
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            return save;
        }
        Save Failed = new Save(); //Creates and empty save to return, which gets recognized as empty and is ignored
        return Failed;
    }
}

[System.Serializable]
public class Save
{
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<resourceManager.Seed> seeds = new List<resourceManager.Seed>();
    public List<string> plants = new List<string>();
    public List<int> stageInts = new List<int>();
    public List<List<graveController.graveResource>> gResources = new List<List<graveController.graveResource>>();
}