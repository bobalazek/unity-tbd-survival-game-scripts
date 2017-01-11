using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Manager;
using Game.Character;
using Game.Character.CharacterAttributes;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Character {
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(AudioSource))]
	public class CharacterObject : MonoBehaviour {
		private Transform character;
		private Transform characterBase; // the bottom base of the object
		[HideInInspector] public Rigidbody characterRigidbody;
		private Collider characterCollider;
		private Animator characterAnimator;
		
		[HideInInspector] public CharacterState characterState;
		public CharacterSpecies characterSpecies;
		[HideInInspector] public BaseCharacterAttributes characterAttributes;

		private float groundCheckDistance = 0.2f; // MUST be more then 0.01f, because that's the offset, that makes sure that the ray starts from inside the character
		private float groundCheckDistanceOffset = 0.01f; // the offset it goes into the character, to raycast from there downside
		private float fallingThreshold = 0.1f; // consider it falling, when the velocityY is more then this
		private float velocityYThreshold = 0.1f; // normalize velocity Y; if it's more then this value, then it's 1, if it's less then this (negative) value, then it's -1. Else it's 0

		void Awake () {
			character = transform;
			characterRigidbody = GetComponent<Rigidbody> ();
			characterCollider = GetComponent<Collider> ();
			characterAnimator = GetComponent<Animator> ();
			characterState = new CharacterState ();

			/***** Collider *****/
			if (!characterCollider) {
				Debug.LogError ("Whops. Seems that no collider was found. Please add a collider to the character!");
				return;
			}

			/***** Constrains *****/
			characterRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

			/***** Base *****/
			characterBase = character;
			Transform characterBaseTmp = character.Find ("CharacterBase");
			if (characterBaseTmp) {
				Debug.Log ("A CharacterBase element was found inside the character. That will be used for the character base from now on.");
				characterBase = characterBaseTmp;
			}
			
			/***** Species *****/
			if (!Enum.IsDefined (typeof(CharacterSpecies), characterSpecies)) {
				Debug.LogError ("The species for this character is invalid.");
				return;
			}

			/***** Attributes *****/
			switch (characterSpecies) {
				case CharacterSpecies.Cat:
					characterAttributes = new CatCharacterAttributes ();
					break;
			}
			
			/***** Character state *****/
			initializeState ();
		}

		void FixedUpdate () {
			// Needs to happen in FixedUpdate, 
			//   because that's after the physics stuff is done.
			updateState ();
		}

		/***** State *****/
		void initializeState () {
			characterState.health = characterAttributes.health;
			characterState.stamina = characterAttributes.stamina;
			characterState.food = characterAttributes.food;
			characterState.water = characterAttributes.water;
			characterState.warmth = characterAttributes.warmth;
			characterState.awake = characterAttributes.awake;
			characterState.oxygen = characterAttributes.oxygen;

			updateState ();
		}

		void updateState () {
			/***** General *****/
			float environmentTemperature = EnvironmentManager.temperature;

			/***** Position & rotation *****/
			updateStatePositionAndRotation ();

			/***** Velocity *****/
			characterState.velocityX = characterState.inputMoveHorizontal;
			characterState.velocityY = characterRigidbody.velocity.y > velocityYThreshold
				? 1
				: (characterRigidbody.velocity.y < -velocityYThreshold
					? -1
					: 0);
			characterState.velocityZ = characterState.inputMoveVertical;

			// TODO: Prevent sliding (RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY), if we are sliding down on a very low slope

			/***** Ambient temperature *****/
			// TODO: Calculate how much is the ambient temperature of the character (maybe a fire nearby, or inside a shelter)?

			/***** Statuses *****/
			updateStateStatuses ();

			/***** Motion state *****/
			updateStateMotion ();

			/***** Speed *****/
			updateStateSpeed ();

			/***** Deltas *****/
			updateStateDeltas ();

			/***** Percentages *****/
			updateStatePercentages ();

			/***** Animator *****/
			// updateAnimator (); // TODO: Not yet used
		}

		void updateStatePositionAndRotation () {
			characterState.positionLast = characterState.position;
			characterState.position = transform.position;
			characterState.rotationLast = characterState.rotation;
			characterState.rotation = transform.rotation;

			// Temporary fix
			characterState.positionLastY = characterState.positionY;
			characterState.positionY = character.position.y;
		}

		void updateStateStatuses () {
			/* Idle */
			characterState.isIdle = characterState.speed == 0;

			/* Grounded */
			RaycastHit hitInfo;
			// The groundCheckDistanceOffset (0.01f) is just a tiny offset, to make sure, that the ray will start from inside the character
			if (Physics.Raycast (
				    characterBase.position + (characterBase.transform.up * groundCheckDistanceOffset),
				    -characterBase.transform.up,
				    out hitInfo,
				    groundCheckDistance + groundCheckDistanceOffset
			)) {
				characterState.isGrounded = true;
				characterState.groundNormal = hitInfo.normal;
			} else {
				characterState.isGrounded = false;
				characterState.groundNormal = Vector3.up;
			}

			/* Airbourne */
			characterState.isAirbourne = !characterState.isGrounded;

			/* Crouching */
			characterState.isCrouching = characterState.inputCrouch;

			/* Walking */
			characterState.isWalking = characterState.speed > 0 && (characterState.inputWalk || characterState.isCrouching);

			/* Running */
			// the default, non-idle motion state
			characterState.isRunning = characterState.speed > 0 && !characterState.inputWalk && !characterState.inputSprint && !characterState.isCrouching;

			/* Sprinting */
			characterState.isSprinting = characterState.speed > 0 && !characterState.inputWalk && characterState.inputSprint && !characterState.isCrouching;

			/* Jumping */
			characterState.isJumping = characterState.velocityY > 0;

			/* Falling */
			characterState.isFalling = characterState.velocityY < -fallingThreshold;

			/* Climbing */
			characterState.isClimbing = false; // TODO

			/* Hanging */
			characterState.isHanging = false; // TODO

			/* Sleeping */
			characterState.isSleeping = false; // TODO

			/* Eating */
			characterState.isEating = false; // TODO

			/* Attacking */
			characterState.isAttacking = characterState.inputAttack;

			/* Attacked */
			characterState.isAttacked = false; // TODO

			/* Burning */
			characterState.isBurning = false; // TODO

			/* Burnt */
			characterState.isBurnt = false; // TODO

			/* Shivering */
			characterState.isShivering = false; // TODO

			/* Freezing */
			characterState.isFreezing = false; // TODO

			/* Frozen */
			characterState.isFrozen = false; // TODO

			/* Wet */
			characterState.isWet = false; // TODO

			/* Poisoned */
			characterState.isPoisoned = false; // TODO

			/* In breathable environment */
			characterState.isInBreathableEnvironment = true; // TODO

			/* Death */
			characterState.isDeath = characterState.health <= 0;
		}

		void updateStateMotion () {
			characterState.motionState = CharacterMotionState.Idle;

			if (characterState.isAttacking) {
				characterState.motionState = CharacterMotionState.Attacking;
			} else if (
				!characterState.isIdle &&
				characterState.isCrouching
			) {
				characterState.motionState = CharacterMotionState.CrouchWalking;
			} else if (characterState.isCrouching) {
				characterState.motionState = CharacterMotionState.Crouching;
			} else if (characterState.isWalking) {
				characterState.motionState = CharacterMotionState.Walking;
			} else if (characterState.isRunning) {
				characterState.motionState = CharacterMotionState.Running;
			} else if (characterState.isSprinting) {
				characterState.motionState = CharacterMotionState.Sprinting;
			}
			// TODO: add all motion states
		}

		void updateStateSpeed () {
			// Speed - get the CURRENT speed of the character
			characterState.speed = characterState.movement.magnitude;

			// Speed max - how fast can the character maximal move?
			characterState.speedMax = characterAttributes.speedRunning; // default walking motion
			switch (characterState.motionState) {
				case CharacterMotionState.Attacking:
					characterState.speedMax = 0; // TBD
					break;
				case CharacterMotionState.Crouching:
				case CharacterMotionState.CrouchWalking:
					characterState.speedMax = characterAttributes.speedCrouching;
					break;
				case CharacterMotionState.Walking:
					characterState.speedMax = characterAttributes.speedWalking;
					break;
				case CharacterMotionState.Sprinting:
					characterState.speedMax = characterAttributes.speedSprinting;
					break;
			}

			characterState.speedMax *= characterState.speedMultiplier;
		}

		void updateStateDeltas () {
			/*** Initial deltas ***/
			characterState.healthDelta = 0.0f;
			characterState.staminaDelta = 0.0f;
			characterState.foodDelta = 0.0f;
			characterState.waterDelta = 0.0f;
			characterState.warmthDelta = 0.0f;
			characterState.awakeDelta = 0.0f;
			characterState.oxygenDelta = 0.0f;

			// TODO: All the magic here ...

			/*** Apply deltas ***/
			characterState.health += characterState.healthDelta * Time.deltaTime;
			characterState.stamina += characterState.staminaDelta * Time.deltaTime;
			characterState.food += characterState.foodDelta * Time.deltaTime;
			characterState.water += characterState.waterDelta * Time.deltaTime;
			characterState.warmth += characterState.warmthDelta * Time.deltaTime;
			characterState.awake += characterState.awakeDelta * Time.deltaTime;
			characterState.oxygen += characterState.oxygenDelta * Time.deltaTime;

			/*** Max / Min limits ***/
			characterState.health = characterState.health < 0 ? 0 : characterState.health;
			characterState.stamina = characterState.health < 0 ? 0 : characterState.stamina;
			characterState.food = characterState.health < 0 ? 0 : characterState.food;
			characterState.water = characterState.health < 0 ? 0 : characterState.water;
			characterState.warmth = characterState.health < 0 ? 0 : characterState.warmth;
			characterState.awake = characterState.health < 0 ? 0 : characterState.awake;
			characterState.oxygen = characterState.health < 0 ? 0 : characterState.oxygen;
		}

		void updateStatePercentages () {
			/* Health */
			characterState.healthPercentage = characterAttributes.health == 0
				? 100
				: (characterState.health == 0
					? 0
					: characterState.health / characterAttributes.health * 100);

			/* Stamina */
			characterState.staminaPercentage = characterAttributes.stamina == 0
				? 100
				: (characterState.stamina == 0
					? 0
					: characterState.stamina / characterAttributes.stamina * 100);

			/* Food */
			characterState.foodPercentage = characterAttributes.food == 0
				? 100
				: (characterState.food == 0
					? 0
					: characterState.food / characterAttributes.food * 100);

			/* Water */
			characterState.waterPercentage = characterAttributes.water == 0
				? 100
				: (characterState.water == 0
					? 0
					: characterState.water / characterAttributes.water * 100);

			/* Warmth */
			characterState.warmthPercentage = characterAttributes.warmth == 0
				? 100
				: (characterState.warmth == 0
					? 0
					: characterState.warmth / characterAttributes.warmth * 100);

			/* Awake */
			characterState.awakePercentage = characterAttributes.awake == 0
				? 100
				: (characterState.awake == 0
					? 0
					: characterState.awake / characterAttributes.awake * 100);

			/* Oxygen */
			characterState.oxygenPercentage = characterAttributes.oxygen == 0
				? 100
				: (characterState.oxygen == 0
					? 0
					: characterState.oxygen / characterAttributes.oxygen * 100);
		}

		void updateAnimator () {
			characterAnimator.SetFloat ("velocityX", characterState.velocityX);
			characterAnimator.SetFloat ("velocityY", characterState.velocityY);
			characterAnimator.SetFloat ("velocityZ", characterState.velocityZ);
			characterAnimator.SetFloat ("speed", characterState.speed);
			characterAnimator.SetFloat ("speedRelative", characterState.speedRelative);
		}
	}
}