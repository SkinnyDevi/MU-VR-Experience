using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using System;
using System.Text.RegularExpressions;
using System.Collections;

public class PointerControls : MonoBehaviour
{
	public Camera PlayerCamera;
	public GameObject SelectedCrosshair, ReplaceableWall, BillboardDoors;
	public float RayLength;
	public static bool canInteractAgain = true;

	string currentObject, hoverObject;
	InputMaster mouseControls;
	bool mouseClick, eKey;

	void Awake()
	{
		mouseControls = new InputMaster();
		mouseControls.Enable();
		mouseControls.Player.MouseClick.performed += _ => mouseClick = true;;
		mouseControls.Player.KeyE.performed += _ => eKey = true;
	}

	void Start()
	{
		currentObject = "";
		hoverObject = "";
		mouseClick = false;
		eKey = false;
	}

	void Update()
	{
		if (!((currentObject.Equals("Login") || currentObject.Equals("Register")) || (hoverObject.Equals("Login") || hoverObject.Equals("Register")))) canInteractAgain = true;
		if (canInteractAgain) HandleInteractionKey();

		HighlightSelectable();
		RemoveCrosshairOnScreen();
	}

	void HandleInteractionKey()
	{
		if (UserInfoManager.GetString(UserInfoManager.SaveType.SettingsInteraction).Equals("LeftClick"))
		{
			if (mouseClick && !UserTransition.TransitionMade)
			{
				CurrentObjectHandler();
			}
		}
		else
		{
			if (eKey)
			{
				CurrentObjectHandler();
			}
		}

		mouseClick = false;
		eKey = false;
	}

	void CurrentObjectHandler()
	{
		RaycastHit hit;
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, RayLength))
		{
			currentObject = hit.transform.name;
			Debug.Log(currentObject);
		}

		EnterBillboard();
		HandleBillboardEnterButtons();
		HandleRatingButtons();
	}

	void EnterBillboard()
	{
		if (currentObject.Equals("BillBoardDoors"))
		{
			SceneLoader.LoadScene(SceneLoader.Scene.TheatreBillboard);
		}
	}

	void HighlightSelectable()
	{
		RaycastHit hoverCast;
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hoverCast, RayLength))
		{
			hoverObject = hoverCast.transform.name;
		}
		else
		{
			hoverObject = "Air";
		}

		Regex hoverableTest = new Regex(".*(Login|Register|Enter|BillBoardDoors|Like|Regular|Dislike|Remove VR).*$");
		if (hoverableTest.Matches(hoverObject).Count > 0)
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
		return currentObject;
	}

	public void SetCurrentObject(string str)
	{
		this.currentObject = str;
	}

	public void TransitionFinished()
	{
		currentObject += "Exited";
		Debug.Log(currentObject);
	}

	public void HandleBillboardEnterButtons()
	{
		if (currentObject.Contains("Enter-"))
		{
			UserInfoManager.SaveInt("VideoID", Int32.Parse(currentObject.Substring(6, 1)));
			currentObject = "EnterButtonExit";
			SceneLoader.LoadScene(SceneLoader.Scene.TheatreCinema);
		}
	}

	public void HandleRatingButtons()
	{
		if (currentObject.Contains("Cube"))
		{
			SubmitRatingHandler.UserID = UserInfoManager.GetInt("User");
			StartCoroutine(SubmitRatingHandler.RatingSubmitTest(currentObject, ReplaceableWall, BillboardDoors));
		}
	}
}
