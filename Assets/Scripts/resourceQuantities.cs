using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceQuantities : MonoBehaviour
{
    // Start is called before the first frame update

    public Text fleshCountBar;
    public Text moneyCount;
    public Text souls;
    public int moneyTotal = 5;
    public int fleshCount = 0;
    public int soulCount = 0;
    public int soulCap = 10;
    public int fleshCap = 10;
    public int moneyChange, fleshChange, soulChange;


    void Start()
    {
        moneyChange = 0;
        fleshChange = 0;
        //fleshCountBar = gameObject.GetComponent<Text>();
        fleshCountBar.text = fleshCount.ToString() + "/" + fleshCap.ToString();
        moneyCount.text = moneyTotal.ToString();
        souls.text = soulCount.ToString() + "/" + soulCap.ToString();
    }

    public void tradeButtonOne()
    {
        Debug.Log("money = " + moneyTotal);

        if (moneyTotal + moneyChange >= 0)
        {
            moneyChange = -3;
            fleshChange = 2; 
            fleshCount += fleshChange;
            moneyTotal += moneyChange;

            fleshCountBar.text = fleshCount.ToString() + "/" + fleshCap.ToString();
            moneyCount.text = moneyTotal.ToString();
        }
        else
        {
           Debug.Log("money = " + moneyTotal);
        }

        
        

        
    }


    public void tradeButtonTwo()
    {
        Debug.Log("money = " + moneyTotal);

        if(moneyTotal + moneyChange >= 0)
        {
            moneyChange = -5;
            soulChange = 7;
            soulCount += soulChange;
            moneyTotal += moneyChange;

            souls.text = soulCount.ToString() + "/" + soulCap.ToString();
            moneyCount.text = moneyTotal.ToString();
        }
        else
        {
            Debug.Log("money = " + moneyTotal);
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
