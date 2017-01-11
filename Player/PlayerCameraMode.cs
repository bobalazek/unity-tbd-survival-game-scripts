using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Player {
	public enum PlayerCameraMode {
		//Spectator,
		//FirstPerson,
		//Prey, // when the player's character is ready to attack
		//Climb,
		ThirdPerson, // default is orbiting around character
		ThirdPersonBehindBack
	}
}
