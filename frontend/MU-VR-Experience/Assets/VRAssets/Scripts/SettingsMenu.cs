using System.Runtime.InteropServices;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
	public GameObject SettingsMenuObject;
	public static bool InSettings;
	public GameObject Crosshair;

    void Start()
    {
		SettingsMenuObject.SetActive(false);
    }

    void Update()
    {
		if (!UserTransition.TransitionMade)
		{
			if (Input.GetKeyDown(KeyCode.K))
			{
				if (InSettings)
				{
					ResumeGame();
				}
				else
				{
					PauseGame();
				}
			}
		}
			
    }

	public void PauseGame()
	{
		SetState(Crosshair, false);
		SetState(SettingsMenuObject, true);
		Time.timeScale = 0f;
		InSettings = true;
	}

	public void ResumeGame()
	{
		SetState(Crosshair, true);
		SetState(SettingsMenuObject, false);
		Time.timeScale = 1f;
		InSettings = false;
	}

	public void SetState(GameObject g, bool state)
	{
		g.SetActive(state);
	}

	[DllImport("__Internal")]
	private static extern string GetFromStorage(string key);

	[DllImport("__Internal")]
	private static extern void WriteToStorage(string key, string value);

	[DllImport("__Internal")]
	private static extern void DeleteFromStorage(string key); 
}
