using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePopulator : MonoBehaviour
{
    [SerializeField] resourceManager rm;
    [SerializeField] GameObject prefab;

    private void Awake()
    {
        Refresh();
    }
    public void Populate()
    {
        foreach (resourceManager.Resource res in rm.resources)
        {
            GameObject r = Instantiate(prefab, transform);
            r.GetComponentInChildren<Text>().text = res.name + ": " + res.value;
            r.name = res.name;
        }
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdateValues()
    {
        foreach (Transform child in transform)
        {
            string name = child.name;
            resourceManager.Resource res = rm.resources.Find(r => r.name == name);
            child.GetComponentInChildren<Text>().text = res.name + ": " + res.value;
        }
    }

    public void Refresh()
    {
        Clear();
        Populate();
    }
}
