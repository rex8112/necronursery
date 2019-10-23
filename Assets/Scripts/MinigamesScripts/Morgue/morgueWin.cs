using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueWin : MonoBehaviour
{
    GameObject movePosition; //The new postion of the wrong gameobject
    public List<SpriteRenderer> wantedPartsList; //List of parts the player needs to find
    Sprite objectCheck; //the object that is being checked
    [SerializeField] int currentAmount; //the current correct amount of parts the player has found
    public int winAmount; // the amount of parts the player needs to find
    float randomX; //The random X location of a body part
    float randomY; //The random Y location of a body part
    Vector3 newLocation; //The new location of the wrong gameobject

    void Start()
    {
        currentAmount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision) //Checks what touches it
    {
        objectCheck = collision.transform.gameObject.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < wantedPartsList.Count; i++)
        {
            if (objectCheck == wantedPartsList[i].sprite) // game is getting mad here
            {
                ++currentAmount;
                CheckForWin();
                Destroy(collision.gameObject);
                wantedPartsList.RemoveAt(i);
                objectCheck = null;
            }
            else
            {
                RandomLocation();
                newLocation = new Vector2(randomX, randomY);
                movePosition = collision.gameObject;
                movePosition.transform.position = newLocation;
            }
        }
    }


    //-------------------This breaks it--------------------
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (objectCheck != null)
        {
            movePosition.transform.position = newLocation;
            objectCheck = null;
        }
        else
        {
            return;
        }
    }

    void CheckForWin()
    {
        if (currentAmount == winAmount)
        {
            GameObject.Find("Main Camera").GetComponent<gameTimer>().resourceAmount = winAmount;
            GameObject.Find("Main Camera").GetComponent<gameTimer>().resourceName = "Flesh";
            GameObject.Find("Main Camera").GetComponent<gameTimer>().win = true;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<gameTimer>().win = false;
        }
    }

    void RandomLocation()
    {
        randomY = Random.Range(-3.0f, 1.5f);
        randomX = Random.Range(-5.0f, 5.0f);
    }

}
