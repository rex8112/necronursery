using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] SaveLoad SaveLoad;
    [SerializeField] resourceManager resourceManager;

    [Header("Camera Movement")]
    public Vector3 touchStart;
    public Vector3 touchDir;
    public bool lockCamera;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera camToMoveTo;
    float initSize;
    [SerializeField] float tranSpeed = 6f;
    [SerializeField] float sizeSpeed = 1f;
    [SerializeField] GameObject AudioController;
    [SerializeField] GameObject tap;

    [Space(10)]
    [SerializeField] List<graveController> graves = new List<graveController>();

    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Ended;

    private void Awake()
    {
        initSize = Camera.main.orthographicSize;
        mainCam = Camera.main;
    }

    private void Start()
    {
        Load();
        if (SaveLoad.tapToStart == false)
        {
            tap.SetActive(false);
            gameObject.GetComponent<CamController>().HardTransition();
        }
        else
            SaveLoad.tapToStart = false;
    }

    private void OnApplicationQuit()
    {
        Save();
        SaveLoad.tapToStart = true;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            Save();
        }
        else
        {
            //Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) //Drag to move camera
        {
            Touch touch = Input.GetTouch(0);

            if (!lockCamera)
            {
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

        //Debug.Log(dir);
        cam.Translate(-dir);
    }

    public void ChangeAudio()
    {
        AudioSource[] audio = AudioController.GetComponents<AudioSource>();
        audio[1].Stop();
        audio[0].Play();
    }

    public void Save() //Load important resources into the SaveLoad object and save
    {
        SaveLoad.plants.Clear(); //Clears the current save info to be repopulated with the new info
        SaveLoad.resources.Clear();
        SaveLoad.seeds.Clear();
        SaveLoad.stageInts.Clear();
        SaveLoad.gResources.Clear();
        foreach (graveController grave in graves) //Saves all the graves
        {
            List<graveController.graveResource> gResources = new List<graveController.graveResource>();
            SaveLoad.plants.Add(grave.plant.name);
            SaveLoad.stageInts.Add(grave.stage);
            foreach (graveController.graveResource res in grave.requiredResources)
            {
                gResources.Add(res);
            }
            SaveLoad.gResources.Add(gResources);
        }
        foreach (resourceManager.Resource res in resourceManager.resources) //Saves all the resources
        {
            SaveLoad.resources.Add(res);
        }
        foreach (resourceManager.Seed seed in resourceManager.seeds)
        {
            SaveLoad.seeds.Add(seed);
        }

        Debug.Log("Preparing Save");
        SaveLoad.BuildSave();


        GetComponent<Events>().OnSave.Invoke();
    }

    public void Load() //Loads the save file
    {
        if (SaveLoad.UnbuildSave())
        {
            for (int i = 0; i < SaveLoad.stageInts.Count; i++)
            {
                if (SaveLoad.stageInts[i] > 0) //Checks if the stage exists, allows remembering grave positions
                {
                    graves[i].Plant(SaveLoad.plants[i], 0); //Passes the name of the plant and lets graveController handle getting the further details

                    if (SaveLoad.stageInts[i] > 1) //If stage is larger than 1, then update grave to that point.
                    {
                        graves[i].stage = SaveLoad.stageInts[i] - 1; //Due to how nextStage() works, we have to remove one from the current stage to get to the intended one
                        graves[i].nextStage();
                    }
                    graves[i].LoadResources(SaveLoad.gResources[i]);
                }
            }
            if (SaveLoad.resources.Count > 0)
            {
                foreach (resourceManager.Resource res in SaveLoad.resources) //Fills the resources back in, was the simplest way to do it that I could think of
                {
                    resourceManager.Resource r = resourceManager.resources.Find(resource => resource.name == res.name);
                    if (r != null)
                        r.value = res.value;
                }
            }
            if (SaveLoad.resources.Count > 0)
            {
                foreach (resourceManager.Seed seed in SaveLoad.seeds)
                {
                    resourceManager.Seed s = resourceManager.seeds.Find(se => se.name == seed.name);
                    if (s != null)
                        s.value = seed.value;
                }
            }
        }
        else
        {
            foreach (resourceManager.Resource res in resourceManager.resources)
            {
                res.value = res.defaultValue;
            }
            foreach (resourceManager.Seed seed in resourceManager.seeds)
            {
                seed.value = seed.defaultValue;
            }
        }

        GetComponent<Events>().OnLoad.Invoke();
    }

    public void Delete()
    {
        SaveLoad.DeleteSave();
        foreach (resourceManager.Resource res in resourceManager.resources)
        {
            res.value = res.defaultValue;
        }
        foreach (resourceManager.Seed seed in resourceManager.seeds)
        {
            seed.value = seed.defaultValue;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
