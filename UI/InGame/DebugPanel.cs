using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Character;

/**
 * Should be thrown in directly into the root of the scene (after the actual camera)!
 * 
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI.InGame {
	public class DebugPanel : MonoBehaviour {
		private CharacterObject characterObject;

		private Text positionText;
		private Text inputText;
		private Text velocityText;
		private Text attributesText;
		private Text statusesText;
		private Text timersText;

		void Start () {
			GameObject playerGameObject = GameObject.Find ("Player");
			if (!playerGameObject) {
				gameObject.SetActive (false);
				Debug.Log ("No player character found. Debug panel will be disabed.");
				return;
			}

			characterObject = playerGameObject.GetComponent<CharacterObject> ();

			positionText = transform.Find ("Content/Row/ColumnLeft/List/Position").GetComponent<Text> ();
			inputText = transform.Find ("Content/Row/ColumnLeft/List/Input").GetComponent<Text> ();
			velocityText = transform.Find ("Content/Row/ColumnLeft/List/Velocity").GetComponent<Text> ();
			attributesText = transform.Find ("Content/Row/ColumnLeft/List/Attributes").GetComponent<Text> ();
			statusesText = transform.Find ("Content/Row/ColumnRight/List/Statuses").GetComponent<Text> ();
			timersText = transform.Find ("Content/Row/ColumnRight/List/Timers").GetComponent<Text> ();
		}

		void Update () {
			if (!characterObject) {
				return;
			}
			// Position
			positionText.text = "Position\n" +
				"X: " + characterObject.transform.position.x + "\n" +
				"Y: " + characterObject.transform.position.y + "\n" +
				"Z: " + characterObject.transform.position.z;

			// Input
			inputText.text = "Input\n" +
				"Horizontal: " + characterObject.characterState.inputMoveHorizontal + "\n" +
				"Strafe: " + characterObject.characterState.inputMoveStrafe + "\n" +
				"Vertical: " + characterObject.characterState.inputMoveVertical + "\n" +
				"Mouse X: " + characterObject.characterState.inputLookMouseX + "\n" +
				"Mouse Y: " + characterObject.characterState.inputLookMouseY + "\n" +
				"Joystick X: " + characterObject.characterState.inputLookJoystickX + "\n" +
				"Joystick Y: " + characterObject.characterState.inputLookJoystickY + "\n" +
				"Jump: " + (characterObject.characterState.inputJump ? "yes" : "no") + "\n" +
				"Crouch: " + (characterObject.characterState.inputCrouch ? "yes" : "no") + "\n" +
				"Walk: " + (characterObject.characterState.inputWalk ? "yes" : "no") + "\n" +
				"Run: " + (characterObject.characterState.inputRun ? "yes" : "no") + "\n" +
				"Sprint: " + (characterObject.characterState.inputSprint ? "yes" : "no") + "\n" +
				"Attack: " + (characterObject.characterState.inputAttack ? "yes" : "no") + "\n" +
				"Use: " + (characterObject.characterState.inputUse ? "yes" : "no") + "\n" +
				"Use: once " + (characterObject.characterState.inputUseOnce ? "yes" : "no") + "\n" +
				"Camera pers.: " + (characterObject.characterState.inputSwitchCameraPerspective ? "yes" : "no") + "\n" +
				"Camera pres. once: " + (characterObject.characterState.inputSwitchCameraPerspectiveOnce ? "yes" : "no");

			// Velocity
			velocityText.text = "Velocity\n" +
				"X: " + characterObject.characterState.velocityX + "\n" +
				"Y: " + characterObject.characterState.velocityY + "\n" +
				"Z: " + characterObject.characterState.velocityZ;

			// Attributes
			attributesText.text =
			// Health
				"Health: " + characterObject.characterState.health + " -" +
				" [" + characterObject.characterState.healthDelta + "] -" +
				" " + characterObject.characterState.healthPercentage + "%\n" +
			// Stamina
				"Stamina: " + characterObject.characterState.stamina + " -" +
				" [" + characterObject.characterState.staminaDelta + "] -" +
				" " + characterObject.characterState.staminaPercentage + "%\n" +
			// Food
				"Food: " + characterObject.characterState.food + " -" +
				" [" + characterObject.characterState.foodDelta + "] -" +
				" " + characterObject.characterState.foodPercentage + "%\n" +
			// Water
				"Water: " + characterObject.characterState.water + " -" +
				" [" + characterObject.characterState.waterDelta + "] -" +
				" " + characterObject.characterState.waterPercentage + "%\n" +
			// Warmth
				"Warmth: " + characterObject.characterState.warmth + " -" +
				" [" + characterObject.characterState.warmthDelta + "] -" +
				" " + characterObject.characterState.warmthPercentage + "%\n" +
			// Awake
				"Awake: " + characterObject.characterState.awake + " -" +
				" [" + characterObject.characterState.awakeDelta + "] -" +
				" " + characterObject.characterState.awakePercentage + "%\n" +
			// Oxygen
				"Oxygen: " + characterObject.characterState.oxygen + " -" +
				" [" + characterObject.characterState.oxygenDelta + "] -" +
				" " + characterObject.characterState.oxygenPercentage + "%\n" +
			// Body temperature
				"Body temp.: " + characterObject.characterState.bodyTemperature + " -" +
				" [" + characterObject.characterState.bodyTemperatureDelta + "]\n" +
			// Ambient temperature
				"Ambient temp.: " + characterObject.characterState.ambientTemperature + "\n" +
			// Speed
				"Speed: " + characterObject.characterState.speed + " -" +
				" " + characterObject.characterState.speedMultiplier + "x -" +
				" " + characterObject.characterState.speedRelative + "\n" +
			// Attack
				"Attack: " + characterObject.characterState.attack + " -" +
				" " + characterObject.characterState.attackMultiplier + "x -" +
				" [" + characterObject.characterState.attackDeltaTime + "]\n";

			// Statuses
			statusesText.text =
				"Motion state: " + characterObject.characterState.motionState.ToString() + "\n" +
				"Idle: " + (characterObject.characterState.isIdle ? "yes" : "no") + "\n" +
				"Grounded: " + (characterObject.characterState.isGrounded ? "yes" : "no") + "\n" +
				"Airbourne: " + (characterObject.characterState.isAirbourne ? "yes" : "no") + "\n" +
				"Crouching: " + (characterObject.characterState.isCrouching ? "yes" : "no") + "\n" +
				"Walking: " + (characterObject.characterState.isWalking ? "yes" : "no") + "\n" +
				"Running: " + (characterObject.characterState.isRunning ? "yes" : "no") + "\n" +
				"Sprinting: " + (characterObject.characterState.isSprinting ? "yes" : "no") + "\n" +
				"Jumping: " + (characterObject.characterState.isJumping ? "yes" : "no") + "\n" +
				"Falling: " + (characterObject.characterState.isFalling ? "yes" : "no") + "\n" +
				"Sliding: " + (characterObject.characterState.isSliding ? "yes" : "no") + "\n" +
				"Climbing: " + (characterObject.characterState.isClimbing ? "yes" : "no") + "\n" +
				"Hanging: " + (characterObject.characterState.isHanging ? "yes" : "no") + "\n" +
				"Sleeping: " + (characterObject.characterState.isSleeping ? "yes" : "no") + "\n" +
				"Eating: " + (characterObject.characterState.isEating ? "yes" : "no") + "\n" +
				"Attacking: " + (characterObject.characterState.isAttacking ? "yes" : "no") + "\n" +
				"Attacked: " + (characterObject.characterState.isAttacked ? "yes" : "no") + "\n" +
				"Burning: " + (characterObject.characterState.isBurning ? "yes" : "no") + "\n" +
				"Burnt: " + (characterObject.characterState.isBurnt ? "yes" : "no") + "\n" +
				"Shivering: " + (characterObject.characterState.isShivering ? "yes" : "no") + "\n" +
				"Freezing: " + (characterObject.characterState.isFreezing ? "yes" : "no") + "\n" +
				"Frozen: " + (characterObject.characterState.isFrozen ? "yes" : "no") + "\n" +
				"Wet: " + (characterObject.characterState.isWet ? "yes" : "no") + "\n" +
				"Poisoned: " + (characterObject.characterState.isPoisoned ? "yes" : "no") + "\n" +
				"Is breathable env.: " + (characterObject.characterState.isInBreathableEnvironment ? "yes" : "no") + "\n" +
				"Death: " + (characterObject.characterState.isDeath ? "yes" : "no");

			// Timers
			timersText.text = "Timers\n" +
				"Poisoned: " + characterObject.characterState.poisonedRemaining + "\n" +
				"Wet: " + characterObject.characterState.wetRemaining + "\n" +
				"Burning: " + characterObject.characterState.burningRemaining;
		}
	}
}