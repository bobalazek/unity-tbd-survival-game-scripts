using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Borut Balazek <bobalazek124@gmail.com>
 */
namespace Game.Core {
	[Serializable]
	public class Options {
		// TODO: Implement ootii - EasyInput!
		// TODO: Fix gamepad stuff. Currently not working.

		/***** Game *****/
		public int gameDifficulty = 1; // 0 - Easy; 1 - Normal; 2 - Hard

		/***** Display *****/
		public int displayHUD = 1; // 0 - false; 1 - true
		public int displayMap = 1; // 0 - false; 1 - true
		public int displayFPSCounter = 0; // 0 - false; 1 - true

		/***** Video *****/
		public int videoFullscreen = 1; // 0 - false; 1 - true
		public string videoResolutionString = "800x600@60Hz";
		public int videoResolution = 0;
		public int videoTextureQuality = 1; // 0 - Low; 1 - Medium; 2 - High; 3 - Very High
		public int videoAntiAliasing = 1; // 0 - Off; 1 - 2x; 2 - 4x; 3 - 8x
		public int videoShadows = 1; // 0 - Off; 1 - Normal; 2 - High
		public int videoShadowResolution = 1; // 0 - Low; 1 - Medium; 2 - High; 3 - Very High
		public int videoVSync = 1; // 0 - Off; 1 - Normal; 2 - High

		/***** Audio *****/
		public float audioMasterVolume = 1f;
		public float audioMusicVolume = 1f;
		public float audioEffectsVolume = 1f;
		public float audioUIVolume = 1f;
		public float audioInGameVolume = 1f;

		/***** Controls *****/
		/** Inverse look **/
		public int controlsInverseLookMouse = 0; // 0 - false; 1 - true
		public int controlsInverseLookJoystick = 0; // 0 - false; 1 - true

		/** Look sensitivity **/
		public float controlsLookSensitivityMouse = 1f;
		public float controlsLookSensitivityJoystick = 1f;

		/** Joystick Look Axes **/
		// The Look axed differ from joystick to joystick, 
		//   but the movement (Horizontal) axes are always the same &
		//   saved inside the Horizontal / Vertical axes.
		public KeyCode controlsJoystickLookXAxis = KeyCode.JoystickButton4;
		public KeyCode controlsJoystickLookYAxis = KeyCode.JoystickButton5;

		/***** Key Bindings *****/
		/*** Forward / Backward - Vertical ***/
		/* Forward */
		public KeyCode keyBindingsForward = KeyCode.UpArrow;
		public KeyCode keyBindingsForwardSecondary = KeyCode.W;
		// public KeyCode keyBindgingsForwardJoystick; // Not needed. Will be the "Vertical" / Y-axis on the joystick

		/* Backward */
		public KeyCode keyBindingsBackward = KeyCode.DownArrow;
		public KeyCode keyBindingsBackwardSecondary = KeyCode.S;
		// public KeyCode keyBindingsBackwardJoystick; // Not needed. Will be the "Vertical" / Y-axis on the joystick

		/*** Turn - Horizontal ***/
		/* Left */
		public KeyCode keyBindingsTurnLeft = KeyCode.LeftArrow;
		public KeyCode keyBindingsTurnLeftSecondary = KeyCode.A;
		// public KeyCode keyBindingsTurnLeftJoystick; // Not needed. Will be the "Horizontal" / X-axis on the joystick

		/* Right */
		public KeyCode keyBindingsTurnRight = KeyCode.RightArrow;
		public KeyCode keyBindingsTurnRightSecondary = KeyCode.D;
		// public KeyCode keyBindingsTurnRightJoystick; // Not needed. Will be the "Horizontal" / X-axis on the joystick

		/*** Strafe ***/
		/* Left */
		public KeyCode keyBindingsStrafeLeft = KeyCode.Q;
		public KeyCode keyBindingsStrafeLeftSecondary;
		public KeyCode keyBindingsStrafeLeftJoystick = KeyCode.JoystickButton12;

		/* Right */
		public KeyCode keyBindingsStrafeRight = KeyCode.E;
		public KeyCode keyBindingsStrafeRightSecondary;
		public KeyCode keyBindingsStrafeRightJoystick = KeyCode.JoystickButton13;

		/*** Actions ***/
		/* Attack */
		public KeyCode keyBindingsAttack = KeyCode.R;
		public KeyCode keyBindingsAttackSecondary;
		public KeyCode keyBindingsAttackJoystick = KeyCode.JoystickButton2;

		/* Use */
		public KeyCode keyBindingsUse = KeyCode.F;
		public KeyCode keyBindingsUseSecondary;
		public KeyCode keyBindingsUseJoystick = KeyCode.JoystickButton3;

		/* Crouch */
		public KeyCode keyBindingsCrouch = KeyCode.LeftControl;
		public KeyCode keyBindingsCrouchSecondary;
		public KeyCode keyBindingsCrouchJoystick = KeyCode.JoystickButton4;

		/* Walk */
		public KeyCode keyBindingsWalk = KeyCode.Y;
		public KeyCode keyBindingsWalkSecondary;
		public KeyCode keyBindingsWalkJoystick = KeyCode.JoystickButton5;

		/* Run */
		// TBD: That's the default state, not really needed anymore
		public KeyCode keyBindingsRun;
		public KeyCode keyBindingsRunSecondary;
		public KeyCode keyBindingsRunJoystick;

		/* Sprint */
		public KeyCode keyBindingsSprint = KeyCode.LeftShift;
		public KeyCode keyBindingsSprintSecondary;
		public KeyCode keyBindingsSprintJoystick = KeyCode.JoystickButton5;

		/* Jump */
		public KeyCode keyBindingsJump = KeyCode.Space;
		public KeyCode keyBindingsJumpSecondary;
		public KeyCode keyBindingsJumpJoystick = KeyCode.JoystickButton0;

		/* Switch camera perspective */
		public KeyCode keyBindingsSwitchCameraPerspective = KeyCode.C;
		public KeyCode keyBindingsSwitchCameraPerspectiveSecondary;
		public KeyCode keyBindingsSwithcCameraPerspectiveJoystick = KeyCode.JoystickButton5;

		/* Pause */
		public KeyCode keyBindingsPause = KeyCode.Escape;
		public KeyCode keyBindingsPauseSecondary = KeyCode.Pause;
		public KeyCode keyBindingsPauseJoystick = KeyCode.JoystickButton6;
	}
}