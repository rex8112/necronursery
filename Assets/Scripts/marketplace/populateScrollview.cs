using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class populateScrollview : MonoBehaviour
{
    [SerializeField] plantManager plantManager;
    [SerializeField] graveController gc;
    public GameObject content;
    [SerializeField] GameObject buttonPrefab;

    public float buffer = 5;

    void Awake()
    {
        gc = transform.GetComponentInParent<graveController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (plantManager.plant plant in plantManager.plants)
        {
            count += 1;
            GameObject button = Instantiate(buttonPrefab, content.transform);
            Text text = button.GetComponentInChildren<Text>();
            text.text = plant.name;

            button.GetComponent<Button>().onClick.AddListener(delegate{gc.plant(plant.name);});
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
