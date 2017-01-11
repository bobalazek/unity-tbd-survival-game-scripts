using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI {
	public class LevelPreloader : MonoBehaviour {
		private AsyncOperation async;
		private Slider loadingSlider;

		void Awake () {
			loadingSlider = GameObject.Find ("Content/LoadingSlider").GetComponent<Slider> ();
		}

		public void loadLevel (string level) {
			level = "Game/Scenes/Levels/" + level; // Not really sure why the "Game/Scenes/" prefix needs to be there ...

			gameObject.SetActive (true);
			StartCoroutine (LoadLevel (level));
		}

		IEnumerator LoadLevel (string level) {
			async = Application.LoadLevelAsync(level);
			while (!async.isDone) {
				loadingSlider.value = async.progress;
				yield return null;
			}
		}
	}
}