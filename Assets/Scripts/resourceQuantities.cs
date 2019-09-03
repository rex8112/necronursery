using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceQuantities : MonoBehaviour
{
    // Start is called before the first frame update

    public Text fleshCountBar;
    public int fleshCount = 0;
    public int fleshCap = 10;

    void Start()
    {
        fleshCount = 0;
        //fleshCountBar = gameObject.GetComponent<Text>();
        fleshCountBar.text = fleshCount.ToString(); //+ "/" + fleshCap.ToString(); 
    }

    public void fleshManagement(int change)
    {
        change = 2;
        fleshCount += change;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
