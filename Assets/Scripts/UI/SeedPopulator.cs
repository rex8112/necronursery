using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeedPopulator : MonoBehaviour
{
    [SerializeField] resourceManager rm;
    [SerializeField] GameObject prefab;

    private void Awake()
    {
        Refresh();
    }
    public void Populate()
    {
        foreach (resourceManager.Seed res in rm.seeds)
        {
            GameObject r = Instantiate(prefab, transform);
            r.transform.GetChild(0).name = res.name;
            r.GetComponentInChildren<SeedUpdater>().ChangeValue();
        }
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Refresh()
    {
        Clear();
        Populate();
    }
}
