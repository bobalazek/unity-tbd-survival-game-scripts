using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character;
using Game.Manager;
using UnityStandardAssets.CrossPlatformInput;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Player {
	public class PlayerCamera : MonoBehaviour {
		private CharacterObject characterObject;
		private PlayerCameraMode playerCameraMode = PlayerCameraMode.ThirdPersonBehindBack;

		[HideInInspector] public GameObject cameraGameObject;

		private float lookYMin = 5f;
		private float lookYMax = 75f;

		private float lookCurrentX = 0f;
		private float lookCurrentY = 0f;

		private float distance = 5.0f;
		private float height = 2.0f;
		private float rotationDamping = 5.0f;
		private float heightDamping = 2.0f;

		void Awake () {
			characterObject = GetComponent<CharacterObject> ();
			cameraGameObject = GameObject.FindGameObjectWithTag ("MainCamera");
		}

		void Update () {
			updateInput ();
		}
		
		void LateUpdate () {
			updateCamera ();
		}

		void updateInput () {
			// Mouse
			lookCurrentX += characterObject.characterState.inputLookMouseX;
			lookCurrentY += characterObject.characterState.inputLookMouseY;

			// Joystick
			if (characterObject.characterState.inputLookJoystickX != 0) {
				lookCurrentX += characterObject.characterState.inputLookJoystickX * OptionsManager.getOptions().controlsLookSensitivityJoystick;
			}
			if (characterObject.characterState.inputLookJoystickY != 0) {
				lookCurrentY += characterObject.characterState.inputLookJoystickY * OptionsManager.getOptions().controlsLookSensitivityJoystick;
			}

			// Limits
			lookCurrentX = ClampAngle (lookCurrentX, -360, 360);
			lookCurrentY = ClampAngle (lookCurrentY, lookYMin, lookYMax);
		}

		// TODO: Prepare for multiple camera modes
		// TODO: Occlusion
		void updateCamera () {
			// Calculate the current rotation angles
			float wantedRotationAngle = characterObject.transform.eulerAngles.y;
			float wantedHeight = characterObject.transform.position.y + height;

			float currentRotationAngle = cameraGameObject.transform.eulerAngles.y;
			float currentHeight = cameraGameObject.transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			cameraGameObject.transform.position = characterObject.transform.position;
			cameraGameObject.transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			cameraGameObject.transform.position = new Vector3(
				cameraGameObject.transform.position.x,
				currentHeight,
				cameraGameObject.transform.position.z
			);

			// Always look at the target
			cameraGameObject.transform.LookAt(characterObject.transform);
		}

		public static float ClampAngle(float angle, float min, float max) {
			if (angle < -360F)
				angle += 360F;
			if (angle > 360F)
				angle -= 360F;
			return Mathf.Clamp(angle, min, max);
		}
	}
}