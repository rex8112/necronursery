using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morgueController : MonoBehaviour
{
    public GameObject bodyPart; //bodyPart Prefab
    public GameObject trash; //Trash Prefab
    public GameObject[] allParts; //An array of all the bodyparts in the scene
    public List<SpriteRenderer> wantedParts; //List of the parts the player needs to find
    SpriteRenderer spriteSwitch; //The sprite that is getting set (for what the player needs to find)
    SpriteRenderer chosenOne; //Holds the sprite before the spriteSwitch
    public int bodyPartCount = 6; //The number of bodyParts that will spawn
    public int trashCount = 10; //How many trash will spawn
    int randomAmount; // The amount of bodyparts the player has to find (1-3)
    int partNumber; //Which partLocation in allParts that the player needs to find
    float randomY; //The random Y location of a body part
    float randomX; //The random X location of a body part
    int test = 0;

    void Awake()
    {
        if(test >= 1)
        ResetMinigame(true);
    }

    void Start()
    {
        randomAmount = 0;
        partNumber = 0;
        RandomLocation();
        MakeGameObjects();
        allParts = GameObject.FindGameObjectsWithTag("bodypart");
        WantedBodyParts();
    }

    void WantedBodyParts() //Makes a list of bodyParts that the player needs to find
    {
        randomAmount = Random.Range(1, 3);
        for (int r = 0; r < randomAmount; r++)
        {
            partNumber = Random.Range(0, allParts.Length);

            if (allParts[partNumber] == null) //if the bodypart[partnumber] has already been used pick another
            {
                partNumber = Random.Range(0, allParts.Length);
            }

            else //else add the bodyPart[partNumber] to the list
            {
                wantedParts.Add(allParts[partNumber].GetComponent<SpriteRenderer>());
                chosenOne = wantedParts[r];
                spriteSwitch = GameObject.Find("Object" + r).GetComponent<SpriteRenderer>();
                spriteSwitch.sprite = chosenOne.sprite;
                allParts[partNumber] = null; //setting the location to NULL so it won't be picked twice
            }
        }
        GameObject.Find("Bag").GetComponent<morgueWin>().wantedPartsList = wantedParts;
        GameObject.Find("Bag").GetComponent<morgueWin>().winAmount = randomAmount;
    }

    void MakeGameObjects() //Create gameobjects in the scene
    {
        for (int i = 0; i < bodyPartCount; i++) //Make bodyparts
        {
            Instantiate(bodyPart, new Vector3(randomX, randomY, 0), Quaternion.identity);
            RandomLocation();
        }

        for (int j = 0; j < trashCount; j++) //make trash
        {
            Instantiate(trash, new Vector3(randomX, randomY, 0), Quaternion.identity);
            RandomLocation();
        }

    }

    void RandomLocation() //pick a random location
    {
            randomY = Random.Range(-3.0f, 1.5f);
            randomX = Random.Range(-5.0f, 5.0f);
    }

    public void ResetMinigame(bool reset)
    {
        if (reset)
        {
            System.Array.Clear(allParts, 0, allParts.Length); //Clear the array and list then remake them
            wantedParts.Clear();
            allParts = GameObject.FindGameObjectsWithTag("bodypart");
            WantedBodyParts();
        }
    }
}

