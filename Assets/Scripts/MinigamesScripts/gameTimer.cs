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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTimer)
        {
            remainingTime -= Time.deltaTime;
            timer.text = (remainingTime).ToString("0");
        }

        if (remainingTime <= 0)
        {
            startTimer = false;
            //Time.timeScale = 0;
            afterTimer.SetActive(true);
        }
    }
}
