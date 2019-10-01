using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class save : MonoBehaviour
{
    [SerializeField]
    resourceManager resourceManager;
    public void saveResources()
    {
        List<resourceManager.Resource> save = resourceManager.resources;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        gameObject.GetComponentInChildren<Text>().text = Application.persistentDataPath;
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, save);
        file.Close();
    }

    public void loadResources()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        Debug.Log(Application.persistentDataPath);
        List<resourceManager.Resource> save = (List<resourceManager.Resource>)bf.Deserialize(file);
        file.Close();

        foreach (resourceManager.Resource resource in save)
        {
            Debug.Log(resource.name);
            Debug.Log(resource.value);
        }
    }
}
