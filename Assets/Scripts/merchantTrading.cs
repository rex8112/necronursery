using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class merchantTrading : MonoBehaviour
{
    // Start is called before the first frame update

        public GameObject help;
    resourceQuantities plsWork;



    void Start()
    {
        plsWork = help.GetComponent<resourceQuantities>();
        

    }

    public void TradeThisBitch()
    {
        plsWork.tradeButtonOne();

    }

    public void TradeNumberTwo()
    {
        plsWork.tradeButtonTwo();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
