using UnityEngine;
using UnityEngine.SceneManagement;

using System;

public static class SceneLoader
{
    public enum Scene {MainHub, Loading, TheatreBillboard, TheatreCinema}

    static Action s_onLoaderCallback;

    public static void LoadScene(Scene scene)
    {
        string userTkn = ((UserDataReceiver)(Resources.FindObjectsOfTypeAll(typeof(UserDataReceiver))[0])).GetToken();

        if (!String.IsNullOrEmpty(userTkn))
        {
            s_onLoaderCallback = () => {
                SceneManager.LoadScene(scene.ToString());
            };

            SceneManager.LoadScene(Scene.Loading.ToString());
        }
    }

    public static void LoaderCallback()
    {
        if (s_onLoaderCallback != null)
        {
            s_onLoaderCallback();
            s_onLoaderCallback = null;
        }
    }
}
