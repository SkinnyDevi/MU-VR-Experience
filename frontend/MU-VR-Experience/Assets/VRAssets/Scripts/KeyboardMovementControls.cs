using UnityEngine;

public class KeyboardMovementControls : MonoBehaviour
{
	public float speedMovement = 8f;
	public float gravity = -9.81f;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public static bool IsInMenu = false;

	Vector3 velocity;
	bool isGrounded;
	InputMaster controls;
	CharacterController controller;
	Vector2 rawHorizontalInput, smoothInputVelocity, horizontalInput;

	void Awake()
	{
		controls = new InputMaster();
		controls.Enable();
		controls.Player.Movement.performed += ctx => rawHorizontalInput = ctx.ReadValue<Vector2>();
	}

	void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
	}

	void Update()
	{
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
}
