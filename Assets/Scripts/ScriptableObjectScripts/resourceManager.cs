using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "ScriptableObjects/SpawnResourceManager", order = 1)]
public class resourceManager : ScriptableObject
{
    public List<Resource> resources;
    public List<Seed> seeds;
    public List<Images> images;
    public SaveLoad SL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Save()
    {
        SL.resources.Clear();
        foreach (Resource res in resources)
        {
            SL.resources.Add(res);
        }
        SL.BuildSave();
    }

    [System.Serializable]
    public class Resource
    {
        public string name;
        public int value;
        public int defaultValue;
        public int teethValue;

        public bool Add(int change)
        {
            value += change;
            return true;
        }

        public bool Remove(int change)
        {
            if (value - change >= 0)
            {
                value -= change;
                return true;
            }
            else
                return false;
        }

        public int RemoveForce(int change)
        {
            if (value - change < 0)
            {
                int valueChange = value;
                value = 0;
                return valueChange;
            }
            else
            {
                value -= change;
                return change;
            }
        }
    }

    [System.Serializable]
    public class Seed
    {
        public string name;
        public string plantName;
        public int level;
        public int value;
        public int defaultValue;
        public int teethValue;

        public bool Add(int change)
        {
            defaultValue += change;
            return true;
        }

        public bool Remove(int change)
        {
            if (value - change >= 0)
            {
                value -= change;
                return true;
            }
            else
                return false;
        }
    }

    [System.Serializable]
    public class Images
    {
        public string name;
        public Sprite img;
    }
}