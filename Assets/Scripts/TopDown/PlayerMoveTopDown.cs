using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveTopDown : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction moveAction;

	private Animator playerAnimator;
	private CharacterController playerCharController;

	private Vector2 moveDirection;

	[Header("Serializable Movement Attributes")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationSpeed;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();

		if(playerInput != null ) 
		{
			moveAction = playerInput.actions["Move"];
		}

		playerCharController = GetComponent<CharacterController>();
		playerAnimator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		Vector2 moveDirection = moveAction.ReadValue<Vector2>();

		if (moveDirection != Vector2.zero)
		{
			// Calculate the angle between the input direction and the player's current forward direction
			float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

			// Rotate the player's forward direction towards the target angle smoothly using Slerp
			Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		}

		Vector3 move = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

		playerCharController.Move(move * moveSpeed * Time.deltaTime);

		//Add animator functionality here if needed
	}
}
