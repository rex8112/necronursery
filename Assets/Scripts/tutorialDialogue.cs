using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tutorialDialogue : MonoBehaviour
{
    public GameObject tutBox;
    public Text tutText;

    public bool textActive;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(textActive && Input.GetKeyDown(KeyCode.Space))
        {
            tutBox.SetActive(false);
            textActive = false;

        }
    }

    public void ShowBox(string dialogue)
    {
        textActive = true;
        tutBox.SetActive(true);
        tutText.text = dialogue;


    }
}
