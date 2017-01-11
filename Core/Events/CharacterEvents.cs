/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Core.Events {
	public static class CharacterEvents {
		public const string Died = "character.died";
		public const string WasAttacked = "character.was_attacked";
		public const string HasAttacked = "character.has_attacked";
	}
}