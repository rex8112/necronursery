using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField] GameObject graveImageCanvas;
    [SerializeField] GameObject resourceInformation;
    [SerializeField] GameObject seedView;
    [SerializeField] GameObject plantButton;
    [SerializeField] GameObject infoGroup;
    public UnityEvent OnValueChange;


    public void Awake()
    {
        OnValueChange.AddListener(updateInfo);
    }

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
        if (ui.name == "graveInfo")
        {
            GameObject.Find("sceneController").GetComponent<sceneController>().lockCamera = false;
        }
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
            stages = Instantiate(stages, graveImageCanvas.transform);

            stage = 1;
        }

        


        OnValueChange.Invoke();
        deactivate(seedView);
        deactivate(plantButton);
        activate(infoGroup);
    }

    public void give()
    {
        int full = 0;
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

            if (resource.current >= resource.needed)
            {
                full += 1;
            }
        }

        //Debug.Log(full + " Full " + requiredResources.Count + " Capacity");
        if (full >= requiredResources.Count)
        {
            nextStage();
        }

        OnValueChange.Invoke();
    }

    public void updateInfo()
    {
        Text plantName = infoGroup.transform.Find("PlantName").GetComponent<Text>();
        Text reqResources = infoGroup.transform.Find("ReqResources").Find("Text").GetComponent<Text>();
        Text curResources = infoGroup.transform.Find("CurResources").Find("Text").GetComponentInChildren<Text>();

        plantName.text = seed.name;
        reqResources.text = "";
        curResources.text = "";
        foreach (graveResource res in requiredResources)
        {
            reqResources.text += (res.name + ": " + res.needed + "\n");
            curResources.text += (res.name + ": " + res.current + "\n");
        }
    }

    public void nextStage()
    {
        if (stage == 1)
        {
            requiredResources.Clear();
            foreach (plantManager.resource plant in seed.stage2)
            {
                graveResource gr = new graveResource();
                gr.name = plant.name;
                gr.needed = plant.required;


                requiredResources.Add(gr);
            }
            stages.GetComponent<prefabChange>().nextStage();

            stage = 2;
        }
        else if (stage == 2)
        {
            requiredResources.Clear();
            stages.GetComponent<prefabChange>().nextStage();
            stage = 3;
        }

        OnValueChange.Invoke();
    }

    [System.Serializable]
    public class graveResource
    {
        public string name;
        public int current;
        public int needed;
    }
}
