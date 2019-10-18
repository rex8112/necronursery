using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "_DontDestroyOnLoad")
        {
            SceneManager.LoadScene(activeScene.buildIndex + 1);
        }
    }
}
