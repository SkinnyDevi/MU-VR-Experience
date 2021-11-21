using UnityEngine;

public class KeyboardMovementControls : MonoBehaviour
{
	public CharacterController controller;

	public float speedMovement = 12f;
	public float gravity = -9.81f;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public static bool IsInMenu = false;

	Vector3 velocity;
	bool isGrounded;

    void Update()
    {
		if (!IsInMenu)
		{
			isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

			if (isGrounded && velocity.y < 0) velocity.y = -2f;

			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			Vector3 movementArrow = transform.right * x + transform.forward * z;

			controller.Move(movementArrow * speedMovement * Time.deltaTime);

			velocity.y += gravity * Time.deltaTime;

			controller.Move(velocity * Time.deltaTime);
		}
    }
}
