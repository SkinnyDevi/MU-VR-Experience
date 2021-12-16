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

	const string RootPath = "Settings Menu/Canvas/Pause Menu/";
    bool _vrOptionChanged, _acceptedVRPopup = false;
    PlayerVRHandler _playerHandler;
    InputMaster _keyboardControls;
	TMP_Text _vrStateText;
	Toggle _vrToggler;
	GameObject _returnToReceptionBtn;

    void Awake()
    {
        _keyboardControls = new InputMaster();
        _keyboardControls.Enable();
        _keyboardControls.Menus.Settings.performed += _ => OpenSettings();
    }

    void Start()
    {
        _playerHandler = GameObject.FindObjectOfType<PlayerVRHandler>();
		_vrStateText = gameObject.transform.Find(RootPath + "Enable VR Checkbox/Toggle/Label").GetComponent<TMP_Text>();
        _vrToggler = gameObject.transform.Find(RootPath + "Enable VR Checkbox/Toggle").GetComponent<Toggle>();
		_returnToReceptionBtn = gameObject.transform.Find(RootPath + "ReturnReception").gameObject;

		if (SceneManager.GetActiveScene().name.Equals("MainHub"))
			_returnToReceptionBtn.SetActive(false);
		else
			_returnToReceptionBtn.SetActive(true);

		SettingsMenuObject.SetActive(false);
    }

    private void OpenSettings()
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

	private void SaveSettings()
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

        // Debug.Log($"SETTINGS TO SAVE: {movementType}, {interactionType}, {sensitivityValue.text}, {saveVolume}");
        UserInfoManager.SaveString(UserInfoManager.SaveType.SettingsMovement, movementType);
        UserInfoManager.SaveString(UserInfoManager.SaveType.SettingsInteraction, interactionType);
        UserInfoManager.SaveFloat(UserInfoManager.SaveType.SettingsSensitivity, float.Parse(sensitivityValue.text));
        UserInfoManager.SaveFloat(UserInfoManager.SaveType.SettingsVolume, saveVolume);
		if (_vrOptionChanged && _acceptedVRPopup) UserInfoManager.SavePlayerType(UserInfoManager.PlayerType.VR);
		else UserInfoManager.SavePlayerType(UserInfoManager.PlayerType.Mouse);
    }

    private void SetState(GameObject g, bool state)
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
		if (gameObject.transform.Find("Settings Menu/Canvas/Warning VR Popup").gameObject.activeSelf)
		{
			SetState(gameObject.transform.Find("Settings Menu/Canvas/Warning VR Popup").gameObject, false);
			VRToggleHandler();
		}
        if (_vrOptionChanged && _acceptedVRPopup) _playerHandler.ChangePlayerType(PlayerVRHandler.PlayerType.VR);
		ContinueResumingGame();
    }

	private void HandleVRPopup(bool acceptedPopup)
	{
		if (acceptedPopup)
		{
			_acceptedVRPopup = true;
			_vrOptionChanged = true;
			UserInfoManager.SavePlayerType(UserInfoManager.PlayerType.VR);
			GameObject.Find("Environment/RegisterRoom/Walls/BillBoardEntry/Canvas/Enter Billboard").gameObject.GetComponent<TMP_Text>().text = "Grab the handle to enter!";
		}
		else
		{
			_acceptedVRPopup = false;
			_vrStateText.text = "Disabled";
            _vrOptionChanged = false;
			_vrToggler.isOn = false;
		}

		SetState(gameObject.transform.Find("Settings Menu/Canvas/Warning VR Popup").gameObject, false);
	}

	private void ContinueResumingGame()
	{
		Time.timeScale = 1f;
        SaveSettings();
        InSettings = false;
	}

	public void ClickYes()
	{
		HandleVRPopup(true);
	}

	public void ClickNo()
	{
		HandleVRPopup(false);
	}

	public void ReturnMainHub() // Only assigned in the UI
	{
		ResumeGame();
		SceneLoader.LoadScene(SceneLoader.Scene.MainHub);
	}

    public void VRToggleHandler() // Only assigned in the UI
    {
        if (_vrStateText.text.Equals("Disabled") && _vrToggler.isOn)
        {
            _vrStateText.text = "Enabled";
			if (!_acceptedVRPopup) SetState(gameObject.transform.Find("Settings Menu/Canvas/Warning VR Popup").gameObject, true);
			else 
			{
				_vrOptionChanged = true;
				_acceptedVRPopup = true;
			}
        }
        else
        {
            _vrStateText.text = "Disabled";
            _vrOptionChanged = false;
			_vrToggler.isOn = false;
        }
    }
}
