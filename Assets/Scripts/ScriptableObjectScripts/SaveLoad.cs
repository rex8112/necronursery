using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Save", menuName = "ScriptableObjects/SpawnSave", order = 1)]
public class SaveLoad : ScriptableObject
{
    public plantManager plantManager;

    public bool tapToStart = true;
    public List<Knowledge> knowledge = new List<Knowledge>();
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<resourceManager.Seed> seeds = new List<resourceManager.Seed>();
    public List<string> plants = new List<string>();
    public List<int> stageInts = new List<int>();
    public List<List<graveController.graveResource>> gResources = new List<List<graveController.graveResource>>();
    public List<List<graveController.graveResource>> currentResources = new List<List<graveController.graveResource>>();

    Save mainSave = new Save();
    public int level = 1;
    public float xp = 0.0f;
    public float xpPerLevel = 40;

    public float xpToLevel
    { get { return 100 + (level - 1) * xpPerLevel; } }

    public void AddXP(float toAdd)
    {
        xp += toAdd;
        LevelUp();
    }

    public void LevelUp()
    {
        if (xp >= xpToLevel)
        {
            xp -= xpToLevel;
            level++;
        }
    }


    public void BuildSave() //Takes the variables above and loads them into the Save object to then be saved to the disk
    {
        Debug.Log("Building Save");
        mainSave.xp = xp;
        mainSave.level = level;
        mainSave.resources = resources;
        mainSave.seeds = seeds;
        mainSave.plants = plants;
        mainSave.stageInts = stageInts;
        mainSave.gResources = gResources;
        mainSave.currentResources = currentResources;
        SaveToDisk(mainSave);
    }

    public bool UnbuildSave() //Takes the loaded Save and populates the Scriptable Object's values
    {
        mainSave = LoadFromDisk();
        if (mainSave.stageInts.Count >= 1) //Checks if the returned save has content
        {
            xp = mainSave.xp;
            level = mainSave.level;
            resources = mainSave.resources;
            seeds = mainSave.seeds;
            plants = mainSave.plants;
            stageInts = mainSave.stageInts;
            gResources = mainSave.gResources;
            currentResources = mainSave.currentResources;
            return true;
        }
        else //Clears everything otherwise so it doesn't get loaded
        {
            resources.Clear();
            seeds.Clear();
            plants.Clear();
            stageInts.Clear();
            gResources.Clear();
            currentResources.Clear();
            return false;
        }
    }

    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/NNSave.save");
        level = 1;
        xp = 0f;
        resources.Clear();
        seeds.Clear();
        plants.Clear();
        stageInts.Clear();
        gResources.Clear();
        currentResources.Clear();
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
    public float xp;
    public int level;
    public List<resourceManager.Resource> resources = new List<resourceManager.Resource>();
    public List<resourceManager.Seed> seeds = new List<resourceManager.Seed>();
    public List<string> plants = new List<string>();
    public List<int> stageInts = new List<int>();
    public List<List<graveController.graveResource>> gResources = new List<List<graveController.graveResource>>();
    public List<List<graveController.graveResource>> currentResources = new List<List<graveController.graveResource>>();
}

[System.Serializable]
public class Knowledge
{
    public string name;
    public plantManager.plant plant;
    public List<KnowledgeResource> stage1;
    public List<KnowledgeResource> stage2;
    public List<KnowledgeResource> stage3;
}

[System.Serializable]
public class KnowledgeResource
{
    public string name;
    public bool known;
}