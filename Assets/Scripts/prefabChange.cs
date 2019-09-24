using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prefabChange : MonoBehaviour
{

    public Button test;
    public Sprite second, second1;

    //public GameObject plant;


    // Start is called before the first frame update
    void Start()
    {
        
        //changeSkin(second);
        Button btn = test.GetComponent<Button>();
        btn.onClick.AddListener(changeImage);
    }
  
    public void changeImage()
    {
        if(gameObject.tag == "spirit")
        {
            gameObject.GetComponent<Image>().sprite = second;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = second1;
        }
    }
    


    // Update is called once per frame
    void Update()
    {

        
    }

    /*public void changeSkin(Sprite sprite)
    {
        plant.GetComponent<SpriteRenderer>().sprite = sprite;
    }*/
}
