using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueController : MonoBehaviour
{
    public GameObject bodyPart;
    public GameObject trash;
    public int bodyPartCount = 6;
    public int trashCount = 10;

    void Start()
    {
        for (int i = 0; i < bodyPartCount; i++)
        {
            Instantiate(bodyPart, new Vector3(0, 0, 0), Quaternion.identity);
        }

        for (int i = 0; i < trashCount; i++)
        {
            Instantiate(trash, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        
    }

    //needs to choose what body parts are need to 'win' 
    //make a list of body part to find. check list with 
    //the body parts in the scene and if one of the 
    //parts in the list is not in the scene pick a new one
    //the bag should check whaat parts are needed and if 
    //a needed part is drug on top of it then destory the object
    //and "cross" it out
}
