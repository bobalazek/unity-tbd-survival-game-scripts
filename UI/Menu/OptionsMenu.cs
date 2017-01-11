using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Shared;
using Game.Manager;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI.Menu {
	public class OptionsMenu : MonoBehaviour {
		private SharedMenuSounds sharedMenuSounds;

		private string selectedGroup;

		private ColorBlock topBarButtonDefaultColors;
		private Color32 topBarButtonDefaultTextColor;

		private Color32 topBarButtonColorActive = new Color32(0x5E, 0x00, 0x00, 0xFF);
		private Color32 topBarButtonTextColorActive = new Color32(0xFF, 0xFF, 0xFF, 0xFF);

		private Resolution[] resolutions;

		/***** Option controls (dropdown & sliders) *****/
		/*** Game options ***/
		private GameObject gameOptionsDifficultyControl;

		/*** Display options ***/
		private GameObject displayOptionsHUDControl;
		private GameObject displayOptionsMapControl;
		private GameObject displayOptionsFPSCounterControl;

		/*** Video options ***/
		private GameObject videoOptionsFullscreenControl;
		private GameObject videoOptionsResolutionControl;
		private GameObject videoOptionsTextureQualityControl;
		private GameObject videoOptionsAntiAliasingControl;
		private GameObject videoOptionsShadowsControl;
		private GameObject videoOptionsShadowResolutionControl;
		private GameObject videoOptionsVSyncControl;

		/*** Audio options ***/
		private GameObject audioOptionsMasterVolumeControl;
		private GameObject audioOptionsMusicVolumeControl;
		private GameObject audioOptionsEffectsVolumeControl;
		private GameObject audioOptionsUIVolumeControl;
		private GameObject audioOptionsInGameVolumeControl;

		/*** Controls options ***/
		private GameObject controlsOptionsInverseLookMouseControl;
		private GameObject controlsOptionsInverseLookJoystickControl;
		private GameObject controlsOptionsLookSensitivityMouseControl;
		private GameObject controlsOptionsLookSensitivityJoystickControl;

		/*** Key Bidnings options ***/
		// TODO

		void Start () {
			/***** Shared Menu Sounds ******/
			GameObject sharedMenuSoundsGameObject = GameObject.Find ("Shared/MenuSounds");
			if (!sharedMenuSoundsGameObject) {
				Debug.LogError ("You need to add the Prefabs/Shared/MenuSounds prefab insde your scene!");
				return;
			}
			sharedMenuSounds = sharedMenuSoundsGameObject.GetComponent<SharedMenuSounds> ();
			
			/***** Option controls (dropdown & sliders) *****/
			/*** Game options ***/
			string gameOptionsPrefix = "ContentWrapper/Content/Body/Contents/GameOptionsMenu/Body/Content";
			gameOptionsDifficultyControl = GameObject.Find (gameOptionsPrefix + "/GameOptionsDifficulty/Control/Dropdown");

			/*** Display options ***/
			string displayOptionsPrefix = "ContentWrapper/Content/Body/Contents/DisplayOptionsMenu/Body/Content";
			displayOptionsHUDControl = GameObject.Find (displayOptionsPrefix + "/DisplayOptionsHUD/Control/Dropdown");
			displayOptionsMapControl = GameObject.Find (displayOptionsPrefix + "/DisplayOptionsMap/Control/Dropdown");
			displayOptionsFPSCounterControl = GameObject.Find (displayOptionsPrefix + "/DisplayOptionsFPSCounter/Control/Dropdown");

			/*** Video options ***/
			string videoOptionsPrefix = "ContentWrapper/Content/Body/Contents/VideoOptionsMenu/Body/Content";
			videoOptionsFullscreenControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsFullscreen/Control/Dropdown");
			videoOptionsResolutionControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsResolution/Control/Dropdown");
			videoOptionsTextureQualityControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsTextureQuality/Control/Dropdown");
			videoOptionsAntiAliasingControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsAntiAliasing/Control/Dropdown");
			videoOptionsShadowsControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsShadows/Control/Dropdown");
			videoOptionsShadowResolutionControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsShadowResolution/Control/Dropdown");
			videoOptionsVSyncControl = GameObject.Find (videoOptionsPrefix + "/VideoOptionsVSync/Control/Dropdown");
			// TODO: Implement blend weights, shadow distance, soft particles, LOD ???

			/*** Audio options ***/
			string audioOptionsPrefix = "ContentWrapper/Content/Body/Contents/AudioOptionsMenu/Body/Content";
			audioOptionsMasterVolumeControl = GameObject.Find (audioOptionsPrefix + "/AudioOptionsMasterVolume/Control/Slider");
			audioOptionsMusicVolumeControl = GameObject.Find (audioOptionsPrefix + "/AudioOptionsMusicVolume/Control/Slider");
			audioOptionsEffectsVolumeControl = GameObject.Find (audioOptionsPrefix + "/AudioOptionsEffectsVolume/Control/Slider");
			audioOptionsUIVolumeControl = GameObject.Find (audioOptionsPrefix + "/AudioOptionsUIVolume/Control/Slider");
			audioOptionsInGameVolumeControl = GameObject.Find (audioOptionsPrefix + "/AudioOptionsInGameVolume/Control/Slider");

			/*** Controls options ***/
			string controlsOptionsPrefix = "ContentWrapper/Content/Body/Contents/ControlsOptionsMenu/Body/Content";
			controlsOptionsInverseLookMouseControl = GameObject.Find (controlsOptionsPrefix + "/ControlsOptionsInverseLookMouse/Control/Dropdown");
			controlsOptionsInverseLookJoystickControl = GameObject.Find (controlsOptionsPrefix + "/ControlsOptionsInverseLookJoystick/Control/Dropdown");
			controlsOptionsLookSensitivityMouseControl = GameObject.Find (controlsOptionsPrefix + "/ControlsOptionsLookSensitivityMouse/Control/Slider");
			controlsOptionsLookSensitivityJoystickControl = GameObject.Find (controlsOptionsPrefix + "/ControlsOptionsLookSensitivityJoystick/Control/Slider");

			/*** Key bindings options ***/
			// TODO

			/***** Other *****/
			// Hydrate resolutions ...
			resolutions = Screen.resolutions;
			// ... & resolutions dropdown.
			foreach (Resolution resolution in resolutions) {
				videoOptionsResolutionControl.GetComponent<Dropdown> ().options.Add (
					new Dropdown.OptionData () { text = resolution.ToString() }
				);
			}

			// Save the default colors, so we can re-set them later.
			Button defaultButton = transform.Find ("ContentWrapper/Content/TopBar/Buttons/GameOptionsButton").GetComponent<Button> ();
			topBarButtonDefaultColors = defaultButton.colors;

			Text defaultButtonText = transform.Find ("ContentWrapper/Content/TopBar/Buttons/GameOptionsButton/Text").GetComponent<Text> ();
			topBarButtonDefaultTextColor = defaultButtonText.color;

			// Set the default group to game
			switchGroup ("game");

			load ();
		}

		void Update () {
			if (Input.GetKey (KeyCode.Escape)) {
				hide ();
			}
		}

		public void hide () {
			gameObject.SetActive (false);
		}

		public void show () {
			gameObject.SetActive (true);
		}

		public void save () {
			Options data = OptionsManager.getOptions();

			/***** Settings data *****/
			/*** Game ***/
			data.gameDifficulty = gameOptionsDifficultyControl.GetComponent<Dropdown> ().value;

			/*** Display ***/
			data.displayHUD = displayOptionsHUDControl.GetComponent<Dropdown> ().value;
			data.displayMap = displayOptionsMapControl.GetComponent<Dropdown> ().value;
			data.displayFPSCounter = displayOptionsFPSCounterControl.GetComponent<Dropdown> ().value;

			/*** Video ***/
			data.videoFullscreen = videoOptionsFullscreenControl.GetComponent<Dropdown> ().value;
			data.videoResolution = videoOptionsResolutionControl.GetComponent<Dropdown> ().value;
			data.videoTextureQuality = videoOptionsTextureQualityControl.GetComponent<Dropdown> ().value;
			data.videoAntiAliasing = videoOptionsAntiAliasingControl.GetComponent<Dropdown> ().value;
			data.videoShadows = videoOptionsShadowsControl.GetComponent<Dropdown> ().value;
			data.videoShadowResolution = videoOptionsShadowResolutionControl.GetComponent<Dropdown> ().value;
			data.videoVSync = videoOptionsVSyncControl.GetComponent<Dropdown> ().value;

			/*** Audio ***/
			data.audioMasterVolume = audioOptionsMasterVolumeControl.GetComponent<Slider> ().value;
			data.audioMusicVolume = audioOptionsMusicVolumeControl.GetComponent<Slider> ().value;
			data.audioEffectsVolume = audioOptionsEffectsVolumeControl.GetComponent<Slider> ().value;
			data.audioUIVolume = audioOptionsUIVolumeControl.GetComponent<Slider> ().value;
			data.audioInGameVolume = audioOptionsInGameVolumeControl.GetComponent<Slider> ().value;

			/*** Controls ***/
			data.controlsInverseLookMouse = controlsOptionsInverseLookMouseControl.GetComponent<Dropdown> ().value;
			data.controlsInverseLookJoystick = controlsOptionsInverseLookJoystickControl.GetComponent<Dropdown> ().value;
			data.controlsLookSensitivityMouse = controlsOptionsLookSensitivityMouseControl.GetComponent<Slider> ().value;
			data.controlsLookSensitivityJoystick = controlsOptionsLookSensitivityJoystickControl.GetComponent<Slider> ().value;

			OptionsManager.saveOptions (data);

			if (Config.DEBUG_MODE) {
				Debug.Log ("Options saved.");
			}
		}

		public void load () {
			Options data = OptionsManager.getOptions();

			/***** Getting data *****/
			/*** Game ***/
			gameOptionsDifficultyControl.GetComponent<Dropdown> ().value = data.gameDifficulty;

			/*** Display ***/
			displayOptionsHUDControl.GetComponent<Dropdown> ().value = data.displayHUD;
			displayOptionsMapControl.GetComponent<Dropdown> ().value = data.displayMap;
			displayOptionsFPSCounterControl.GetComponent<Dropdown> ().value = data.displayFPSCounter;

			/*** Video ***/
			videoOptionsFullscreenControl.GetComponent<Dropdown> ().value = data.videoFullscreen;
			videoOptionsResolutionControl.GetComponent<Dropdown> ().value = data.videoResolution;
			videoOptionsTextureQualityControl.GetComponent<Dropdown> ().value = data.videoTextureQuality;
			videoOptionsAntiAliasingControl.GetComponent<Dropdown> ().value = data.videoAntiAliasing;
			videoOptionsShadowsControl.GetComponent<Dropdown> ().value = data.videoShadows;
			videoOptionsShadowResolutionControl.GetComponent<Dropdown> ().value = data.videoShadowResolution;
			videoOptionsVSyncControl.GetComponent<Dropdown> ().value = data.videoVSync;

			/*** Audio ***/
			audioOptionsMasterVolumeControl.GetComponent<Slider> ().value = data.audioMasterVolume;
			audioOptionsMusicVolumeControl.GetComponent<Slider> ().value = data.audioMusicVolume;
			audioOptionsEffectsVolumeControl.GetComponent<Slider> ().value = data.audioEffectsVolume;
			audioOptionsUIVolumeControl.GetComponent<Slider> ().value = data.audioUIVolume;
			audioOptionsInGameVolumeControl.GetComponent<Slider> ().value = data.audioInGameVolume;

			/*** Controls ***/
			controlsOptionsInverseLookMouseControl.GetComponent<Dropdown> ().value = data.controlsInverseLookMouse;
			controlsOptionsInverseLookJoystickControl.GetComponent<Dropdown> ().value = data.controlsInverseLookJoystick;
			controlsOptionsLookSensitivityMouseControl.GetComponent<Slider> ().value = data.controlsLookSensitivityMouse;
			controlsOptionsLookSensitivityJoystickControl.GetComponent<Slider> ().value = data.controlsLookSensitivityJoystick;

			apply ();
		}

		public void reset () {
			// TODO; maybe by group?
		}

		public void apply () {
			OptionsManager.apply ();
		}

		public void switchGroup (string group) {
			selectedGroup = group;
			string uppercaseFirstGroup = char.ToUpper(group[0]) + group.Substring(1);

			/***** Top bar *****/
			Transform groupTopBarButtons = transform.Find ("ContentWrapper/Content/TopBar/Buttons");
			Transform groupTopBarButton = transform.Find ("ContentWrapper/Content/TopBar/Buttons/" + uppercaseFirstGroup + "OptionsButton");
			Transform groupTopBarButtonText = transform.Find ("ContentWrapper/Content/TopBar/Buttons/" + uppercaseFirstGroup + "OptionsButton/Text");

			foreach (Transform child in groupTopBarButtons) {
				child.GetComponent<Button> ().colors = topBarButtonDefaultColors;
				child.Find ("Text").GetComponent<Text> ().color = topBarButtonDefaultTextColor;
			}

			// Set all the colors to activr
			ColorBlock groupTopBarButtonColors = groupTopBarButton.GetComponent<Button> ().colors;
			groupTopBarButtonColors.normalColor = topBarButtonColorActive;
			groupTopBarButtonColors.highlightedColor = topBarButtonColorActive;
			groupTopBarButtonColors.normalColor = topBarButtonColorActive;
			groupTopBarButton.GetComponent<Button> ().colors = groupTopBarButtonColors;

			groupTopBarButtonText.GetComponent<Text> ().color = topBarButtonTextColorActive;

			/***** Contents *****/
			Transform groupContents = transform.Find ("ContentWrapper/Content/Body/Contents");
			Transform groupContent = transform.Find ("ContentWrapper/Content/Body/Contents/" + uppercaseFirstGroup + "OptionsMenu");

			foreach (Transform child in groupContents) {
				child.gameObject.SetActive (false);
			}

			groupContent.gameObject.SetActive (true);
		}

		/***** Triggers *****/
		/*** Clicks ***/
		public void onClickCloseButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Options menu close button clicked.");
			}
			
			hide ();
		}

		public void onClickConfirmButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Options menu confirm button clicked.");
			}

			save ();
			hide ();
		}

		public void onClickCancelButton () {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Options menu cancel button clicked.");
			}

			hide ();
		}

		public void onClickTopBarButton (string group) {
			if (Config.DEBUG_MODE) {
				Debug.Log ("Options menu '" + group + "' clicked.");
			}

			switchGroup (group);
		}

		/*** Changes ***/
		/* Game options */
		public void onChangeGameOptionsDifficulty () {
			int value = gameOptionsDifficultyControl.GetComponent<Dropdown> ().value;

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Game Options - Difficulty to " + value + ".");
			}
		}

		/* Display options */
		public void onChangeDisplayOptionsHUD () {
			int value = displayOptionsHUDControl.GetComponent<Dropdown> ().value;

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Display Options - HUD to " + value + ".");
			}
		}

		public void onChangeDisplayOptionsMap () {
			int value = displayOptionsMapControl.GetComponent<Dropdown> ().value;

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Display Options - Map to " + value + ".");
			}
		}

		public void onChangeDisplayOptionsFPSCounter () {
			int value = displayOptionsFPSCounterControl.GetComponent<Dropdown> ().value;

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Display Options - FPS Counter to " + value + ".");
			}
		}

		/* Video options */
		public void onChangeVideoOptionsFullscreen () {
			int value = videoOptionsFullscreenControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - Fullscreen to " + value + ".");
			}
		}

		public void onChangeVideoOptionsResolution () {
			int value = videoOptionsResolutionControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - Resolution to " + value + ".");
			}
		}

		public void onChangeVideoOptionsTextureQuality () {
			int value = videoOptionsTextureQualityControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - Texture Quality to " + value + ".");
			}
		}

		public void onChangeVideoOptionsAntiAliasing () {
			int value = videoOptionsAntiAliasingControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - Anti Aliasing to " + value + ".");
			}
		}

		public void onChangeVideoOptionsShadows () {
			int value = videoOptionsShadowsControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - Shadows to " + value + ".");
			}
		}

		public void onChangeVideoOptionsShadowResolution () {
			int value = videoOptionsShadowResolutionControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - Shadow Resolution to " + value + ".");
			}
		}

		public void onChangeVideoOptionsVSync () {
			int value = videoOptionsVSyncControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Video Options - VSync to " + value + ".");
			}
		}

		/* Audio options */
		public void onChangeAudioOptionsMasterVolume () {
			float value = audioOptionsMasterVolumeControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Audio Options - Master Volume to " + value + ".");
			}
		}

		public void onChangeAudioOptionsMusicVolume () {
			float value = audioOptionsMusicVolumeControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Audio Options - Music Volume to " + value + ".");
			}
		}

		public void onChangeAudioOptionsEffectsVolume () {
			float value = audioOptionsEffectsVolumeControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Audio Options - Effects Volume to " + value + ".");
			}
		}

		public void onChangeAudioOptionsUIVolume () {
			float value = audioOptionsUIVolumeControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Audio Options - UI Volume to " + value + ".");
			}
		}

		public void onChangeAudioOptionsInGameVolume () {
			float value = audioOptionsInGameVolumeControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Audio Options - InGame Volume to " + value + ".");
			}
		}

		/* Controls options */
		public void onChangeControlsOptionsInverseLookMouse () {
			float value = controlsOptionsInverseLookMouseControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Controls Options - Inverse Look - Mouse to " + value + ".");
			}
		}

		public void onChangeControlsOptionsInverseLookJoystick () {
			float value = controlsOptionsInverseLookJoystickControl.GetComponent<Dropdown> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Controls Options - Inverse Look - Joystick to " + value + ".");
			}
		}

		public void onChangeControlsOptionsLookSensitivityMouse () {
			float value = controlsOptionsLookSensitivityMouseControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Controls Options - Look Sensitivity - Mouse to " + value + ".");
			}
		}

		public void onChangeControlsOptionsLookSensitivityJoystick () {
			float value = controlsOptionsLookSensitivityJoystickControl.GetComponent<Slider> ().value;

			apply ();

			if (Config.DEBUG_MODE) {
				Debug.Log ("Changed Controls Options - Look Sensitivity - Joystick to " + value + ".");
			}
		}

		/* Key Bindings */
		// TODO
	}
}