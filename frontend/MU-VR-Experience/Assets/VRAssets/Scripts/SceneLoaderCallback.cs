using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    private bool firstUpdateOcurred = true;

    void Update()
    {
        if (firstUpdateOcurred)
        {
            firstUpdateOcurred = false;
            SceneLoader.LoaderCallback();
        }
    }
}
