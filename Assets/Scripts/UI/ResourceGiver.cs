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
    [HideInInspector]
    public graveController graveController;

    public void Increase(int amt)
    {
        if (graveController.SelectedTotal < graveController.RequiredTotal - graveController.CurrentTotal)
        {
            graveController.SelectedTotal += amt;
            Value += amt;
        }
    }

    public void Decrease(int amt)
    {
        if (m_value > 0)
        {
            graveController.SelectedTotal -= amt;
            Value -= amt;
        }
    }
}
