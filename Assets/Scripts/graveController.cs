using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class graveController : MonoBehaviour
{
    [SerializeField] Events sc;
    [SerializeField] resourceManager resourceManager;
    [SerializeField] plantManager plantManager;
    [Space(10)]
    [Header("Grave Information")]
    public plantManager.plant plant;
    public resourceManager.Seed seed;
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
    [SerializeField] GameObject giveButton;


    public void Awake()
    {
        sc = GameObject.Find("sceneController").GetComponent<Events>();
        sc.OnValueChange.AddListener(updateInfo);
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

    public void LoadResources(List<graveResource> gResources)
    {
        requiredResources.Clear();
        foreach (graveResource res in gResources)
        {
            requiredResources.Add(res);
            Debug.Log(res.name + ": " + res.current + " / " + res.needed);
        }
        sc.OnValueChange.Invoke();
    }

    public void Plant(string name, int seedCost)
    {
        if (stage == 0)
        {
            plant = plantManager.plants.Find(p => p.name == name);
            seed = resourceManager.seeds.Find(s => s.plantName == name);
            if (seed.Remove(seedCost))
            {
                foreach (plantManager.resource p in plant.stage1)
                {
                    graveResource gr = new graveResource
                    {
                        name = p.name,
                        needed = p.required
                    };


                    requiredResources.Add(gr);
                }
                stages = plant.stages;
                stages = Instantiate(stages, graveImageCanvas.transform);

                stage = 1;

                sc.OnValueChange.Invoke();
                deactivate(seedView);
                deactivate(plantButton);
                activate(infoGroup);
            }
            else
            {
                seed = new resourceManager.Seed();
                plant = new plantManager.plant();
            }
        }
    }

    public void give()
    {
        int full = 0;
        foreach (graveResource resource in requiredResources)
        {
            resourceManager.Resource giveResource = resourceManager.resources.Find(r => r.name == resource.name);
            resource.current += giveResource.RemoveForce(resource.needed - resource.current);

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

        sc.OnValueChange.Invoke();
    }

    public void updateInfo()
    {
        Text plantName = infoGroup.transform.Find("PlantName").GetComponent<Text>();
        Text reqResources = infoGroup.transform.Find("ReqResources").Find("Text").GetComponent<Text>();
        Text curResources = infoGroup.transform.Find("CurResources").Find("Text").GetComponentInChildren<Text>();

        plantName.text = plant.name;
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
            foreach (plantManager.resource plant in plant.stage2)
            {
                graveResource gr = new graveResource();
                gr.name = plant.name;
                gr.needed = plant.required;


                requiredResources.Add(gr);
            }
            stage = 2;
            stages.GetComponent<prefabChange>().nextStage(stage);
        }
        else if (stage == 2)
        {
            requiredResources.Clear();
            foreach (plantManager.resource plant in plant.stage3)
            {
                graveResource gr = new graveResource();
                gr.name = plant.name;
                gr.needed = plant.required;


                requiredResources.Add(gr);
            }
            stage = 3;
            stages.GetComponent<prefabChange>().nextStage(stage);
        }
        else if (stage == 3)
        {
            requiredResources.Clear();
            Destroy(stages);
            deactivate(infoGroup);
            activate(plantButton);
            stage = 0;
        }

        sc.OnValueChange.Invoke();
    }

    [System.Serializable]
    public class graveResource
    {
        public string name;
        public int current;
        public int needed;
    }
}
