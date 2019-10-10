using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    [SerializeField] SaveLoad SaveLoad;
    [SerializeField] resourceManager resourceManager;
    public Vector3 touchStart;
    public Vector3 touchDir;

    public bool lockCamera;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera camToMoveTo;
    float initSize;
    [SerializeField] float tranSpeed = 6f;
    [SerializeField] float sizeSpeed = 1f;
    [SerializeField] List<graveController> graves = new List<graveController>();

    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Ended;

    private void Awake()
    {
        initSize = Camera.main.orthographicSize;
        mainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) //Drag to move camera
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

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) //Check for grave touch
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hit.collider.CompareTag("grave") && lockCamera == false)
            {
                hit.collider.GetComponent<graveController>().activateRI();
                lockCamera = true;
                camToMoveTo = hit.collider.GetComponentInChildren<Camera>();
            }
        }

        if (lockCamera)
        {
            float sizeToChange = (camToMoveTo.orthographicSize - mainCam.orthographicSize) * sizeSpeed * Time.deltaTime;
            float predictSize = (mainCam.orthographicSize + sizeToChange);

            mainCam.orthographicSize += sizeToChange;

            mainCam.transform.position = Vector2.MoveTowards(mainCam.transform.position, camToMoveTo.transform.position, tranSpeed * Time.deltaTime);
            mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -10f);
        }
        else
        {
            float sizeToChange = (initSize - mainCam.orthographicSize) * sizeSpeed * Time.deltaTime;
            mainCam.orthographicSize += sizeToChange;
        }
    }

    void moveCamera(Vector2 dir)
    {
        Transform cam = Camera.main.transform;

        Debug.Log(dir);
        cam.Translate(-dir);
    }

    public void Save() //Load important resources into the SaveLoad object and save
    {
        SaveLoad.plants.Clear(); //Clears the current save info to be repopulated with the new info
        SaveLoad.resources.Clear();
        SaveLoad.stageInts.Clear();
        foreach (graveController grave in graves) //Saves all the graves
        {
            SaveLoad.plants.Add(grave.seed.name);
            SaveLoad.stageInts.Add(grave.stage);
        }
        foreach (resourceManager.Resource res in resourceManager.resources) //Saves all the resources
        {
            SaveLoad.resources.Add(res);
        }

        Debug.Log("Preparing Save");
        SaveLoad.BuildSave();
    }

    public void Load() //Loads the save file
    {
        SaveLoad.UnbuildSave();
        for (int i = 0; i < SaveLoad.stageInts.Count; i++)
        {
            if (SaveLoad.stageInts[i] > 0) //Checks if the stage exists, allows remembering grave positions
            {
                graves[i].plant(SaveLoad.plants[i]); //Passes the name of the plant and lets graveController handle getting the further details

                if (SaveLoad.stageInts[i] > 1) //If stage is larger than 1, then update grave to that point.
                {
                    graves[i].stage = SaveLoad.stageInts[i] - 1; //Due to how nextStage() works, we have to remove one from the current stage to get to the intended one
                    graves[i].nextStage();
                }
            }
        }
        resourceManager.resources.Clear(); //Resets all resources
        foreach (resourceManager.Resource res in SaveLoad.resources) //Fills the resources back in, was the simplest way to do it that I could think of
        {
            resourceManager.resources.Add(res);
        }
    }
}
