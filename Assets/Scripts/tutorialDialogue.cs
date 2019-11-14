using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tutorialDialogue : MonoBehaviour
{
    public GameObject tutBox;
    public Text tutText;

    public bool textActive;
    int stepCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        ShowBox(" Hello there young one. Planning to raise the dead are you? Good to see such spirits from today’s youth. Hehehe.");
    }

    // Update is called once per frame
    void Update()
    {
        if (textActive && Input.GetKeyDown(KeyCode.Space))
        {
            nextDialogue();
        }
    }

    public void ShowBox(string dialogue)
    {
        textActive = true;
        tutBox.SetActive(true);
        tutText.text = dialogue;


    }

    public void nextDialogue()
    {
        stepCounter++;

            switch(stepCounter)
            {
                case 1: 
                    tutText.text = " Hello there young one. Planning to raise the dead are you? Good to see such spirits from today’s youth. Hehehe.";
                    break;
                case 2:
                    tutText.text = "Ah, but you can’t raise such spirits without some tools. Thankfully our art isn’t as complex as it once was.";
                    break;
                case 3:
                    tutText.text = "Ah, the days of ceremonies and exotic sacrifices. Today it’s much simpler, and faster, to raise the dead. Or even create monsters.";
                    break;
                case 4:
                    tutText.text = "Ah, but to raise a monster, you’ll have to plant it. While we use to rely on dead bodies, today we have handy, and easy to move, seeds instead. Here have some, as a gift.";
                    break;
                case 5:
                    tutText.text = "You’ll need some ‘sacrifices’ to raise that seed to maturity. Here is some flesh for it.";
                    break;
                case 6:
                    tutText.text = "Flesh is a common enough resource. Easily obtained, and easy to feed to a monster plant.";
                    break;
                case 7:
                    tutText.text = "With that much Flesh, you should be able to raise a zombie from this seedling.Just don’t let the little fella nip any digits off your hand.They can be a bit feisty at first.";
                    break;
                case 8:
                    Destroy(gameObject);
                    break;
            }

    }

}
