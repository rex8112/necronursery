
    //needs to choose what body parts are need to 'win' 
    //make a list of body part to find. check list with 
    //the body parts in the scene and if one of the 
    //parts in the list is not in the scene pick a new one
    //the bag should check whaat parts are needed and if 
    //a needed part is drug on top of it then destory the object
    //and "cross" it out
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueController : MonoBehaviour
{
    public GameObject bodyPart;
    public GameObject trash;
    public int bodyPartCount = 6;
    public int trashCount = 10;
    int randomAmount;
    int partNumber;
    public GameObject[] allParts;
    public List<SpriteRenderer> wantedParts;
//    public Sprite placeHolder;

    void Start()
    {
        MakeGameObjects();
        allParts = GameObject.FindGameObjectsWithTag("bodypart");
        Debug.Log(allParts.Length);
//        wantedParts.Add(placeHolder);
        WantedBodyParts();

    }

    void Update()
    {
        
    }

        //make list of bodyparts from scene
        //pick X parts from list
    void WantedBodyParts()
    {
        randomAmount = Random.Range(1, 3);
        for(int r = 0; r <= randomAmount; r ++)
        {
            partNumber = Random.Range(0, allParts.Length);
            wantedParts.Add(allParts[partNumber].GetComponent<SpriteRenderer>());
            //wantedParts[r] = allParts[partNumber].GetComponent<SpriteRenderer>().sprite;
            Debug.Log(wantedParts.Count);
        }
    }

    void MakeGameObjects()
    {
        for (int i = 0; i < bodyPartCount; i++)
        {
            Instantiate(bodyPart, new Vector3(0, 0, 0), Quaternion.identity);
        }

        for (int j = 0; j < trashCount; j++)
        {
            Instantiate(trash, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

}

