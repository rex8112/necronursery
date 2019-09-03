using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    GameObject menuCanvas;
    string resourceMenu = "Resource Menu";
    string closeButton = "Close";
    private void Start()
    {
        menuCanvas.SetActive(false);
    }
    public void OnMouseDown(string buttonName)
    {
        if (buttonName == resourceMenu && menuCanvas.activeInHierarchy == false)
        {
            menuCanvas.SetActive(true);
        }
        if (buttonName == closeButton && menuCanvas.activeInHierarchy == true)
        {
            menuCanvas.SetActive(false);
        }
    }
}
