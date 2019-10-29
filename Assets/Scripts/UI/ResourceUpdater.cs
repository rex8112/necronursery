using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUpdater : MonoBehaviour
{
    [SerializeField] resourceManager RM;
    [SerializeField] bool showName;
    bool sprite = false;

    private void Awake()
    {
        GameObject.Find("sceneController").GetComponent<Events>().OnValueChange.AddListener(ChangeValue);
    }

    public void ChangeValue()
    {
        string n = gameObject.name;
        Text t = gameObject.GetComponent<Text>();
        resourceManager.Resource r = RM.resources.Find(e => e.name == n);
        if (showName == true)
            t.text = r.name + ": " + r.value;
        else
            t.text = r.value.ToString();

        if (sprite == false)
        {
            Sprite s = RM.images.Find(e => e.name == n).img;
            Image g = transform.parent.GetComponent<Image>();
            if (g == null)
                g = transform.parent.GetComponentInChildren<Image>();
            g.sprite = s;
            sprite = true;
        }
    }
}
