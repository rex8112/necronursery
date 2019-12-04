using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{

    public void OnMouseDown()
    {
        GameObject.Find("Main Camera").GetComponent<MastermindController>().CodeChecker(true);
    }


}
