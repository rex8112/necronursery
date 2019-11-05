using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;



    // Start is called before the first frame update
    void Start()
    {
        currentCameraIndex = 0;

        //turn all cameras off but the first one
        for(int i =1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        //if any cameras added, enable first one

        if(cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera is now activated");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
                //Debug.Log("Camera with name: " + cameras[currentCameraIndex].camera.name + ", is now enabled");
            }
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                //Debug.Log("Camera with name: " + cameras[currentCameraIndex].camera.name + ", is now enabled");
            }
        }

    }
}
