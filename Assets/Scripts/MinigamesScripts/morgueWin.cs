using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueWin : MonoBehaviour
{
    public List<SpriteRenderer> wantedPartsList;
    public int winAmount;
    int currentAmount;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer objectCheck = collision.gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < wantedPartsList.Count; i++)
        {
            if ( objectCheck = wantedPartsList[i])
            {
                currentAmount++;
                CheckForWin();
                Destroy(objectCheck.gameObject);
            }
        }
    }

    void CheckForWin()
    {
        if (currentAmount == winAmount)
            GameObject.Find("Main Camera").GetComponent<morgueController>().Win = true;
        else
            GameObject.Find("Main Camera").GetComponent<morgueController>().Win = false;
    }
}
