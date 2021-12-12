using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsMenuObject, Crosshair, RemoveVRButton;
    public static bool InSettings;

	string rootPath = "Settings Menu/Canvas/Pause Menu/";
    bool vrOptionChanged = false;
    PlayerVRHandler playerHandler;
    InputMaster keyboardControls;
	TMP_Text vrStateText;
	Toggle vrToggler;
	GameObject returnToReceptionBtn;


    void Awake()
    {
        keyboardControls = new InputMaster();
        keyboardControls.Enable();
        keyboardControls.Menus.Settings.performed += _ => OpenSettings();
    }

    void Start()
    {
        playerHandler = GameObject.FindObjectOfType<PlayerVRHandler>();
		vrStateText = gameObject.transform.Find(rootPath + "Enable VR Checkbox/Toggle/Label").GetComponent<TMP_Text>();
        vrToggler = gameObject.transform.Find(rootPath + "Enable VR Checkbox/Toggle").GetComponent<Toggle>();
		returnToReceptionBtn = gameObject.transform.Find(rootPath + "ReturnReception").gameObject;

		if (SceneManager.GetActiveScene().name.Equals("MainHub"))
			returnToReceptionBtn.SetActive(false);
		else
			returnToReceptionBtn.SetActive(true);

		SettingsMenuObject.SetActive(false);
    }

    void OpenSettings()
    {
        if (!UserTransition.TransitionMade)
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
	
	public void ReturnMainHub() // Only assigned in the UI
	{
		ResumeGame();
		SceneLoader.LoadScene(SceneLoader.Scene.MainHub);
	}

    public void VRToggleHandler() // Only assigned in the UI
    {
        if (vrStateText.text.Equals("Disabled") && vrToggler.isOn)
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

	public void ExternalVRDeactivation()
	{
		vrStateText.text = "Disabled";
        vrOptionChanged = false;
		vrToggler.isOn = false;
	}
}
