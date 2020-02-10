using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] SaveLoad save;
    public void UpdateText()
    {
        gameObject.GetComponent<Slider>().maxValue = save.xpToLevel;
        gameObject.GetComponent<Slider>().value = save.xp;
        gameObject.GetComponentInChildren<Text>().text = save.xp.ToString() + "/" + save.xpToLevel.ToString();
    }
}
