using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMovementControls : MonoBehaviour
{
	public enum Movement {WASD, Arrows}
	public float SpeedMovement = 8f;
	public float Gravity = -9.81f;
	public Transform GroundCheck;
	public float GroundDistance = 0.4f;
	public LayerMask GroundMask;
	public static bool IsInMenu = false;
	public static Movement CurrentMovementType = Movement.WASD;

	Vector3 _velocity;
	bool _isGrounded;
	InputMaster _controls;
	CharacterController _controller;
	Vector2 _rawWASDInput, _rawArrowsInput, _rawHorizontalInput, _smoothInputVelocity, _horizontalInput;

	void Awake()
	{
		_controls = new InputMaster();
		_controls.Enable();
		_controls.Player.MovementWASD.performed += ctx => _rawWASDInput = ctx.ReadValue<Vector2>();
		_controls.Player.MovementArrows.performed += ctx => _rawArrowsInput = ctx.ReadValue<Vector2>();
	}

	void Start()
	{
		_controller = gameObject.GetComponent<CharacterController>();
	}

	void Update()
	{
		HandleMovementType();
		_horizontalInput = Vector2.SmoothDamp(_horizontalInput, _rawHorizontalInput, ref _smoothInputVelocity, .15f);
		Move(_horizontalInput);	
	}

	private void Move(Vector2 direction)
	{
		if (!IsInMenu)
		{
			_isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

			if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;

			float x = direction.x;
			float z = direction.y;

			Vector3 movementArrow = transform.right * x + transform.forward * z;

			_controller.Move(movementArrow * SpeedMovement * Time.deltaTime);

			_velocity.y += Gravity * Time.deltaTime;

			_controller.Move(_velocity * Time.deltaTime);
		}
	}

	private void HandleMovementType()
	{
		switch(UserInfoManager.GetString(UserInfoManager.SaveType.SettingsMovement))
		{
			case "WASD":
				_rawHorizontalInput = _rawWASDInput;
				break;
			case "Arrows":
				var keyboard = Keyboard.current;
				bool arrowPressed = keyboard.upArrowKey.isPressed || keyboard.leftArrowKey.isPressed || keyboard.rightArrowKey.isPressed || keyboard.downArrowKey.isPressed;
				if (_rawArrowsInput != new Vector2(0f, 0f) && arrowPressed) _rawHorizontalInput = _rawArrowsInput;
				else _rawHorizontalInput = new Vector2(0f, 0f);
				break;
		}
	}
}
