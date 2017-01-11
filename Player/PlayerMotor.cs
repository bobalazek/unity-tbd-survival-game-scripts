using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Player {
	public class PlayerMotor : MonoBehaviour {
		private CharacterObject characterObject;
		private PlayerCamera playerCamera;
		private Transform objectTransform; // depending on the camera mode, that's either the character, or the camera

		private float turnAmount;
		private float forwardAmount;

		private float turnSmoothing = 2f;
		private float stationaryTurnSpeed = 180f;
		private float movingTurnSpeed = 360f;

		void Awake () {
			characterObject = GetComponent<CharacterObject> ();
			playerCamera = GetComponent<PlayerCamera> ();
		}
		
		public void updateMotion () {
			/*** General ***/
			objectTransform = playerCamera.cameraGameObject.transform;
			
			Vector3 forward = objectTransform.TransformDirection(Vector3.forward);
			Vector3 right = objectTransform.TransformDirection(Vector3.right);
			Vector3 move = (
				forward * characterObject.characterState.inputMoveVertical +
				right * characterObject.characterState.inputMoveHorizontal
			);

			if (move.magnitude > 1f) move.Normalize();

			move = transform.InverseTransformDirection(move);
			move = Vector3.ProjectOnPlane(move, characterObject.characterState.groundNormal);

			turnAmount = Mathf.Atan2(move.x, move.z);
			forwardAmount = move.z;

			/*** Move ***/
			characterObject.characterState.movement = move * characterObject.characterState.speedMax;

			characterObject.characterRigidbody.MovePosition (
				characterObject.transform.position +
				characterObject.characterState.movement * Time.deltaTime
			);

			/*** Turn ***/
			if (move != Vector3.zero) {
				Quaternion targetRotation = Quaternion.LookRotation (move, Vector3.up);
				Quaternion newRotation = Quaternion.Slerp (
					characterObject.characterRigidbody.rotation,
					targetRotation,
					turnSmoothing * Time.deltaTime
				);

				characterObject.characterRigidbody.MoveRotation (newRotation);
			}

			/*** Jump ***/
			// TODO: fix, not working on a slide
			if (
				characterObject.characterState.inputJump &&
				characterObject.characterState.isGrounded
			) {
				characterObject.characterRigidbody.velocity = new Vector3(
					0,
					characterObject.characterAttributes.jumpForce * 3,
					0
				);
			}
		}
	}
}