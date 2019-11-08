using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public bool move = false;
    [SerializeField] sceneController sc;
    [SerializeField] Camera menuCam;
    [SerializeField] Camera mainCam;
    [SerializeField] float tranSpeed = 6f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void moveCam(bool status)
    { move = status; }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            menuCam.transform.position = Vector2.MoveTowards(menuCam.transform.position, mainCam.transform.position, tranSpeed * Time.deltaTime);
            menuCam.transform.position = new Vector3(menuCam.transform.position.x, menuCam.transform.position.y, mainCam.transform.position.z);
            if (menuCam.transform.position == mainCam.transform.position)
            {
                move = false;
                menuCam.gameObject.SetActive(false);
                sc.ChangeAudio();
            }
        }

    }
}
