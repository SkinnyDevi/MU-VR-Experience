using System.Runtime.InteropServices;
using UnityEngine;

using TMPro;

public class SettingsMenu : MonoBehaviour
{
	public GameObject SettingsMenuObject;
	public static bool InSettings;
	public GameObject Crosshair;

	bool vrOptionChanged = false;
	PlayerVRHandler playerHandler;

    void Start()
    {
		playerHandler = GameObject.FindObjectOfType<PlayerVRHandler>();
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
		if (vrOptionChanged)
		{
			playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.VR);
			//TODO: call player handler -> do transition -> switch camera to vr player
			// --------------------------> change to vr
		}
		else
		{
			playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.Mouse);
			//TODO: call player handler -> do transition ----------> switch camera to mouse player
			// --------------------------> change to mouse player
		}
		Time.timeScale = 1f;
		InSettings = false;
	}

	public void SetState(GameObject g, bool state)
	{
		g.SetActive(state);
	}

	public void VRToggleHandler()
	{
		TMP_Text vrStateText = gameObject.transform.Find("Settings Menu/Canvas/Pause Menu/Enable VR Checkbox/Toggle/Label").GetComponent<TMP_Text>();
		if (vrStateText.text.Equals("Disabled"))
		{
			vrStateText.text = "Enabled";
			vrOptionChanged = true;
		}
		else
		{
			vrStateText.text = "Disabled";
				vrOptionChanged = false;
		}
	}

	[DllImport("__Internal")]
	private static extern string GetFromStorage(string key);

	[DllImport("__Internal")]
	private static extern void WriteToStorage(string key, string value);

	[DllImport("__Internal")]
	private static extern void DeleteFromStorage(string key); 
}
