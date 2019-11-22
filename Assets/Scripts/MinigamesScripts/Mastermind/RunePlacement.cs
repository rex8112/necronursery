using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePlacement : MonoBehaviour
{

    public bool defaultImage;
    public Sprite spriteHolder;
    public Color tempcolorholder;
    Color tempDefaultColor = Color.white;
    public Sprite defaultSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = tempDefaultColor;
        // gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        defaultImage = true;
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Runes") && Input.GetMouseButtonUp(0))
        {

            //spriteHolder = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            //gameObject.GetComponent<SpriteRenderer>().sprite = spriteHolder;
            tempcolorholder = collision.gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = tempcolorholder;
            defaultImage = false;
        }
    }
    
    void OnMouseDown()
    {
        if (!defaultImage)
        {
            gameObject.GetComponent<SpriteRenderer>().color = tempDefaultColor;
            //gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            defaultImage = true;
        }
    }

}