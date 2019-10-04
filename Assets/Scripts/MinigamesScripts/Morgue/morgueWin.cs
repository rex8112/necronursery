using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueWin : MonoBehaviour
{
    public List<SpriteRenderer> wantedPartsList;
    public int winAmount;
    [SerializeField]
    int currentAmount;
    Sprite objectCheck;

    void Start()
    {
        currentAmount = 0;
    }


    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    SpriteRenderer objectCheck = collision.gameObject.GetComponent<SpriteRenderer>();
    //    Debug.Log("Got the object");
    //    for (int i = 0; i < wantedPartsList.Count; i++)
    //    {
    //        if (objectCheck = wantedPartsList[i])
    //        {
    //            currentAmount++;
    //            Debug.Log("increase Current amount");
    //            CheckForWin();
    //            Destroy(objectCheck.gameObject);
    //            Debug.Log("Object has been destory");
    //        }
    //    }
    //}
    //void OnCollider2D(Collider colided)
    //{
    //    Debug.Log("Got the object");
    //    objectCheck = gameObject.GetComponent<SpriteRenderer>().sprite;
    //    for (int i = 0; i <= wantedPartsList.Count; i++)
    //    {
    //        if (objectCheck == wantedPartsList[i].sprite)
    //        {
    //            currentAmount++;
    //            Debug.Log("increase Current amount");
    //            CheckForWin();
    //            Destroy(gameObject);
    //            Debug.Log("Object has been destory");
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectCheck = collision.transform.gameObject.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i <= wantedPartsList.Count; i++)
        {
            if (objectCheck == wantedPartsList[i].sprite)
            {
                currentAmount++;
                Debug.Log("increase Current amount");
                CheckForWin();
                Destroy(collision.gameObject);
                Debug.Log("Object has been destory");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        objectCheck = null;
    }



    void CheckForWin()
    {
        if (currentAmount == winAmount + 1)
        {
            GameObject.Find("Main Camera").GetComponent<morgueController>().Win = true;
            GameObject.Find("Main Camera").GetComponent<gameTimer>().win = true;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<morgueController>().Win = false;
            Debug.Log("haven't won yet");
        }
    }
}
