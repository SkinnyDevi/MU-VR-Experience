using UnityEngine;

public class WalkingBehaviour : MonoBehaviour
{
	Animator animator;

    void Start()
    {
		animator = GetComponent<Animator>();
    }

    void Update()
    {
		bool isWalking = animator.GetBool("isWalking");
		float walkSpeed = animator.GetFloat("backwardsWalk");

		bool forwardPressed = Input.GetKey("w");
		bool backwardsPressed = Input.GetKey("s");
		bool rightPressed = Input.GetKey("d");
		bool leftPressed = Input.GetKey("a");

		animator.SetBool("isWalking", forwardPressed || backwardsPressed || rightPressed || leftPressed ? true : false);
		animator.SetFloat("backwardsWalk", forwardPressed ? 1.5f : -1.5f);
		animator.SetFloat("backwardsWalk", backwardsPressed ? -1.5f : 1.5f);
	}
}
