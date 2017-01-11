using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Game.Core;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Manager {
	public class OptionsManager {
		private static Options options;

		private static string optionsFilePath;
		private static BinaryFormatter binaryFormatter = new BinaryFormatter ();

		static OptionsManager () {
			options = new Options ();

			optionsFilePath = Application.persistentDataPath + "/options.dat";

			reloadOptions ();
		}

		public static Options getOptions () {
			return options;
		}

		public static void reloadOptions () {
			options = new Options ();

			if (File.Exists (optionsFilePath)) {
				try {
					FileStream file = File.Open (optionsFilePath, FileMode.Open);
					options = (Options)binaryFormatter.Deserialize (file);
					file.Close ();
				} catch (Exception e)  {
					// set the default options back
					options = new Options ();
				}
			}
		}

		public static void saveOptions (Options data) {
			FileStream file = File.Create (optionsFilePath);
			
			binaryFormatter.Serialize (file, data);
			file.Close ();

			reloadOptions ();
		}

		public static void apply () {
			/*** Video ***/
			/* Fullscreen */
			bool fullscreen = options.videoFullscreen == 1;
			Screen.fullScreen = fullscreen;

			/* Resolution */
			// TODO
			int resolutionWidth = 800;
			int resolutionHeight = 600;
			bool resolutionFullscreen = Screen.fullScreen;
			int resolutionPreferredRefreshRate = 60;
			/*Screen.SetResolution(
				resolutionWidth,
				resolutionHeight,
				resolutionFullscreen,
				resolutionPreferredRefreshRate
			);*/

			/* Texture quality */
			int textureQuality = options.videoTextureQuality;
			QualitySettings.masterTextureLimit = textureQuality;

			/* Anti aliasing */
			int antiAliasing = options.videoAntiAliasing;
			antiAliasing = antiAliasing == 1
				? 2
				: (antiAliasing == 2
					? 4
					: (antiAliasing == 3
						? 8
						: 0));
			QualitySettings.antiAliasing = antiAliasing;

			/* Shadows */
			int shadows = options.videoShadows;
			switch (shadows) {
			case 0:
				QualitySettings.shadows = ShadowQuality.Disable;
				break;
			case 1:
				QualitySettings.shadows = ShadowQuality.HardOnly;
				break;
			case 2:
				QualitySettings.shadows = ShadowQuality.All;
				break;
			}

			/* Shadow Resolution */
			int shadowResolution = options.videoShadowResolution;
			switch (shadowResolution) {
			case 0:
				QualitySettings.shadowResolution = ShadowResolution.Low;
				break;
			case 1:
				QualitySettings.shadowResolution = ShadowResolution.Medium;
				break;
			case 2:
				QualitySettings.shadowResolution = ShadowResolution.High;
				break;
			case 3:
				QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
				break;
			}

			/* VSync */
			int vSync = options.videoVSync;
			QualitySettings.vSyncCount = vSync;
		}
	}
}