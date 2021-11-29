using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PointerControls : MonoBehaviour
{
	public Camera PlayerCamera;
	public GameObject SelectedCrosshair;
	public float RayLength;

	string currentObject = "";
	string hoverObject = "";

    void Update()
    {
		HighlightSelectable();
		EnterBillboard();
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

		if (hoverObject.Equals("Login") || hoverObject.Equals("Register") || hoverObject.Contains("Enter"))
		{
			SelectedCrosshair.SetActive(true);
		}
		else
		{
			SelectedCrosshair.SetActive(false);
		}
	}
}
