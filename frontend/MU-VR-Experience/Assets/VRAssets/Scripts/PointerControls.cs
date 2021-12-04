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
	Coroutine CheckRating, NewRatingSubmit = null;

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

		Regex hoverableTest = new Regex(".*(Login|Register|Enter|BillBoardDoors|Like|Regular|Dislike).*$");
		if (hoverableTest.Matches(hoverObject).Count > 0)
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
			UserInfoManager.SaveString("VideoURL", video);
			UserInfoManager.SaveInt("VideoID", Int32.Parse(currentObject.Substring(6, 1)));
			currentObject = "EnterButtonExit";
			SceneLoader.LoadScene(SceneLoader.Scene.TheatreCinema);
		}
	}

	public void HandleRatingButtons()
	{
		if (currentObject.Contains("Cube"))
		{
			Debug.Log(UserInfoManager.GetInt("User"));
			SubmitRatingHandler.UserID = UserInfoManager.GetInt("User");
			SubmitRatingHandler submit = GameObject.FindObjectOfType<SubmitRatingHandler>();
			if (!SubmitRatingHandler.hasCheckedExistence)
			{
				CheckRating = StartCoroutine(submit.CheckRatingExistence(currentObject));
				currentObject = "RatingButtonOnHold";
			}	
			else
			{
				if (CheckRating != null) StopCoroutine(CheckRating);
				NewRatingSubmit = StartCoroutine(submit.SubmitRating(ReplaceableWall, BillboardDoors));
				currentObject = "RatingButtonExit";
			}
		}
		else
			if (NewRatingSubmit != null) StopCoroutine(NewRatingSubmit);
	}
}
