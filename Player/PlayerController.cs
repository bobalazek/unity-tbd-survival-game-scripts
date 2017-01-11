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
	[RequireComponent(typeof(PlayerObject))]
	[RequireComponent(typeof(PlayerMotor))]
	[RequireComponent(typeof(PlayerCamera))]
	[RequireComponent(typeof(CharacterObject))]
	public class PlayerController : MonoBehaviour {
		private CharacterObject characterObject;
		private PlayerMotor playerMotor;

		void Start () {
			characterObject = GetComponent<CharacterObject> ();
			playerMotor = GetComponent<PlayerMotor> ();
		}

		void Update () {
			updateInput ();
		}

		void FixedUpdate () {
			playerMotor.updateMotion ();
		}

		void updateInput () {
			/*** Horizontal ***/
			float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");

			/*** Vertical ***/
			float vertical = CrossPlatformInputManager.GetAxis ("Vertical");

			/*** Strafe ***/
			bool strafeLeft = (
				(
					OptionsManager.getOptions().keyBindingsStrafeLeft != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsStrafeLeft)
				) || (
					OptionsManager.getOptions().keyBindingsStrafeLeftSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsStrafeLeftSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsStrafeLeftJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsStrafeLeftJoystick)
				)
			);
			bool strafeRight = (
				(
					OptionsManager.getOptions().keyBindingsStrafeRight != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsStrafeRight)
				) || (
					OptionsManager.getOptions().keyBindingsStrafeRightSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsStrafeRightSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsStrafeRightJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsStrafeRightJoystick)
				)
			);
			int strafe = strafeLeft && strafeRight
				? 0
				: (strafeLeft
					? -1
					: (strafeRight
						? 1
						: 0
					)
				);

			/*** Mouse X ***/
			float mouseX = CrossPlatformInputManager.GetAxis ("Mouse X");

			/*** Mouse Y ***/
			float mouseY = CrossPlatformInputManager.GetAxis ("Mouse Y");

			/*** Joystick X ***/
			float joystickX = CrossPlatformInputManager.GetAxis ("Joystick X");

			/*** Joystick Y ***/
			float joystickY = CrossPlatformInputManager.GetAxis ("Joystick Y");

			/*** Jump ***/
			bool jump = (
				(
					OptionsManager.getOptions().keyBindingsJump != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsJump)
				) || (
					OptionsManager.getOptions().keyBindingsJumpSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsJumpSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsJumpJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsJumpJoystick)
				)
			);

			/*** Crouch ***/
			bool crouch = (
				(
					OptionsManager.getOptions().keyBindingsCrouch != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsCrouch)
				) || (
					OptionsManager.getOptions().keyBindingsCrouchSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsCrouchSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsCrouchJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsCrouchJoystick)
				)
			);

			/*** Walk ***/
			bool walk = (
				(
					OptionsManager.getOptions().keyBindingsWalk != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsWalk)
				) || (
					OptionsManager.getOptions().keyBindingsWalkSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsWalkSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsWalkJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsWalkJoystick)
				)
			);

			/*** Run ***/
			bool run = (
				(
					OptionsManager.getOptions().keyBindingsRun != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsRun)
				) || (
					OptionsManager.getOptions().keyBindingsRunSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsRunSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsRunJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsRunJoystick)
				)
			);

			/*** Sprint ***/
			bool sprint = (
				(
					OptionsManager.getOptions().keyBindingsSprint != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsSprint)
				) || (
					OptionsManager.getOptions().keyBindingsSprintSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsSprintSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsSprintJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsSprintJoystick)
				)
			);

			/*** Attack ***/
			bool attack = (
				(
					OptionsManager.getOptions().keyBindingsAttack != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsAttack)
				) || (
					OptionsManager.getOptions().keyBindingsAttackSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsAttackSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsAttackJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsAttackJoystick)
				)
			);

			/*** Use ***/
			bool use = (
				(
					OptionsManager.getOptions().keyBindingsUse != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsUse)
				) || (
					OptionsManager.getOptions().keyBindingsUseSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsUseSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsUseJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsUseJoystick)
				)
			);

			/*** Use once ***/
			bool useOnce = (
				(
					OptionsManager.getOptions().keyBindingsUse != null &&
					Input.GetKeyDown (OptionsManager.getOptions().keyBindingsUse)
				) || (
					OptionsManager.getOptions().keyBindingsUseSecondary != null &&
					Input.GetKeyDown (OptionsManager.getOptions().keyBindingsUseSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsUseJoystick != null &&
					Input.GetKeyDown (OptionsManager.getOptions().keyBindingsUseJoystick)
				)
			);

			/*** Switch camera prespective ***/
			bool switchCameraPerspective = (
				(
					OptionsManager.getOptions().keyBindingsSwitchCameraPerspective != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsSwitchCameraPerspective)
				) || (
					OptionsManager.getOptions().keyBindingsSwitchCameraPerspectiveSecondary != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsSwitchCameraPerspectiveSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsSwithcCameraPerspectiveJoystick != null &&
					Input.GetKey (OptionsManager.getOptions().keyBindingsSwithcCameraPerspectiveJoystick)
				)
			);

			/*** Switch camera prespective once ***/
			bool switchCameraPerspectiveOnce = (
				(
					OptionsManager.getOptions().keyBindingsSwitchCameraPerspective != null &&
					Input.GetKeyDown (OptionsManager.getOptions().keyBindingsSwitchCameraPerspective)
				) || (
					OptionsManager.getOptions().keyBindingsSwitchCameraPerspectiveSecondary != null &&
					Input.GetKeyDown (OptionsManager.getOptions().keyBindingsSwitchCameraPerspectiveSecondary)
				) || (
					OptionsManager.getOptions().keyBindingsSwithcCameraPerspectiveJoystick != null &&
					Input.GetKeyDown (OptionsManager.getOptions().keyBindingsSwithcCameraPerspectiveJoystick)
				)
			);

			/***** Inverse *****/
			/*** Mouse ***/
			if (OptionsManager.getOptions ().controlsInverseLookMouse == 1) {
				mouseY *= -1;
			}

			/*** Joystick ***/
			if (OptionsManager.getOptions ().controlsInverseLookJoystick == 1) {
				joystickY *= -1;
			}

			/***** Set data *****/
			characterObject.characterState.inputMoveHorizontal = horizontal;
			characterObject.characterState.inputMoveStrafe = strafe;
			characterObject.characterState.inputMoveVertical = vertical;

			characterObject.characterState.inputLookMouseX = mouseY;
			characterObject.characterState.inputLookMouseY = mouseY;
			characterObject.characterState.inputLookJoystickX = joystickX;
			characterObject.characterState.inputLookJoystickY = joystickY;

			characterObject.characterState.inputJump = jump;
			characterObject.characterState.inputCrouch = crouch;
			characterObject.characterState.inputWalk = walk;
			characterObject.characterState.inputRun = run;
			characterObject.characterState.inputSprint = sprint;
			characterObject.characterState.inputAttack = attack;
			characterObject.characterState.inputUse = use;
			characterObject.characterState.inputUseOnce = useOnce;
			characterObject.characterState.inputSwitchCameraPerspective = switchCameraPerspective;
			characterObject.characterState.inputSwitchCameraPerspectiveOnce = switchCameraPerspectiveOnce;
		}
	}
}