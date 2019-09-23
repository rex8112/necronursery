using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updatePlantInfo : MonoBehaviour
{

    [SerializeField] resourceManager resourceManager;
    [SerializeField] plantManager plantManager;

    public Button giveResource;

    // Start is called before the first frame update
    void Start()
    {
        


        Button btn = giveResource.GetComponent<Button>();
        btn.onClick.AddListener(inventoryChanges);

    }

    public void inventoryChanges()
    {


        //if(resourceManager.Resource(gameObject.GetComponent<string>) == )
        {

        }


    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
