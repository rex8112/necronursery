﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastermindController : MonoBehaviour
{
    [SerializeField] private Vector3 touchPosWorld;
    [SerializeField] List<Sprite> CorrectCode;
    [SerializeField] private GameObject runes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosWorld2D;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
                    if (hit.collider.CompareTag("Runes"))
                    {
                        runes = Instantiate(hit.collider.gameObject);
                        runes.transform.position = touchPosWorld2D;
                    }
                    break;
                case TouchPhase.Moved:
                    touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    if (runes != null)
                        runes.transform.position = touchPosWorld2D;
                    break;
                case TouchPhase.Ended:
                    if (runes)
                        Destroy(runes);
                    runes = null;
                    break;
            }
        }
    }
}
