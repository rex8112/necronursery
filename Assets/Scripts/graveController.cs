using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graveController : MonoBehaviour
{
    [SerializeField]
    GameObject resourceInformation;
    public void activate()
    {
        resourceInformation.SetActive(true);
    }

    public void deactivate()
    {
        resourceInformation.SetActive(false);
    }
}
