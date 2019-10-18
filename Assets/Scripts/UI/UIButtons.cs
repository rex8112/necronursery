using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
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
            SceneManager.LoadScene("Graveyard");
        }
        if (buttonName == startTimer)
        {
            timer = GameObject.Find("Main Camera").GetComponent<gameTimer>().startTimer = true;
            gameObject.SetActive(false);
        }
        if (buttonName == morgueMinigame)
        {
            SceneManager.LoadScene("Morgue");
        }
        if (buttonName == marketPlace)
        {
            SceneManager.LoadScene("MarketPlace");
        }
    }
}
