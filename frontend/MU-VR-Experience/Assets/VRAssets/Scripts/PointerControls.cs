using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PointerControls : MonoBehaviour
{
	public Camera PlayerCamera;
	public float RayLength = 4f;

	string currentObject = "";

    void Update()
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

	public void TransitionFinished()
	{
		currentObject += "Exited";
		Debug.Log(currentObject);
	}
}
