using UnityEngine;
using TMPro;

public class MouseLookControls : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	public Transform playerBody;
	public TMP_Text SensitivityValue;
	
	float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		if (SettingsMenu.inSettings)
		{
			Cursor.lockState = CursorLockMode.None;
			mouseSensitivity = float.Parse(SensitivityValue.text);
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
			playerBody.Rotate(Vector3.up * mouseX);
		}
    }
}
