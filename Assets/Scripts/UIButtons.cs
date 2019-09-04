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

    private void Start()
    {
        canvas.SetActive(false);
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
    }
}
