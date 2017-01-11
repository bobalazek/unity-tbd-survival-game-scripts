using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Shared;
using Game.Manager;
using Game.UI;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI.Menu {
	public class MainMenu : MonoBehaviour {
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

			// TODO: Maybe play a background music?
		}

		void Update () {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				/***** Modal panel *****/
				modalPanel.hide ();

				/***** Options menu *****/
				optionsMenu.hide ();
			}
		}

		/***** Triggers *****/
		public void onClickSinglePlayerButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked MainMenu - Single Player Button");
			}

			// TEMPORARY!
			GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager> ();
			gameManager.loadLevel (InGameLevel.FreePlay);
			// TEMPORARY END!
			// TODO
		}

		public void onClickMultiplayerButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked MainMenu - Multiplayer Button");
			}

			// TODO
		}

		public void onClickOptionsButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked MainMenu - Options Button");
			}

			optionsMenu.show ();
		}

		public void onClickExtrasButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked MainMenu - Extras Button");
			}

			// TODO
		}

		public void onClickExitButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Clicked MainMenu - Exit Button");
			}

			modalPanel.init (
				"Exit",
				"Are you sure you want to exit the game?",
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
				Debug.Log ("Exiting game ...");
			}

			modalPanel.hide ();
			
			Application.Quit ();
		}
	}
}