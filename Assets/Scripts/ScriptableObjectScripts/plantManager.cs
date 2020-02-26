using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "plantManager", menuName = "ScriptableObjects/SpawnPlantManager", order = 1)]
public class plantManager : ScriptableObject
{
    public resourceManager resourceManager;
    public List<plant> plants = new List<plant>();

    [System.Serializable]
    public class plant
    {
        public string name;
        public string type;
        public float xpToGive = 0.0f;
        public List<resource> stage1 = new List<resource>();
        public List<resource> stage2 = new List<resource>();
        public List<resource> stage3 = new List<resource>();
        public GameObject stages;
    }

    [System.Serializable]
    public class resource
    {
        public string name;
        public int required;
    }
}
