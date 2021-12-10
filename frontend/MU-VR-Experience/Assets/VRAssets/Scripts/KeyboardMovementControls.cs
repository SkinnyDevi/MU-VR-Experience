using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMovementControls : MonoBehaviour
{
	public enum Movement {WASD, Arrows}
	public float speedMovement = 8f;
	public float gravity = -9.81f;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public static bool IsInMenu = false;
	public static Movement currentMovementType = Movement.WASD;

	Vector3 velocity;
	bool isGrounded;
	InputMaster controls;
	CharacterController controller;
	Vector2 rawWASDInput, rawArrowsInput, rawHorizontalInput, smoothInputVelocity, horizontalInput;

	void Awake()
	{
		controls = new InputMaster();
		controls.Enable();
		controls.Player.MovementWASD.performed += ctx => rawWASDInput = ctx.ReadValue<Vector2>();
		controls.Player.MovementArrows.performed += ctx => rawArrowsInput = ctx.ReadValue<Vector2>();
	}

	void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
	}

	void Update()
	{
		HandleMovementType();
		horizontalInput = Vector2.SmoothDamp(horizontalInput, rawHorizontalInput, ref smoothInputVelocity, .15f);
		Move(horizontalInput);	
	}

	void Move(Vector2 direction)
	{
		if (!IsInMenu)
		{
			isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

			if (isGrounded && velocity.y < 0) velocity.y = -2f;

			float x = direction.x;
			float z = direction.y;

			Vector3 movementArrow = transform.right * x + transform.forward * z;

			controller.Move(movementArrow * speedMovement * Time.deltaTime);

			velocity.y += gravity * Time.deltaTime;

			controller.Move(velocity * Time.deltaTime);
		}
	}

	void HandleMovementType()
	{
		switch(UserInfoManager.GetString(UserInfoManager.SaveType.SettingsMovement))
		{
			case "WASD":
				rawHorizontalInput = rawWASDInput;
				break;
			case "Arrows":
				var keyboard = Keyboard.current;
				bool arrowPressed = keyboard.upArrowKey.isPressed || keyboard.leftArrowKey.isPressed || keyboard.rightArrowKey.isPressed || keyboard.downArrowKey.isPressed;
				if (rawArrowsInput != new Vector2(0f, 0f) && arrowPressed) rawHorizontalInput = rawArrowsInput;
				else rawHorizontalInput = new Vector2(0f, 0f);
				break;
		}
	}
}
