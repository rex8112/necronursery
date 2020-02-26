using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGiver : MonoBehaviour
{
    [SerializeField]
    public int Value
    { 
        get { return m_value; }
        set { 
            m_value = value;
            valueText.text = value.ToString();
        }
    }
    [SerializeField] int m_value = 0;
    public GameObject decrease;
    public GameObject increase;
    public Text valueText;
    public resourceManager resourceManager;
    [HideInInspector]
    public graveController graveController;

    public void Increase(int amt)
    {
        int futureValue = m_value + amt;
        if (graveController.SelectedTotal + amt <= graveController.RequiredTotal - graveController.CurrentTotal
            && futureValue <= resourceManager.resources.Find(x => x.name == gameObject.name).value)
        {
            graveController.SelectedTotal += amt;
            Value += amt;
        }
    }

    public void Decrease(int amt)
    {
        int futureValue = m_value - amt;
        if (futureValue >= 0)
        {
            graveController.SelectedTotal -= amt;
            Value -= amt;
        }
    }
}
