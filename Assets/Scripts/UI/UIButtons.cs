using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField] resourceManager rm;
    string openCanvas = "Open";
    string closeButton = "Close";
    string graveyardScene = "Graveyard";
    string startTimer = "Start";
    string morgueMinigame = "Morgue";
    string marketPlace = "MarketPlace";

    bool timer;

    private void Start()
    {
        //canvas.SetActive(false); //Deactivated to prevent the necessity of clicking Menu twice
    }
    public void OnMouseDown(string buttonName)
    {
        if (buttonName == openCanvas && canvas.activeInHierarchy == false)
        {
            canvas.SetActive(true);
        }
        if (buttonName == closeButton && canvas.activeInHierarchy == true)
        {
            canvas.SetActive(false);
        }
        if (buttonName == graveyardScene)
        {
            Save();
            SceneManager.LoadScene("Graveyard");
        }
        if (buttonName == startTimer)
        {
            timer = GameObject.Find("Main Camera").GetComponent<gameTimer>().startTimer = true;
            gameObject.SetActive(false);
        }
        if (buttonName == morgueMinigame)
        {
            Save();
            SceneManager.LoadScene("Morgue");
        }
        if (buttonName == marketPlace)
        {
            Save();
            SceneManager.LoadScene("MarketPlace");
        }
    }

    public void Activate(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Deactivate(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void StartTimer(gameTimer timer)
    {
        timer.startTimer = true;
    }

    public void ChangeScene(string name)
    {
        Save();
        SceneManager.LoadScene(name);
    }

    void Save()
    {
        if (SceneManager.GetActiveScene().name == "Graveyard")
        {
            gameObject.GetComponent<sceneController>().Save();
        }
        else
        {
            //I NEED THIS TO FIND THE SAVELOAD SCRIPTABLEOBJECT IN ALL INSTANCES OF THE SCRIPT
        }
    }
}
//thisscriptishell