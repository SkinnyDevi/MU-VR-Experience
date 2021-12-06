using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;
using System;

using TMPro;

public class SettingsMenu : MonoBehaviour
{
	public GameObject SettingsMenuObject;
	public static bool InSettings;
	public GameObject Crosshair;

	bool vrOptionChanged = false;
	bool subsystemsStarted = false;
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
			if (Input.GetKeyDown(KeyCode.Escape))
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

		if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
		{
			XRGeneralSettings.Instance.Manager.StartSubsystems();
			subsystemsStarted = true;
			playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.VR);
		}
    }

	void OnApplicationQuit()
	{
		if (subsystemsStarted)
		{
			XRGeneralSettings.Instance.Manager.StopSubsystems();
			XRGeneralSettings.Instance.Manager.DeinitializeLoader();
			subsystemsStarted = false;
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
			if (!subsystemsStarted )
			{
				StartCoroutine(XRGeneralSettings.Instance.Manager.InitializeLoader());
			}
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
		TMP_Text sensitivityValue;

		string settingsPath = "Settings Menu/Canvas/Pause Menu";
		
		wasd = gameObject.transform.Find(settingsPath + "/Movement Choose/WASD").GetComponent<Button>();
		arrows = gameObject.transform.Find(settingsPath + "/Movement Choose/Arrows").GetComponent<Button>();
		leftclick = gameObject.transform.Find(settingsPath + "/Interaction Choose/LeftClick").GetComponent<Button>();
		keye = gameObject.transform.Find(settingsPath + "/Interaction Choose/KeyE").GetComponent<Button>();
		sensitivityValue = gameObject.transform.Find(settingsPath + "/Sensitivity Slider/Sensitivity Value").GetComponent<TMP_Text>();
		
		string movementType = !wasd.IsInteractable() ? wasd.gameObject.name : arrows.gameObject.name;
		string interactionType = !leftclick.IsInteractable() ? leftclick.gameObject.name : keye.gameObject.name;
		float saveVolume = VolumeSlider.RawVolume;

		Debug.Log($"SETTINGS TO SAVE: {movementType}, {interactionType}, {sensitivityValue.text}, {saveVolume}");
		UserInfoManager.SaveString(UserInfoManager.SaveType.SettingsMovement, movementType);
		UserInfoManager.SaveString(UserInfoManager.SaveType.SettingsInteraction, interactionType);
		UserInfoManager.SaveFloat(UserInfoManager.SaveType.SettingsSensitivity, float.Parse(sensitivityValue.text));
		UserInfoManager.SaveFloat(UserInfoManager.SaveType.SettingsVolume, saveVolume);
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
}
