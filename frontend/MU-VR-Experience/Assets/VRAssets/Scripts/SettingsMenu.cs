using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
	public GameObject settingsMenu;
	public static bool inSettings;
	public GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
		settingsMenu.SetActive(false);
    }

    // Update is called once per frame
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
}
