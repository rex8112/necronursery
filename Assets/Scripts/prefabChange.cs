using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prefabChange : MonoBehaviour
{

    public Button test;
    public Sprite second;


    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<Button>();

        
    }
    public void changeImage()
    {
        gameObject.GetComponent<Image>().sprite = second;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
