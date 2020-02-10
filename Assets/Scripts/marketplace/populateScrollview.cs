using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populateScrollview : MonoBehaviour
{
    [SerializeField] resourceManager resourceManager;
    [SerializeField] graveController gc;
    [SerializeField] SaveLoad saveLoad;
    public GameObject content;
    [SerializeField] GameObject buttonPrefab;

    public float buffer = 5;

    // Start is called before the first frame update
    public void UpdateScrollview()
    {
        int count = 0;
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (resourceManager.Seed seed in resourceManager.seeds)
        {
            if (seed.value > 0)
            {
                count += 1;
                GameObject button = Instantiate(buttonPrefab, content.transform);
                Text text = button.GetComponentInChildren<Text>();
                text.text = seed.name + "   Level: " + seed.level.ToString();

                button.GetComponent<Button>().onClick.AddListener(delegate { gc.Plant(seed.plantName, 1); });
                if (seed.level > saveLoad.level)
                    button.GetComponent<Button>().interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
