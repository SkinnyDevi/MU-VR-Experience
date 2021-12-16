using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingBehaviour : MonoBehaviour
{
	Animator _animator;
	InputMaster _controls;
	Vector2 _rawWASDInput, _rawArrowsInput, _horizontalInput;

	void Awake()
	{
		_controls = new InputMaster();
		_controls.Enable();
		_controls.Player.MovementWASD.performed += ctx => _rawWASDInput = ctx.ReadValue<Vector2>();
		_controls.Player.MovementArrows.performed += ctx => _rawArrowsInput = ctx.ReadValue<Vector2>();
	}

    void Start()
    {
		_animator = GetComponent<Animator>();
    }

    void Update()
    {
		bool movementType = UserInfoManager.GetString(UserInfoManager.SaveType.SettingsMovement).Equals("WASD");
		_horizontalInput = movementType ? _rawWASDInput : _rawArrowsInput;

		if (!movementType)
		{
			var keyboard = Keyboard.current;
			bool arrowPressed = keyboard.upArrowKey.isPressed || keyboard.leftArrowKey.isPressed || keyboard.rightArrowKey.isPressed || keyboard.downArrowKey.isPressed;

			if (_rawArrowsInput != new Vector2(0f, 0f) && arrowPressed) _horizontalInput = _rawArrowsInput;
			else _horizontalInput = new Vector2(0f, 0f);
		}
			
		HandleAnimation(_horizontalInput);
	}

	void HandleAnimation(Vector2 direction)
	{
		bool isWalking = _animator.GetBool("isWalking");
		float walkSpeed = _animator.GetFloat("backwardsWalk");

		_animator.SetBool("isWalking", direction.x != 0 || direction.y != 0 ? true : false);
		_animator.SetFloat("backwardsWalk", direction.x > 0 ? 1.5f : -1.5f);
		_animator.SetFloat("backwardsWalk", direction.x < 0 ? -1.5f : 1.5f);
	}
}
