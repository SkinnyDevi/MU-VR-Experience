using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SceneLoader
{
    public enum Scene {MainHub, Loading, TheatreBillboard}

    static Action onLoaderCallback;

    public static void LoadScene(Scene scene)
    {
        string userTkn = GameObject.FindObjectOfType<UserDataReceiver>().GetToken();

        if (!String.IsNullOrEmpty(userTkn))
        {
            onLoaderCallback = () => {
                SceneManager.LoadScene(scene.ToString());
            };

            SceneManager.LoadScene(Scene.Loading.ToString());
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
