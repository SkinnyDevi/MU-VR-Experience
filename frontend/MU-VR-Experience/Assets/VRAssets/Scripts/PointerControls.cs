using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Text.RegularExpressions;

public class PointerControls : MonoBehaviour
{
	public Camera PlayerCamera;
	public GameObject SelectedCrosshair, ReplaceableWall, BillboardDoors;
	public float RayLength;

	string currentObject;
	string hoverObject;

	void Start()
	{
		currentObject = "";
		hoverObject = "";
	}

    void Update()
    {
		CurrentObjectHandler();
		HighlightSelectable();

		EnterBillboard();

		HandleBillboardEnterButtons();
		HandleRatingButtons();
    }

	void CurrentObjectHandler()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, RayLength))
			{
				currentObject = hit.transform.name;
				Debug.Log(currentObject);
			}
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

		if (hoverObject.Equals("Login") || hoverObject.Equals("Register") || hoverObject.Contains("Enter") || hoverObject.Equals("BillBoardDoors"))
		{
			SelectedCrosshair.SetActive(true);
		}
		else
		{
			SelectedCrosshair.SetActive(false);
		}
	}

	public void HandleBillboardEnterButtons()
	{
		if (currentObject.Contains("Enter-"))
		{
			string video = "Assets/VRAssets/static/videos/" + currentObject.Substring(6, 1) + ".mp4";
			UserInfoManager.SaveString("videoID", video);
			currentObject = "EnterButtonExit";
			SceneLoader.LoadScene(SceneLoader.Scene.TheatreCinema);
		}
	}

	public void HandleRatingButtons()
	{
		Regex rx = new Regex(@"(Liked||Regular||Disliked)");

		if (rx.Matches(currentObject).Count > 1)
		{
			//StartCoroutine(SubmitRatingHandler.SubmitRating(currentObject));
			ReplaceableWall.SetActive(false);
			BillboardDoors.SetActive(true);
		}
	}
}
