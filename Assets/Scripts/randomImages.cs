using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomImages : MonoBehaviour
{
    //public List<Object> images;
    SpriteRenderer sr;
    int count = 5;
    int index;
    Sprite[] images = new Sprite[5];

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        fillListOfSprites();
        showRandomImage();
    }

    void showRandomImage()
    {
        index = Random.Range(0, count);
        sr.sprite = images[index];
    }

    void fillListOfSprites()
    {
        if(images)
        images = Resources.Load<Sprite>("Sprites");
    }

}
