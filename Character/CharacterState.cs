using System;
using UnityEngine;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Character {
	[Serializable]
	public class CharacterState {
		/********** General **********/
		/***** Position *****/
		public Vector3 position = Vector3.zero;
		public Vector3 positionLast = Vector3.zero;

		// Temporary fix
		public float positionY = 0;
		public float positionLastY = 0;

		/***** Rotation *****/
		public Quaternion rotation = Quaternion.identity;
		public Quaternion rotationLast = Quaternion.identity;
		
		/***** Motion *****/
		public CharacterMotionState motionState = CharacterMotionState.Idle;
		public Vector3 groundNormal = Vector3.up;

		/***** Predation *****/
		public CharacterPredation predation = CharacterPredation.None;

		/********** Inputs **********/
		public float inputMoveHorizontal = 0f;
		public float inputMoveStrafe = 0f;
		public float inputMoveVertical = 0f;
		public float inputLookMouseX = 0f;
		public float inputLookMouseY = 0f;
		public float inputLookJoystickX = 0f;
		public float inputLookJoystickY = 0f;
		public bool inputCrouch = false;
		public bool inputWalk = false;
		public bool inputRun = false;
		public bool inputSprint = false;
		public bool inputJump = false;
		public bool inputAttack = false;
		public bool inputUse = false;
		public bool inputUseOnce = false;
		public bool inputSwitchCameraPerspective = false; // if it's pressed and hold (GetKey)
		public bool inputSwitchCameraPerspectiveOnce = false; // only the first time it's pressed (GetKeyDown)

		/********** Velocity **********/
		public float velocityX = 0f; // velocity turn (-1 = left; 1 = right)
		public float velocityY = 0f; // velocity vertical (-1 = falling down; 0 = grounded; 1 = jumping up)
		public float velocityZ = 0f; // velocity forward (1 = forward; -1 = backward)

		public Vector3 movement = Vector3.zero;

		/********** Attributes **********/
		/***** Health *****/
		public float health = 100f;
		public float healthDelta = 0f; // for example if the character is on fire, then this would be negative
		public float healthPercentage = 100f;

		/***** Stamina *****/
		public float stamina = 100f;
		public float staminaDelta = 0f;
		public float staminaPercentage = 100f;

		/***** Food *****/
		public float food = 100f; // 100 (not hungry) - 0 (very hungry; starts taking health damage)
		public float foodDelta = 0f;
		public float foodPercentage = 100f;

		/***** Water *****/
		public float water = 100f; // 100 (not thirsty) - 0 (very thirsty; starts taking health damage)
		public float waterDelta = 0f;
		public float waterPercentage = 100f;

		/***** Warmth *****/
		public float warmth = 100f; // 100 (warm) - 0 (very cold; start taking health damage)
		public float warmthDelta = 0f; // depends mostly on the temperature
		public float warmthPercentage = 100f;

		/***** Awake *****/
		public float awake = 100f; // 100 (not sleepy) - 0 (falls asleep)
		public float awakeDelta = 0f;
		public float awakePercentage = 100f;

		/***** Oxygen *****/
		public float oxygen = 100f; // 100 (full) - 0 (empty)
		public float oxygenDelta = 0f;
		public float oxygenPercentage = 100f;

		/***** Body Temperature *****/
		public float bodyTemperature = 37f; // in degrees celsium
		public float bodyTemperatureDelta = 0f;

		/***** Ambient temperature *****/
		public float ambientTemperature = 25f; // in degrees celsium; what's the temperature around the character (is different if character is near fire or inside a shelter)

		/***** Speed *****/
		public float speed = 0f; // The current speed of that character
		public float speedMax = 0f; // depends on the state (walk, run, crouch, ...)
		public float speedMultiplier = 1f; // 0.5 (TBD) - 1; if you are damaged or something, you won't be able to "use" 100% of the speeds anymore
		public float speedRelative = 0f; // relative speed (0 = idle; 0.33 = crouching / climbing; 0.66 = walking; 1 = running)

		/***** Attack *****/
		public float attack = 5f; // the less health / stamina the character has, the less damage it can do
		public float attackMultiplier = 1f; // when you have less stamina or health, you can't give 100% of the damage anymore
		public float attackDeltaTime = 1f; // same as for attack damage. It take more time to attack

		/********** States **********/
		public bool isIdle = true;
		public bool isGrounded = true;
		public bool isAirbourne = false;
		public bool isCrouching = false; // isCrawling
		public bool isWalking = false;
		public bool isRunning = false;
		public bool isSprinting = false;
		public bool isJumping = false;
		public bool isFalling = false;
		public bool isSliding = false;
		public bool isClimbing = false;
        public bool isHanging = false; // TBD; when hanging onto a tree or window; should probably also quickly use stamina. Probably some fast clicking a button, to save themself?
		public bool isSleeping = false;
		public bool isEating = false;
		public bool isAttacking = false;
		public bool isAttacked = false;
		public bool isBurning = false; // if you step on fire
		public bool isBurnt = false;
		public bool isShivering = false;
		public bool isFreezing = false; // maybe then show the freezing shader?
		public bool isFrozen = false;
		public bool isWet = false;
		public bool isPoisoned = false; // TBD
		public bool isInBreathableEnvironment = true; // if the character is NOT under watter (for land animals; opposite for watter animals)
		public bool isDeath = false;

		/********** Timers **********/
		public float poisonedRemaining = 0f; // how long is the character still incapacitated from the poison?
		public float wetRemaining = 0f; // how long will the character still be remaining wet
		public float burningRemaining = 0f; // when running over a fire, the character will still furn for a second (or two)

		/********** Falling **********/
		public float fallYStart = 0f; // at which Y coordinate the character has started falling?
		public float fallDistance = 0f; // when the character jumps & then starts falling, until the character is grounded.
	}
}
