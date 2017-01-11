/**n
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Character.CharacterAttributes {
	public class CatCharacterAttributes : BaseCharacterAttributes {
		/***** General *****/
		public CharacterPredation predation = CharacterPredation.None;

		/***** Physics *****/
		public override float weight { get { return 5f; } }

		/***** Health *****/
		public override float health { get { return 100f; } }
		public override float healthDeltaIdle { get { return 0.05f; } }
		public override float healthDeltaZeroFoodDamage { get { return 1f; } }
		public override float healthDeltaZeroWaterDamage { get { return 1f; } }
		public override float healthDeltaZeroWarmthDamage { get { return 1f; } }

		/***** Stamina *****/
		public override float stamina { get { return 100f; } }
		public override float staminaDeltaIdle { get { return 0.05f; } }
		public override float staminaDeltaClimbing { get { return -0.05f; } }
		public override float staminaDeltaWalking { get { return -0.005f; } }
		public override float staminaDeltaRunning { get { return -0.05f; } }
		public override float staminaDeltaSprinting { get { return -0.1f; } }

		/***** Food *****/
		public override float food { get { return 100f; } }
		public override float foodDeltaIdle { get { return -0.01f; } }
		public override float foodDeltaClimbing { get { return -0.05f; } }
		public override float foodDeltaWalking { get { return -0.005f; } }
		public override float foodDeltaRunning { get { return -0.01f; } }
		public override float foodDeltaSprinting { get { return -0.2f; } }

		/***** Water *****/
		public override float water { get { return 100f; } }
		public override float waterDeltaIdle { get { return -0.01f; } }
		public override float waterDeltaClimbing { get { return -0.05f; } }
		public override float waterDeltaWalking { get { return -0.02f; } }
		public override float waterDeltaRunning { get { return -0.1f; } }
		public override float waterDeltaSprinting { get { return -0.2f; } }

		/***** Warmth *****/
		public override float warmth { get { return 100f; } }
		public override float warmthDeltaIdle { get { return 0f; } }
		public override float warmthDeltaClimbing { get { return 0.02f; } }
		public override float warmthDeltaWalking { get { return 0f; } }
		public override float warmthDeltaRunning { get { return 0.05f; } }
		public override float warmthDeltaSprinting { get { return 0.1f; } }

		/***** Awake *****/
		public override float awake { get { return 100f; } }
		public override float awakeDeltaIdle { get { return 0.01f; } }
		public override float awakeDeltaClimbing { get { return 0.05f; } }
		public override float awakeDeltaWalking { get { return 0.02f; } }
		public override float awakeDeltaRunning { get { return 0.1f; } }
		public override float awakeDeltaSprinting { get { return 0.2f; } }

		/***** Oxygen *****/
		public override float oxygen { get { return 100f; } }
		public override float oxygenDeltaBreathable { get { return 5f; } }
		public override float oxygenDeltaNonBreathable { get { return 5f; } }

		/***** Body temperature *****/
		public override float bodyTemperature { get { return 37.5f; } }
		public override float bodyTemperatureHypothermia { get { return 28f; } }
		public override float bodyTemperatureHyperthermia { get { return 40f; } }

		/***** Ambient temperature *****/
		public override float ambientTemperatureMin { get { return 16f; } }
		public override float ambientTemperatureMax { get { return 32f; } }

		/***** Speed *****/
		public override float speedCrouching { get { return 0.5f; } }
		public override float speedClimbing { get { return 0.5f; } }
		public override float speedWalking { get { return 1f; } }
		public override float speedRunning { get { return 2f; } }
		public override float speedSprinting { get { return 5f; } }

		/***** Jump & fall *****/
		public override float jumpForce { get { return 2f; } }
		public override float fallHeightMax { get { return 20f; } }

		/***** Attack *****/
		public override float attack { get { return 5f; } }
		public override float attackDeltaTime { get { return 1f; } }

		/***** Statuses *****/
		public override bool canClimb { get { return true; } }
		public override bool canJump { get { return true; } }
		public override bool canFly { get { return false; } }
		public override bool canSwim { get { return false; } }
	}
}
