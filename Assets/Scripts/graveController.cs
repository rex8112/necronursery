using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graveController : MonoBehaviour
{
    public Events sc;
    public resourceManager resourceManager;
    public plantManager plantManager;
    public SaveLoad saveLoad;
    [Space(10)]
    [Header("Grave Information")]
    public plantManager.plant plant;
    public resourceManager.Seed seed;
    public int stage;
    public List<graveResource> requiredResources = new List<graveResource>();
    public List<graveResource> currentResources = new List<graveResource>();
    GameObject stages;
    [Space(10)]
    [Header("UI Information")]
    [SerializeField] GameObject graveImageCanvas;
    public GameObject resourceInformation;
    [SerializeField] GameObject seedView;
    [SerializeField] GameObject plantButton;
    [SerializeField] GameObject infoGroup;
    [SerializeField] GameObject giveButton;
    [SerializeField] GameObject resourcePanel;
    [SerializeField] GameObject resourceGiver;
    List<GameObject> resourceGivers = new List<GameObject>();


    public void Awake()
    {
        sc = GameObject.Find("sceneController").GetComponent<Events>();
        sc.OnValueChange.AddListener(updateInfo);
        sc.OnValueChange.AddListener(UpdateResourcePanel);
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
            Debug.Log(res.name + ": " + res.value);
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
                        value = p.required
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
        //int full = 0;
        //foreach (graveResource resource in requiredResources)
        //{
        //    resourceManager.Resource giveResource = resourceManager.resources.Find(r => r.name == resource.name);
        //    resource.current += giveResource.RemoveForce(resource.value - resource.current);

        //    if (resource.current >= resource.value)
        //    {
        //        full += 1;
        //    }
        //}

        ////Debug.Log(full + " Full " + requiredResources.Count + " Capacity");
        //if (full >= requiredResources.Count)
        //{
        //    nextStage();
        //}

        sc.OnValueChange.Invoke();
    }

    public void updateInfo()
    {
        Text plantName = infoGroup.transform.Find("PlantName").GetComponent<Text>();
        Text reqResources = infoGroup.transform.Find("ReqResources").Find("Text").GetComponent<Text>();

        plantName.text = plant.name;
        reqResources.text = "";
        foreach (graveResource res in requiredResources)
        {
            string nameString = res.name;
            if (res.known == false)
                nameString = "Unknown";
            reqResources.text += (res.name + ": " + res.value + "\n");
        }
    }

    public void UpdateResourcePanel()
    {
        foreach (Transform child in resourcePanel.transform)
        {
            Destroy(child);
        }
        foreach (resourceManager.Resource resource in resourceManager.resources)
        {
            GameObject r = Instantiate(resourceGiver, resourcePanel.transform);
            r.name = resource.name;
            r.transform.GetChild(0).GetComponent<Image>().sprite = resourceManager.images.Find(x => x.name == resource.name).img;
            resourceGivers.Add(r);
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
                gr.value = plant.required;


                requiredResources.Add(gr);
            }
            stage = 2;
            stages.GetComponent<prefabChange>().nextStage(stage);
        }
        else if (stage == 2)
        {
            requiredResources.Clear();
            currentResources.Clear();
            foreach (plantManager.resource plant in plant.stage3)
            {
                graveResource gr = new graveResource();
                gr.name = plant.name;
                gr.value = plant.required;


                requiredResources.Add(gr);
            }
            stage = 3;
            stages.GetComponent<prefabChange>().nextStage(stage);
        }
        else if (stage == 3)
        {
            saveLoad.AddXP(plant.xpToGive);
            DestroyPlant();
        }

        sc.OnValueChange.Invoke();
    }

    public void DestroyPlant()
    {
        requiredResources.Clear();
        currentResources.Clear();
        plant = new plantManager.plant();
        seed = new resourceManager.Seed();
        Destroy(stages);
        deactivate(infoGroup);
        activate(plantButton);
        stage = 0;
    }

    [System.Serializable]
    public class graveResource
    {
        public string name;
        public int value;
        public bool known = true;
    }
}
