using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Player;
using Game.Character;
using Game.UI.Menu;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Manager {
	public class InGameManager : MonoBehaviour {
		private GameManager gameManager;

		private PauseMenu pauseMenu;
		private bool isGamePaused = false;

		private PlayerController playerController;

		private InGameMode mode;
		private string level; // InGameLevel
		
		void Awake () {
			/***** Game Manager ******/
			GameObject gameManagerGameObject = GameObject.Find ("Shared/GameManager");
			if (!gameManagerGameObject) {
				Debug.LogError ("You need to add the Prefabs/Shared/GameManager prefab insde your scene!");
				return;
			}
			gameManager = gameManagerGameObject.GetComponent<GameManager> ();

			/***** Pause Menu ******/
			GameObject pauseMenuGameObject = GameObject.Find ("UI/Canvas/Content/PauseMenu");
			if (!pauseMenuGameObject) {
				Debug.LogError ("You need to add the Prefabs/UI/Menus/PauseMenu prefab insde your scene!");
				return;
			}
			pauseMenu = pauseMenuGameObject.GetComponent<PauseMenu> ();

			/***** Cursor *****/
			cursorLock ();

			/***** Player controller *****/
			GameObject playerGameObject = GameObject.Find ("Player");
			if (playerGameObject) {
				playerController = playerGameObject.GetComponent<PlayerController> ();
			} else {
				Debug.Log ("No player object found in the scene.");
			}
		}

		void Update () {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				bool isAnySubMenuOpen = false; // check if any submenu (such as: load game, save game or options) is open, then DO NOT close the pause menu!

				GameObject modalPanelGameObject = GameObject.Find ("UI/Canvas/Content/ModalPanel");
				GameObject optionsMenuGameObject = GameObject.Find ("UI/Canvas/Content/OptionsMenu");

				if (
					modalPanelGameObject.activeSelf ||
					optionsMenuGameObject.activeSelf
				) {
					isAnySubMenuOpen = true;
				}

				if (!isAnySubMenuOpen) {
					if (isGamePaused) {
						gameResume ();
					} else {
						gamePause ();
					}

					isGamePaused = !isGamePaused;
				}
			}
		}

		/***** Actions *****/
		void gameResume () {
			pauseMenu.gameResume ();
			cursorLock ();
			Time.timeScale = 1;

			if (playerController) {
				playerController.enabled = true;
			}

			if (Config.DEBUG_MODE) {
				Debug.Log ("Game resumed.");
			}
		}

		void gamePause () {
			pauseMenu.gamePause ();
			cursorUnlock ();
			Time.timeScale = 0;

			if (playerController) {
				playerController.enabled = false;
			}

			if (Config.DEBUG_MODE) {
				Debug.Log ("Game paused.");
			}

			// TODO: maybe, at this point, we could do garbage collection?
		}

		/***** Cursor *****/
		// TODO: Not really sure why unity freezes ...
		void cursorLock () {
			//Cursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;
		}

		void cursorUnlock () {
			//Cursor.lockState = CursorLockMode.None;
			//Cursor.visible = true;
		}
	}
}