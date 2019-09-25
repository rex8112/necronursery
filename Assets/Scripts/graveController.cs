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
    [Space(10)]
    [Header("UI Information")]
    [SerializeField] GameObject resourceInformation;
    [SerializeField] GameObject seedView;
    [SerializeField] GameObject plantButton;



    public void activateRI()
    {
        resourceInformation.SetActive(true);
    }

    public void activate(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void deactivate(GameObject ui)
    {
        ui.SetActive(false);
    }
    public void toggle(GameObject ui)
    {
        if (ui.activeSelf == true)
            deactivate(ui);
        else
            activate(ui);
    }

    public void plant(string name)
    {
        if (stage == 0)
        {
            seed = plantManager.plants.Find(seed => seed.name == name);
            foreach (plantManager.resource plant in seed.stage1)
            {
                graveResource gr = new graveResource();
                gr.name = plant.name;
                gr.needed = plant.required;


                requiredResources.Add(gr);
            }
            stages = seed.stages;


            stage = 1;
        }

        deactivate(seedView);
        deactivate(plantButton);
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
