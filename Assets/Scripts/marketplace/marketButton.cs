using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class marketButton : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] string resource;
    [SerializeField] bool sell;
    [SerializeField] int minCount = 1;
    [SerializeField] int maxCount = 10;
    [SerializeField] bool randomResource;
    [SerializeField] List<string> randomResources;

    [Header("Scripts")]
    [SerializeField] Events ev;
    [SerializeField] resourceManager resourceManager;

    [Header("Debug Variables")]
    [SerializeField] resourceManager.Resource sellResource;
    [SerializeField] int sellCount;
    [SerializeField] resourceManager.Resource buyResource;
    [SerializeField] int buyCount;

    Text buttonText;

    private void Awake()
    {
        buttonText = gameObject.transform.GetComponentInChildren<Text>();
        initializeButton();
    }
    void Start()
    {

    }

    public resourceManager.Resource randomize()
    {
        string res;
        resourceManager.Resource ran;
        int max = randomResources.Count;
        res = randomResources[Random.Range(0, max)];
        ran = resourceManager.resources.Find(e => e.name == res);

        return ran;
    }

    public void initializeButton()
    {
        if (randomResource == true)
        {
            if (sell == true)
            {
                sellResource = randomize();
                sellCount = Random.Range(minCount, maxCount + 1);

                buyResource = resourceManager.resources.Find(e => e.name == "Teeth");
                buyCount = sellCount * sellResource.teethValue;
            }
            else
            {
                buyResource = randomize();
                buyCount = Random.Range(minCount, maxCount + 1);

                sellResource = resourceManager.resources.Find(e => e.name == "Teeth");
                sellCount = buyCount * buyResource.teethValue;
            }
        }
        else
        {
            if (sell == true)
            {
                buyResource = resourceManager.resources.Find(e => e.name == "Teeth");
                buyCount = sellCount * buyResource.teethValue;

                sellResource = resourceManager.resources.Find(e => e.name == resource);
                sellCount = Random.Range(minCount, maxCount + 1);
            }
            else
            {
                buyResource = resourceManager.resources.Find(e => e.name == resource);
                buyCount = Random.Range(minCount, maxCount + 1);

                sellResource = resourceManager.resources.Find(e => e.name == "Teeth");
                sellCount = buyCount * buyResource.teethValue;
            }
        }

        buttonText.text = "Trade " + sellCount + " " + sellResource.name + " for " + buyCount + " " + buyResource.name;

    }

    public void purchase()
    {
        if (sellResource.Remove(sellCount))
            buyResource.Add(buyCount);

        ev.OnValueChange.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
