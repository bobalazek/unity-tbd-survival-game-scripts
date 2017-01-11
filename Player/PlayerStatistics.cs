using System;
using UnityEngine;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Player {
	[Serializable]
	public class PlayerStatistics {
		public int wasKilled; // TODO: should be a list with the datetime, ingamemode, character that killed you, ...
		public int hasKilled; // TODO: same as above, but the opposite
		public int attacks; // TODO: Should save all the attacks you did to other characters
		public int playSessions; // TODO: sessionId, dateTimeStart, dateTimeEnd, minutes, ...; should always set the timeEnd +1 minute every minute, in case of a crash
		public int playTime; //TODO: in minutes
	}
}