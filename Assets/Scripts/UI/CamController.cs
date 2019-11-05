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
        //currentCameraIndex = 0;

        ////turn all cameras off but the first one
        //for(int i =1; i < cameras.Length; i++)
        //{
        //    cameras[i].gameObject.SetActive(false);
        //}

        ////if any cameras added, enable first one

        //if(cameras.Length > 0)
        //{
        //    cameras[0].gameObject.SetActive(true);
        //    Debug.Log("Camera is now activated");
        //}

        
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
