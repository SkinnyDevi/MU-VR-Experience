using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class MouseLookControls : MonoBehaviour
{
	public float MouseSensitivity = 5f;
	public Transform PlayerBody;
	public TMP_Text SensitivityValue;
	
	float _xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
		if (SettingsMenu.InSettings || UserTransition.TransitionMade)
		{
			Cursor.lockState = CursorLockMode.None;
			MouseSensitivity = float.Parse(SensitivityValue.text);
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;

			var mouse = Pointer.current.delta.ReadValue();
			float mouseX = mouse.x * MouseSensitivity * Time.deltaTime;
			float mouseY = mouse.y * MouseSensitivity * Time.deltaTime;

			_xRotation -= mouseY;
			_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

			transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
			PlayerBody.Rotate(Vector3.up * mouseX);
		}
    }
}
