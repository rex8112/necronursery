using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public Vector3 touchStart;
    public Vector3 touchDir;

    public float slowness = 10;

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
    }

    void moveCamera(Vector2 dir)
    {
        Transform cam = Camera.main.transform;

        Debug.Log(dir);
        cam.Translate(-dir/slowness);
    }
}
