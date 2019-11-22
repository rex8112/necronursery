using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePlacement : MonoBehaviour
{

    public bool defaultImage;
    public Sprite spriteHolder;
    public Color tempcolorholder;

    // Start is called before the first frame update
    void Start()
    {
        defaultImage = true;   
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("he hit me");
        if (collision.gameObject.CompareTag("Runes"))
        {
            //spriteHolder = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            //gameObject.GetComponent<SpriteRenderer>().sprite = spriteHolder;
            tempcolorholder = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Debug.Log("Color got");
            gameObject.GetComponent<SpriteRenderer>().color = tempcolorholder;
            Debug.Log("i changed colors");
            defaultImage = false;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
