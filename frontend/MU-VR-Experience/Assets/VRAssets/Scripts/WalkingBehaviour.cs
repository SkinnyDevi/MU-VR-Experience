using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingBehaviour : MonoBehaviour
{
	Animator animator;
	InputMaster controls;
	Vector2 horizontalInput;

	void Awake()
	{
		controls = new InputMaster();
		controls.Enable();
		controls.Player.Movement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
	}

    void Start()
    {
		animator = GetComponent<Animator>();
    }

    void Update()
    {
		HandleAnimation(horizontalInput);
	}

	void HandleAnimation(Vector2 direction)
	{
		var keyboard = Keyboard.current;
		bool isWalking = animator.GetBool("isWalking");
		float walkSpeed = animator.GetFloat("backwardsWalk");

		animator.SetBool("isWalking", direction.x != 0 || direction.y != 0 ? true : false);
		animator.SetFloat("backwardsWalk", direction.x > 0 ? 1.5f : -1.5f);
		animator.SetFloat("backwardsWalk", direction.x < 0 ? -1.5f : 1.5f);
	}
}
