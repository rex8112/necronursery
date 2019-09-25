using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public Vector3 touchStart;
    public Vector3 touchDir;

    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Ended;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = Camera.main.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Moved:
                    touchDir = Camera.main.ScreenToWorldPoint(touch.position) - touchStart;
                    touchDir.z = 0f;
                    moveCamera(touchDir);
                    break;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hit.collider.CompareTag("grave"))
            {
                hit.collider.GetComponent<graveController>().activateRI();
            }
        }
    }

    void moveCamera(Vector2 dir)
    {
        Transform cam = Camera.main.transform;

        Debug.Log(dir);
        cam.Translate(-dir);
    }
}
