using UnityEngine;

public class PointerControls : MonoBehaviour
{
	public Camera PlayerCamera;
	public float RayLength = 4f;

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, RayLength))
			{
				Debug.Log(hit.transform.name);
			}
		}
    }
}
