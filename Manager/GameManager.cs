using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.UI;
using Game.Manager;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Manager {
	public class GameManager : MonoBehaviour {
		private LevelPreloader levelPreloader;
		private AudioSource uiAudioSource;

		void Awake () {
			/***** Level Preloader ******/
			GameObject levelPreloaderGameObject = GameObject.Find ("UI/Canvas/Content/LevelPreloader");
			if (!levelPreloaderGameObject) {
				Debug.LogError ("You need to add the Prefabs/UI/LevelPreloader prefab insde your scene!");
				return;
			}
			levelPreloader = levelPreloaderGameObject.GetComponent<LevelPreloader> ();

			/***** Audio Source ******/
			GameObject uiAudioSourceGameObject = GameObject.Find ("UI/AudioSource");
			if (!uiAudioSourceGameObject) {
				Debug.LogError ("You need to add the Prefabs/UI/AudioSource prefab insde your scene!");
				return;
			}
			uiAudioSource = uiAudioSourceGameObject.GetComponent<AudioSource> ();

			/***** Options *****/
		}

		public void newGame () {
			levelPreloader.loadLevel (InGameLevel.Forest);
		}

		public void loadGame (string savedGameFileName) {
			// TODO
		}

		public void saveGame (string saveGameFileName) {
			// TODO
		}

		public void getSavedGames() {
			// TODO
		}

		public void loadLevel (string level) {
			levelPreloader.loadLevel (level);
		}
	}
}