using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction moveAction;

	private Animator playerAnimator;
	private CharacterController playerCharController;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();

		playerCharController = GetComponent<CharacterController>();
		playerAnimator = GetComponentInChildren<Animator>();
	}
}
