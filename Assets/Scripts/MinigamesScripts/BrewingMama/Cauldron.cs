using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] BrewingController bc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ingredient"))
            bc.MatchingImages();
    }
}
