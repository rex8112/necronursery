using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{
    public int triesLeft;
    public Text onScreenNumber;

    private void Start()
    {
        triesLeft = 12;

    }
    public void TriesLeft()
    {
        if (triesLeft <= 0)
        {
            GameObject.Find("Main Camera").GetComponent<>(MastermindController).
        }
        triesLeft--;
        onScreenNumber.text = (triesLeft).ToString("00");
    }


}
