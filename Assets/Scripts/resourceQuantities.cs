using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceQuantities : MonoBehaviour
{
    // Start is called before the first frame update

    public Text fleshCountBar;
    public Text moneyCount;
    public int moneyTotal = 5;
    public int fleshCount = 0;
    public int fleshCap = 10;
public void fleshManagement()
    {
        //change = 2;
        //fleshCount += change;
        Debug.Log("HEY");

    }
    void Start()
    {
        fleshCount = 1;
        //fleshCountBar = gameObject.GetComponent<Text>();
        fleshCountBar.text = fleshCount.ToString() + "/" + fleshCap.ToString();
        moneyCount.text = moneyTotal.ToString();
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
