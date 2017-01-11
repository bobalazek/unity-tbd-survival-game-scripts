using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Core;
using Game.Shared;
using Game.Manager;
using Game.UI;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI.Menu {
	public class PauseMenu : MonoBehaviour {
		private SharedMenuSounds sharedMenuSounds;
		private ModalPanel modalPanel;
		private OptionsMenu optionsMenu;

		void Start () {
			/***** Shared Menu Sounds ******/
			GameObject sharedMenuSoundsGameObject = GameObject.Find ("Shared/MenuSounds");
			if (!sharedMenuSoundsGameObject) {
				Debug.LogError ("You need to add the Prefabs/Shared/MenuSounds prefab insde your scene!");
				return;
			}
			sharedMenuSounds = sharedMenuSoundsGameObject.GetComponent<SharedMenuSounds> ();

			/***** Modal Panel *****/
			GameObject modalPanelGameObject = GameObject.Find ("UI/Canvas/Content/ModalPanel");
			if (!modalPanelGameObject) {
				Debug.LogError ("You need to add the Prefabs/UI/ModalPanel prefab insde your scene!");
				return;
			}
			modalPanel = modalPanelGameObject.GetComponent<ModalPanel> ();

			/***** Options menu *****/
			GameObject optionsMenuGameObject = GameObject.Find ("UI/Canvas/Content/OptionsMenu");
			if (!optionsMenuGameObject) {
				Debug.LogError ("You need to add the Prefabs/UI/Menus/OptionsMenu prefab insde your scene!");
				return;
			}
			optionsMenu = optionsMenuGameObject.GetComponent<OptionsMenu> ();
		}

		void Update () {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				/***** Modal panel *****/
				modalPanel.hide ();

				/***** Options menu *****/
				optionsMenu.hide ();
			}
		}

		public void gamePause () {
			gameObject.SetActive (true);
			// TODO: stop timer
		}

		public void gameResume () {
			gameObject.SetActive (false);
			// TODO: continue timer
		}
		
		/***** Triggers *****/
		public void onClickResumeGameButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked PauseMenu - Resume Game Button");
			}

			gameResume ();
		}

		public void onClickRestartGameButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked PauseMenu - Restart Game Button");
			}

			// TODO
		}

		public void onClickLoadGameButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked PauseMenu - Load Game Button");
			}

			// TODO
		}

		public void onClickSaveGameButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked PauseMenu - Save Game Button");
			}

			// TODO
		}

		public void onClickOptionsButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked PauseMenu - Options Button");
			}

			optionsMenu.show ();
		}

		public void onClickBackToMainMenuButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked PauseMenu - Back to Main Menu Button");
			}

			modalPanel.init (
				"Back to Main Menu",
				"Are you sure you want to go back to the main menu? All progress will be lost.",
				confimCallback,
				cancelCallback
			);
		}

		/***** Callbacks *****/
		private void cancelCallback() {
			modalPanel.hide ();
		}

		private void confimCallback() {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Going to main menu ...");
			}

			modalPanel.hide ();

			SceneManager.LoadScene("10_MainMenu");
		}
	}
}