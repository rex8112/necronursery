using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameTimer : MonoBehaviour
{
    public float remainingTime = 10.0f;
    public Text timer;
    public GameObject afterTimer;
    public GameObject tapToStart;
    public bool startTimer = false;
    public bool win = false;
    bool winOver = false;
    public string resourceName;
    public int resourceAmmount;
    public resourceManager.Resource changeResource;
    [SerializeField] resourceManager resourceManager;

    private void Awake()
    {
        win = false;
        winOver = false;
    }


    void Update()
    {
        if (startTimer)
        {
            remainingTime -= Time.deltaTime;
            timer.text = (remainingTime).ToString("0.0");
        }
    }


    void FixedUpdate()
    {
        if (remainingTime <= 0 && win == false)
        {
            startTimer = false;
            Time.timeScale = 0;
            afterTimer.SetActive(true);
        }
        else if(win == true && winOver == false)
        {
            startTimer = false;
            Time.timeScale = 0;
            changeResource = resourceManager.resources.Find(name => name.name == resourceName);
            changeResource.Add(resourceAmmount);
            winOver = true;
        }
    }
}
