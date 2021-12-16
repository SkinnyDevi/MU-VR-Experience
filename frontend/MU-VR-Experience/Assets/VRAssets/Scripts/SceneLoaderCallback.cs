using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    bool _firstUpdateOcurred = true;

    void Update()
    {
        if (_firstUpdateOcurred)
        {
            _firstUpdateOcurred = false;
            SceneLoader.LoaderCallback();
        }
    }
}
