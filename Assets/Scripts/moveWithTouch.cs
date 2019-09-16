using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveWithTouch : MonoBehaviour
{
    float deltaX, deltaY;// touch offset allows ball not to shake when it starts moving
    //Rigidbody2D rb;
    bool moveAllowed = false;

    // Use this for initialization
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)// if touch event takes place
        {
            Touch touch = Input.GetTouch(0);// get touch position
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);// obtain touch position

            switch (touch.phase)
            {
                case TouchPhase.Began:// if you touch the screen
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))// if you touch the gameobject
                    {
                        // get the offset between position you touch and the center of the game object
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;

                        // if touch begins within the collider then it is allowed to move 
                        moveAllowed = true;

                        // restrict some rigidbody properties so it moves more smoothly
                        //rb.freezeRotation = true;
                        //rb.velocity = new Vector2(0, 0);
                    }
                    break;

                case TouchPhase.Moved://you move your finger    
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && moveAllowed)// if you touch the  and movement is allowed then move
                        transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
                    break;

                case TouchPhase.Ended: // you release your finger

                    // restore initial parameters
                    moveAllowed = false;
                    //rb.freezeRotation = false;
                    //rb.gravityScale = 2;
                    break;
            }
        }
    }
}
