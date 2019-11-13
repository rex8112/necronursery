using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField] resourceManager rm;
    [SerializeField] SaveLoad sl;
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
            SceneManager.LoadSceneAsync("Graveyard");
        }
        if (buttonName == startTimer)
        {
            timer = GameObject.Find("Main Camera").GetComponent<gameTimer>().startTimer = true;
            gameObject.SetActive(false);
        }
        if (buttonName == morgueMinigame)
        {
            Save();
            SceneManager.LoadSceneAsync("Morgue");
        }
        if (buttonName == marketPlace)
        {
            Save();
            SceneManager.LoadSceneAsync("MarketPlace");
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
        if (sl != null)
            Save();
        SceneManager.LoadSceneAsync(name);
    }

    void Save()
    {
        if (SceneManager.GetActiveScene().name == "Graveyard")
        {
            gameObject.GetComponent<sceneController>().Save();
        }
        else
        {
            rm.resources.Clear();
            foreach (resourceManager.Resource res in rm.resources)
            {
                rm.resources.Add(res);
            }
            sl.BuildSave();
        }
    }
}
//thisscriptishell