using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populateScrollview : MonoBehaviour
{
    [SerializeField] plantManager plantManager;
    [SerializeField] GameObject content;
    [SerializeField] GameObject buttonPrefab;

    public float buffer = 5;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (plantManager.plant plant in plantManager.plants)
        {
            GameObject button = Instantiate(buttonPrefab, content.transform);
            RectTransform rt = button.transform.GetComponent<RectTransform>();
            float buttonHeight = rt.localScale.y * rt.sizeDelta.y;
            float buttonWidthPosition = (rt.localScale.x * rt.sizeDelta.x);
            rt.localPosition = new Vector2(buttonWidthPosition, -buttonHeight - buffer);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
