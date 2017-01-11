/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Character {
	public enum CharacterMotionState {
		Idle, // Standing
		Crouching,
		CrouchWalking,
		Walking,
		Running,
		Sprinting,
		Strafing,
		Turning, // TBD
		Jumping,
		Falling,
		Landing,
		Sliding,
		Climbing,
		Attacking,
		Death
	}
}
