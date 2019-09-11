using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        test.GetComponent<Image>().sprite = second;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
