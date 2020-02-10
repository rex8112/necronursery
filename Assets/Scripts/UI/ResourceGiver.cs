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

    public void Increase(int amt)
    {
        Value += amt;
    }

    public void Decrease(int amt)
    {
        Value -= amt;
    }
}
