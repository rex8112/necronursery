using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class marketButton : MonoBehaviour
{

    [SerializeField] resourceManager resourceManager;
    public resourceManager.Resource sellResource;
    public int sellCount;
    public resourceManager.Resource buyResource;
    public int buyCount;
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
        resourceManager.Resource ran;
        int max = resourceManager.resources.Count;
        ran = resourceManager.resources[Random.Range(0, max)];

        return ran;
    }

    public void initializeButton()
    {
        buyResource = randomize();

        if (buyResource.name == "Teeth") // If resource to buy is teeth, thus selling material for teeth.
        {
            do
            {
                sellResource = randomize();
            } while (sellResource.name == "Teeth");

            buyCount = sellResource.teethValue;
            sellCount = Random.Range(1, 10);
            buyCount = sellCount * buyCount;
        }
        else // Else buy material with teeth
        {
            sellResource = resourceManager.resources.Find(name => name.name == "Teeth");
            sellCount = buyResource.teethValue;
            buyCount = Random.Range(1, 10);
            sellCount = buyCount * sellCount;
        }

        buttonText.text = "Trade " + sellCount + " " + sellResource.name + " for " + buyCount + " " + buyResource.name;

    }

    public void purchase()
    {
        if (sellResource.Remove(sellCount))
            buyResource.Add(buyCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
