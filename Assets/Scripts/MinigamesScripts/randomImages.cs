using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomImages : MonoBehaviour
{
    public List<Sprite> images;
    SpriteRenderer sr;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        showRandomImage();
    }

    void showRandomImage()
    {
        int count = images.Count;
        int index = Random.Range(0, count);
        sr.sprite = images[index];
    }


}
