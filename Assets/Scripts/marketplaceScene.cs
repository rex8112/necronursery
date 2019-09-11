using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class marketplaceScene : MonoBehaviour
{
    [SerializeField] List<GameObject> resourceUIs = new List<GameObject>();
    [SerializeField] resourceManager resources;
    public UnityEvent purchase;

    // Start is called before the first frame update
    void Start()
    {
        if (purchase == null)
            purchase = new UnityEvent();

        purchase.AddListener(updateUI);
        purchase.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateUI()
    {
        foreach (GameObject res in resourceUIs)
        {
            resourceManager.Resource resource = resources.resources.Find(name => name.name == res.name);
            res.GetComponentInChildren<Text>().text = resource.value.ToString();
        }
    }
}
