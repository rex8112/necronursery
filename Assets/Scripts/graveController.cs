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
        if (stage == 0)
        {
            seed = plantManager.plants.Find(seed => seed.name == name);
            foreach (plantManager.resource plant in seed.stage1)
            {
                graveResource shit = new graveResource();
                shit.name = plant.name;
                shit.needed = plant.required;


                requiredResources.Add(shit);
            }
            stages = seed.stages;


            stage = 1;
        }
        

    }

    public void give()
    {
        foreach (graveResource resource in requiredResources)
        {
            resourceManager.Resource giveResource = resourceManager.resources.Find(r => r.name == resource.name);
            if (giveResource.Remove(resource.needed))
            {
                Debug.Log("it worked");
                resource.current += resource.needed;
            }
            else
            {
                Debug.Log("It didnt work");
            }
        }


    }

    [System.Serializable]
    public class graveResource
    {
        public string name;
        public int current;
        public int needed;
    }
}
