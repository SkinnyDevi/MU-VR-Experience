using System.Runtime.InteropServices;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
	public GameObject settingsMenu;
	public static bool inSettings;
	public GameObject crosshair;

    void Start()
    {
		settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
		{
			if (inSettings)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
    }

	public void PauseGame()
	{
		SetState(crosshair, false);
		SetState(settingsMenu, true);
		Time.timeScale = 0f;
		inSettings = true;
	}

	public void ResumeGame()
	{
		SetState(crosshair, true);
		SetState(settingsMenu, false);
		Time.timeScale = 1f;
		inSettings = false;
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
