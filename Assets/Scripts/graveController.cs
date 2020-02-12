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
    [SerializeField] GameObject resourcePanel;
    [SerializeField] GameObject resourceGiver;
    List<GameObject> resourceGivers = new List<GameObject>();
    int m_selectedTotal = 0;
    public int SelectedTotal
    {
        get { return m_selectedTotal;  }
        set { m_selectedTotal = value; }
    }
    public int RequiredTotal
    { get
        {
            int tmp = 0;
            foreach (graveResource resource in requiredResources)
                tmp += resource.value;
            return tmp;
        } }

    public int CurrentTotal
    {
        get
        {
            int tmp = 0;
            foreach (graveResource resource in currentResources)
                tmp += resource.value;
            return tmp;
        }
    }


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

    public void LoadResources(List<graveResource> gResources, List<graveResource> cResources)
    {
        requiredResources.Clear();
        currentResources.Clear();
        foreach (graveResource res in gResources)
        {
            requiredResources.Add(res);
            Debug.Log(res.name + ": " + res.value);
        }
        foreach (graveResource res in cResources)
        {
            currentResources.Add(res);
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
        foreach (Transform child in resourcePanel.transform)
        {
            resourceManager.Resource resource = resourceManager.resources.Find(x => x.name == child.name);
            int value = child.GetComponent<ResourceGiver>().Value;
            int changedValue = resource.RemoveForce(value);

            if (changedValue > 0)
            {
                graveResource resourceChanged = currentResources.Find(x => x.name == child.name);
                if (resourceChanged == null)
                {
                    var tmp = new graveResource
                    {
                        name = child.name,
                        value = changedValue,
                        known = true
                    };
                    currentResources.Add(tmp);
                }
                else
                    resourceChanged.value += changedValue;
            }
        }



        sc.OnValueChange.Invoke();
    }

    public void updateInfo()
    {
        Text plantName = infoGroup.transform.Find("PlantName").GetComponent<Text>();
        Text reqResources = infoGroup.transform.Find("ReqResources").Find("Text").GetComponent<Text>();
        Text addResources = infoGroup.transform.Find("AddResources").Find("Text").GetComponent<Text>();

        plantName.text = plant.name;
        reqResources.text = "";
        addResources.text = "";
        foreach (graveResource res in requiredResources)
        {
            string nameString = res.name;
            if (res.known == false)
                nameString = "Unknown";
            reqResources.text += (nameString + ": " + res.value + "\n");
        }
        foreach (graveResource res in currentResources)
        {
            string nameString = res.name;
            addResources.text += (nameString + ": " + res.value + "\n");
        }
    }

    public void UpdateResourcePanel()
    {
        m_selectedTotal = 0;
        foreach (Transform child in resourcePanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (resourceManager.Resource resource in resourceManager.resources)
        {
            if (resource.value > 0 && resource.name != "Teeth")
            {
                GameObject r = Instantiate(resourceGiver, resourcePanel.transform);
                r.name = resource.name;
                r.GetComponent<ResourceGiver>().graveController = this;
                r.transform.GetChild(0).GetComponent<Image>().sprite = resourceManager.images.Find(x => x.name == resource.name).img;
                resourceGivers.Add(r);
            }
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
