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
    public void OnMouseDown()
    {
 //       GameObject.Find("Main Camera").GetComponent<MastermindController>().CodeChecker(true);
        triesLeft -= 1;
        onScreenNumber.text = (triesLeft).ToString("0");
    }


}
