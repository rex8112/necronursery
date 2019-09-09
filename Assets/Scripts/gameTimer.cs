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
    public 

    // Update is called once per frame
    void Update()
    {
        if (!tapToStart.activeInHierarchy)
        {
            remainingTime -= Time.deltaTime;
            timer.text = (remainingTime).ToString("0");
        }

        if (remainingTime <= 0)
        {
            Time.timeScale = 0;
            afterTimer.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        tapToStart.SetActive(false);
    }

}
