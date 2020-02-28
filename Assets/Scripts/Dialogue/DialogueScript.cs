using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public string identifier;
    [Space(5)]
    public GameObject dialogueBox;
    public GameObject characterSprite;
    public Text dialogueText;
    public GameObject buttonPanel;
    public GameObject buttonPrefab;
    [TextArea]
    public string startText;
    public List<Dialogue> allOptions = new List<Dialogue>();
    private Dialogue currentDialogue = null;

    public void Start()
    {
        if (dialogueBox.activeInHierarchy)
        {
            ShowBox();
        }
    }

    public void ShowBox()
    {
        dialogueBox.SetActive(true);
        characterSprite.SetActive(true);
        dialogueText.text = startText;
        currentDialogue = new Dialogue
        {
            response = startText,
            options = allOptions
        };
        RefreshButtons();
    }

    public void NextDialogue(string chosenOption)
    {
        if (currentDialogue == null)
        {
            currentDialogue = allOptions.Find(x => x.option == chosenOption);
        }
        else
        {
            currentDialogue = currentDialogue.options.Find(x => x.option == chosenOption);
        }

        dialogueText.text = currentDialogue.response;
        RefreshButtons();
    }

    public void RefreshButtons()
    {
        foreach (Transform child in buttonPanel.transform)
        {
            Destroy(child.gameObject);
        }

        if (currentDialogue.options.Count > 0)
        {
            foreach (Dialogue option in currentDialogue.options)
            {
                GameObject button = Instantiate(buttonPrefab, buttonPanel.transform);
                button.GetComponentInChildren<Text>().text = option.option;
                button.GetComponent<Button>().onClick.AddListener(delegate { NextDialogue(option.option); });
            }
        }
        else
        {
            GameObject button = Instantiate(buttonPrefab, buttonPanel.transform);
            button.GetComponentInChildren<Text>().text = "Close";
            button.GetComponent<Button>().onClick.AddListener(CloseDialogue);
        }
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        characterSprite.SetActive(false);
    }

    [System.Serializable]
    public class Dialogue
    {
        [TextArea]
        public string option;
        [TextArea]
        public string response;
        public List<Dialogue> options = new List<Dialogue>();
    }

}


//public void UpdateResourcePanel()
//{
//    m_selectedTotal = 0;
//    foreach (Transform child in resourcePanel.transform)
//    {
//        Destroy(child.gameObject);
//    }
//    foreach (resourceManager.Resource resource in resourceManager.resources)
//    {
//        if (resource.value > 0 && resource.name != "Teeth")
//        {
//            GameObject r = Instantiate(resourceGiver, resourcePanel.transform);
//            r.name = resource.name;
//            r.GetComponent<ResourceGiver>().graveController = this;
//            r.transform.GetChild(0).GetComponent<Image>().sprite = resourceManager.images.Find(x => x.name == resource.name).img;
//            resourceGivers.Add(r);
//        }
//    }
//}

//button.GetComponent<Button>().onClick.AddListener(delegate { gc.Plant(seed.plantName, 1); });
//Make function to replace gc.Plant that checks string to determine next option