using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueWin : MonoBehaviour
{
    public List<SpriteRenderer> wantedPartsList;
    public int winAmount;
    [SerializeField]
    int currentAmount;

    void Start()
    {
        currentAmount = 0;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer objectCheck = collision.gameObject.GetComponent<SpriteRenderer>();
        Debug.Log("Got the object");
        for (int i = 0; i < wantedPartsList.Count; i++)
        {
            if (objectCheck = wantedPartsList[i])
            {
                currentAmount++;
                Debug.Log("increase Current amount");
                CheckForWin();
                Destroy(objectCheck.gameObject);
                Debug.Log("Object has been destory");
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    SpriteRenderer objectCheck = collision.gameObject.GetComponent<SpriteRenderer>();
    //    for (int i = 0; i < wantedPartsList.Count; i++)
    //    {
    //        if (objectCheck = wantedPartsList[i])
    //        {
    //            currentAmount++;
    //            CheckForWin();
    //            Destroy(objectCheck.gameObject);
    //        }
    //    }
    //}

    void CheckForWin()
    {
        if (currentAmount == winAmount + 1)
        {
            GameObject.Find("Main Camera").GetComponent<morgueController>().Win = true;
            Debug.Log("You won");
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<morgueController>().Win = false;
            Debug.Log("haven't won yet");
        }
    }
}
