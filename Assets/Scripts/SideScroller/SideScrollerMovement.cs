using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SideScrollerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private LayerMask groundLayer;

	private PlayerInput playerInput;
	private InputAction moveAction;

	private Transform groundCheckTransform;
	private Animator playerAnimator;
	private Rigidbody2D playerRb;
	private Vector2 moveDirection;

	public bool IsGrounded { get; private set; }
	private bool facingRight = true;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		moveAction = playerInput.actions["Move"];
	}

	private void Update()
	{
		IsGrounded = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, 0.7f, groundLayer);

		Move();
	}

	private void Move()
	{
		moveDirection = moveAction.ReadValue<Vector2>();

		if(moveDirection.x > 0 || moveDirection.x < 0 ) 
		{
			PlayerTurnCheck();
			//Animation bool set to true here
		}
		else
		{
			//Animation bool set to false here
		}
		playerRb.velocity = new Vector2(moveDirection.x * moveSpeed, playerRb.velocity.y);
	}

	private void PlayerTurnCheck()
	{
		if (moveDirection.x > 0 && !facingRight)
		{
			TurnPlayer();
		}
		else if (moveDirection.x < 0 && facingRight)
		{
			TurnPlayer();
		}
	}

	private void TurnPlayer()
	{
		if (facingRight)
		{
			Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
			transform.rotation = Quaternion.Euler(rotator);
			facingRight = !facingRight;
		}
		else
		{
			Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
			transform.rotation = Quaternion.Euler(rotator);
			facingRight = !facingRight;
		}
	}
}
