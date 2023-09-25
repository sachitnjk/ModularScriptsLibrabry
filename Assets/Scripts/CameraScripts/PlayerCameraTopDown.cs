using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerCameraTopDown : MonoBehaviour
{
	[Header("References needed for camera")]
	[SerializeField] private Transform playerTransform;

	[Header("Camera Position and sensitivity")]
	[SerializeField] private Vector3 positionOffset;
	[SerializeField] private Vector3 rotationOffset;
	[SerializeField] private float sensitivity;
	[SerializeField] private float distance;

	[Header("Camera alignment angles")]
	[SerializeField] private float camVerticalViewAngle;
	[SerializeField] private float camHorizontalViewAngle;


	private PlayerInput playerInput;
	private InputAction lookAction;

	private Vector2 _rotation;
	private float _currentDistance;

	private float minVerticalAngle;
	private float maxVerticalAngle;
	private float minHorizontalAngle;
	private float maxHorizontalAngle;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();

		minVerticalAngle = -camVerticalViewAngle;
		maxVerticalAngle = camVerticalViewAngle;

		minHorizontalAngle = -camHorizontalViewAngle;
		maxHorizontalAngle = camHorizontalViewAngle;
	}

	private void LateUpdate()
	{
		_currentDistance = distance;

		lookAction = playerInput.actions["Look"];
		Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

		_rotation += mouseDelta * sensitivity;

		_rotation.y = Mathf.Clamp(_rotation.y, minVerticalAngle, maxVerticalAngle);
		_rotation.x = Mathf.Clamp(_rotation.x, minHorizontalAngle, maxHorizontalAngle);

		Quaternion rotation = Quaternion.Euler(_rotation.y + rotationOffset.x, _rotation.x + rotationOffset.y, 0 + rotationOffset.z);
		Vector3 position = playerTransform.position - (rotation * Vector3.forward * _currentDistance) + positionOffset;

		transform.rotation = rotation;
		transform.position = position;
	}

}
