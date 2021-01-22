using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadFirsScene",2f);
    }

    void LoadFirsScene()
    {
        SceneManager.LoadScene(1);
    }
}
