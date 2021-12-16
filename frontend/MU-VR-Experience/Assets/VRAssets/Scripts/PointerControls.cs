using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using System;
using System.Text.RegularExpressions;
using System.Collections;

public class PointerControls : MonoBehaviour
{
	public Camera PlayerCamera;
	public GameObject SelectedCrosshair, ReplaceableWall, BillboardDoors;
	public float RayLength;
	public static bool CanInteractAgain = true;

	string _currentObject, _hoverObject;
	InputMaster _mouseControls;
	bool _mouseClick, _eKey;

	void Awake()
	{
		_mouseControls = new InputMaster();
		_mouseControls.Enable();
		_mouseControls.Player.MouseClick.performed += _ => _mouseClick = true;;
		_mouseControls.Player.KeyE.performed += _ => _eKey = true;
	}

	void Start()
	{
		_currentObject = "";
		_hoverObject = "";
		_mouseClick = false;
		_eKey = false;
	}

	void Update()
	{
		if (!(SceneManager.GetActiveScene().name.Equals("TheatreCinema") && PlayerVRHandler.CurrentPlayerType == PlayerVRHandler.PlayerType.VR))
		{
			if (!((_currentObject.Equals("Login") || _currentObject.Equals("Register")) || (_hoverObject.Equals("Login") || _hoverObject.Equals("Register")))) CanInteractAgain = true;
			if (CanInteractAgain) HandleInteractionKey();

			HighlightSelectable();
			RemoveCrosshairOnScreen();
		}
	}

	void HandleInteractionKey()
	{
		if (UserInfoManager.GetString(UserInfoManager.SaveType.SettingsInteraction).Equals("LeftClick"))
		{
			if (_mouseClick && !UserTransition.TransitionMade)
			{
				CurrentObjectHandler();
			}
		}
		else
		{
			if (_eKey)
			{
				CurrentObjectHandler();
			}
		}

		_mouseClick = false;
		_eKey = false;
	}

	void CurrentObjectHandler()
	{
		RaycastHit hit;
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, RayLength))
		{
			_currentObject = hit.transform.name;
			// Debug.Log(_currentObject);
		}

		EnterBillboard();
		HandleBillboardEnterButtons();
		HandleRatingButtons();
	}

	void EnterBillboard()
	{
		if (_currentObject.Equals("BillBoardDoors"))
		{
			SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
		}
	}

	void HighlightSelectable()
	{
		RaycastHit hoverCast;
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hoverCast, RayLength))
		{
			_hoverObject = hoverCast.transform.name;
		}
		else
		{
			_hoverObject = "Air";
		}

		Regex hoverableTest = new Regex(".*(Login|Register|Enter|BillBoardDoors|Like|Regular|Dislike|Remove VR).*$");
		if (hoverableTest.Matches(_hoverObject).Count > 0)
		{
			SelectedCrosshair.SetActive(true);
		}
		else
		{
			SelectedCrosshair.SetActive(false);
		}
	}

	void RemoveCrosshairOnScreen()
	{
		RaycastHit hoverCast;
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hoverCast, 100f))
		{
			if (hoverCast.transform.name.Equals("Black Screen")) gameObject.transform.Find("Canvas").gameObject.SetActive(false);
			else gameObject.transform.Find("Canvas").gameObject.SetActive(true);
		}
	}

	public string GetCurrentObject()
	{
		return _currentObject;
	}

	public void SetCurrentObject(string str)
	{
		this._currentObject = str;
	}

	public void TransitionFinished()
	{
		_currentObject += "Exited";
		// Debug.Log(_currentObject);
	}

	public void HandleBillboardEnterButtons()
	{
		if (_currentObject.Contains("Enter-"))
		{
			UserInfoManager.SaveInt("VideoID", Int32.Parse(_currentObject.Substring(6, 1)));
			_currentObject = "EnterButtonExit";
			SceneLoader.LoadScene(SceneLoader.Scene.TheatreCinema);
		}
	}

	public void HandleRatingButtons()
	{
		if (_currentObject.Contains("Cube"))
		{
			SubmitRatingHandler.UserID = UserInfoManager.GetInt("User");
			StartCoroutine(SubmitRatingHandler.SubmitRating(_currentObject, ReplaceableWall, BillboardDoors));
		}
	}
}
