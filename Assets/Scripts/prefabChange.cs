using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prefabChange : MonoBehaviour
{
    public Sprite first, second, third;

    //public GameObject plant;


    // Start is called before the first frame update
    //void Awake()
    //{
    //    gameObject.GetComponent<Image>().sprite = first;
    //    stage = 1;
    //}
  
    //public void changeImage()
    //{
    //    if(gameObject.tag == "spirit")
    //    {
    //        gameObject.GetComponent<Image>().sprite = second;
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<Image>().sprite = second1;
    //    }
    //}
    
    public void nextStage(int stage)
    {
        switch (stage)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = first;
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = second;
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = third;
                break;
            default:
                break;
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
