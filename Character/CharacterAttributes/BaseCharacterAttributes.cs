/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Character.CharacterAttributes {
	public abstract class BaseCharacterAttributes {
		/***** General *****/
		public CharacterPredation predation = CharacterPredation.None;
		
		/***** Physics *****/
		public abstract float weight { get; }
		
		/***** Health *****/
		public abstract float health { get; }
		public abstract float healthDeltaIdle { get; } // how much positive health you get when doing nothing, like a regeneration
		public abstract float healthDeltaZeroFoodDamage { get; }  // when the food is zero, how much damage will the health take?
		public abstract float healthDeltaZeroWaterDamage { get; }
		public abstract float healthDeltaZeroWarmthDamage { get; }

		/***** Stamina *****/
		public abstract float stamina { get; }
		public abstract float staminaDeltaIdle { get; }
		public abstract float staminaDeltaClimbing { get; }
		public abstract float staminaDeltaWalking { get; }
		public abstract float staminaDeltaRunning { get; }
		public abstract float staminaDeltaSprinting { get; }

		/***** Food *****/
		public abstract float food { get; }
		public abstract float foodDeltaIdle { get; }
		public abstract float foodDeltaClimbing{ get; }
		public abstract float foodDeltaWalking { get; }
		public abstract float foodDeltaRunning { get; }
		public abstract float foodDeltaSprinting { get; }

		/***** Water *****/
		public abstract float water { get; }
		public abstract float waterDeltaIdle { get; }
		public abstract float waterDeltaClimbing{ get; }
		public abstract float waterDeltaWalking { get; }
		public abstract float waterDeltaRunning { get; }
		public abstract float waterDeltaSprinting { get; }

		/***** Warmth *****/
		public abstract float warmth { get; }
		public abstract float warmthDeltaIdle { get; } // How much it increases when in idle state (but will be mostly dependent on the temperature).
		public abstract float warmthDeltaClimbing { get; }
		public abstract float warmthDeltaWalking { get; } // May get decreased (positive), because you are moving and the blood flows
		public abstract float warmthDeltaRunning { get; }
		public abstract float warmthDeltaSprinting { get; }

		/***** Awake *****/
		public abstract float awake { get; }
		public abstract float awakeDeltaIdle { get; }
		public abstract float awakeDeltaClimbing { get; }
		public abstract float awakeDeltaWalking { get; } // May get decreased (positive), because you are moving and the blood flows
		public abstract float awakeDeltaRunning { get; }
		public abstract float awakeDeltaSprinting { get; }

		/***** Oxygen *****/
		public abstract float oxygen { get; }
		public abstract float oxygenDeltaBreathable { get; } // when the character can breath normally
		public abstract float oxygenDeltaNonBreathable { get; } // when the character can't breathe (for example, in watter)

		/***** Body temperature *****/
		public abstract float bodyTemperature { get; }
		public abstract float bodyTemperatureHypothermia { get; }
		public abstract float bodyTemperatureHyperthermia { get; }

		/***** Ambient temperature *****/
		public abstract float ambientTemperatureMin { get; } // after that temperature, the body temperature starts to lower
		public abstract float ambientTemperatureMax { get; } // after that temperature, staminaDelta & waterDelta starts to fall / rise

		/***** Speed *****/
		public abstract float speedCrouching { get; }
		public abstract float speedClimbing { get; }
		public abstract float speedWalking { get; }
		public abstract float speedRunning { get; }
		public abstract float speedSprinting { get; }

		/***** Jump *****/
		public abstract float jumpForce { get; }
		public abstract float fallHeightMax { get; } // from which height the character can fall, without any injury?

		/***** Attack *****/
		public abstract float attack { get; }
		public abstract float attackDeltaTime { get; }

		/***** Statuses *****/
		public abstract bool canClimb { get; }
		public abstract bool canJump { get; }
		public abstract bool canFly { get; }
		public abstract bool canSwim { get; }
	}
}
