using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingBehaviour : MonoBehaviour
{
	Animator animator;
	InputMaster controls;
	Vector2 rawWASDInput, rawArrowsInput, horizontalInput;

	void Awake()
	{
		controls = new InputMaster();
		controls.Enable();
		controls.Player.MovementWASD.performed += ctx => rawWASDInput = ctx.ReadValue<Vector2>();
		controls.Player.MovementArrows.performed += ctx => rawArrowsInput = ctx.ReadValue<Vector2>();
	}

    void Start()
    {
		animator = GetComponent<Animator>();
    }

    void Update()
    {
		bool movementType = UserInfoManager.GetString(UserInfoManager.SaveType.SettingsMovement).Equals("WASD");
		horizontalInput = movementType ? rawWASDInput : rawArrowsInput;

		if (!movementType)
		{
			var keyboard = Keyboard.current;
			bool arrowPressed = keyboard.upArrowKey.isPressed || keyboard.leftArrowKey.isPressed || keyboard.rightArrowKey.isPressed || keyboard.downArrowKey.isPressed;

			if (rawArrowsInput != new Vector2(0f, 0f) && arrowPressed) horizontalInput = rawArrowsInput;
			else horizontalInput = new Vector2(0f, 0f);
		}
			
		HandleAnimation(horizontalInput);
	}

	void HandleAnimation(Vector2 direction)
	{
		bool isWalking = animator.GetBool("isWalking");
		float walkSpeed = animator.GetFloat("backwardsWalk");

		animator.SetBool("isWalking", direction.x != 0 || direction.y != 0 ? true : false);
		animator.SetFloat("backwardsWalk", direction.x > 0 ? 1.5f : -1.5f);
		animator.SetFloat("backwardsWalk", direction.x < 0 ? -1.5f : 1.5f);
	}
}
