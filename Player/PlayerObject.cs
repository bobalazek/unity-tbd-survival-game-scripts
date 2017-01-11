using System;
using UnityEngine;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Player {
	[Serializable]
	public class PlayerObject : MonoBehaviour {
		private string playerName = "Player";
		private int playerExperiencePoints = 0;
		private int playerLevel = 0;
	}
}