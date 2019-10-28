using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            resourceManager.Images s = rm.images.Find(e => e.name == res.name);
            if (SceneManager.GetActiveScene().name == "Graveyard")
            {
                r.GetComponentInChildren<Image>().sprite = s.img;
                r.GetComponentInChildren<Text>().text = res.name + ": " + res.value;
            }
            else
            {
                r.GetComponent<Image>().sprite = s.img;
                r.GetComponentInChildren<Text>().text = res.value.ToString();
            }
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
            if (SceneManager.GetActiveScene().name == "Graveyard")
                child.GetComponentInChildren<Text>().text = res.name + ": " + res.value;
            else
                child.GetComponentInChildren<Text>().text = res.value.ToString();
        }
    }

    public void Refresh()
    {
        Clear();
        Populate();
    }
}
