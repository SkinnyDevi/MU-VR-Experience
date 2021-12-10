using UnityEngine;

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
		horizontalInput = UserInfoManager.GetString(UserInfoManager.SaveType.SettingsMovement).Equals("WASD") ? rawWASDInput : rawArrowsInput;
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
