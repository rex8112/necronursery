using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graveController : MonoBehaviour
{
    [SerializeField] resourceManager resourceManager;
    [SerializeField] plantManager plantManager;
    [Space(10)]
    [Header("Grave Information")]
    public plantManager.plant seed;
    public int stage;
    public List<graveResource> requiredResources = new List<graveResource>();
    GameObject stages;

    [SerializeField]
    GameObject resourceInformation;


    public void activate()
    {
        resourceInformation.SetActive(true);
    }

    public void deactivate()
    {
        resourceInformation.SetActive(false);
    }

    public void plant(string name)
    {
        seed = plantManager.plants.Find(seed => seed.name == name);

    }

    [System.Serializable]
    public class graveResource
    {
        public string name;
        public int current;
        public int needed;
    }
}
