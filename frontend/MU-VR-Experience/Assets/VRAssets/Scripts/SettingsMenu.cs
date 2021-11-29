using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System;

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
		}
		else
		{
			playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.Mouse);
		}
		Time.timeScale = 1f;
		SaveSettings();
		InSettings = false;
	}

	void SaveSettings()
	{
		Button wasd, arrows, leftclick, keye;
		TMP_Text sensitivityValue, volumeValue;

		string settingsPath = "Settings Menu/Canvas/Pause Menu";
		
		wasd = gameObject.transform.Find(settingsPath + "/Movement Choose/WASD").GetComponent<Button>();
		arrows = gameObject.transform.Find(settingsPath + "/Movement Choose/Arrows").GetComponent<Button>();
		leftclick = gameObject.transform.Find(settingsPath + "/Interaction Choose/LeftClick").GetComponent<Button>();
		keye = gameObject.transform.Find(settingsPath + "/Interaction Choose/KeyE").GetComponent<Button>();
		sensitivityValue = gameObject.transform.Find(settingsPath + "/Sensitivity Slider/Sensitivity Value").GetComponent<TMP_Text>();
		volumeValue = gameObject.transform.Find(settingsPath + "/Volume Slider/Volume Value").GetComponent<TMP_Text>();

		string movementType = !wasd.IsInteractable() ? wasd.gameObject.name : arrows.gameObject.name;
		string interactionType = !leftclick.IsInteractable() ? leftclick.gameObject.name : keye.gameObject.name;

		Debug.Log($"SETTINGS TO SAVE: {movementType}, {interactionType}, {sensitivityValue.text}, {volumeValue.text}");
		UserInfoManager.SaveString(UserInfoManager.SaveType.SettingsMovement, movementType);
		UserInfoManager.SaveString(UserInfoManager.SaveType.SettingsInteraction, interactionType);
		UserInfoManager.SaveInt(UserInfoManager.SaveType.SettingsSensitivity, Int32.Parse(sensitivityValue.text));
		UserInfoManager.SaveInt(UserInfoManager.SaveType.SettingsVolume, Int32.Parse(volumeValue.text));
	}

	void SetState(GameObject g, bool state)
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
