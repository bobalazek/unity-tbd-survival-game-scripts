using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Shared;

namespace Game.UI.Menu.Helpers {
	[RequireComponent(typeof(Image))]
	public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
		private Color32 textColorDefault = new Color32(0x99, 0x99, 0x99, 0xFF);
		private Color32 textColorHover = new Color32(0xFF, 0xFF, 0xFF, 0xFF);

		private Color32 backgroundColorDefault = new Color32(0xFF, 0xFF, 0xFF, 0x00);
		private Color32 backgroundColorHover = new Color32(0xFF, 0xFF, 0xFF, 0x22);

		private Image buttonImage;
		private Text buttonText;

		private SharedMenuSounds menuSounds;
		private AudioSource audioSource;

		void Start () {
			buttonImage = transform.GetComponent<Image> ();
			buttonText = transform.Find ("Text").GetComponent<Text> ();

			// Audio source
			GameObject audioSourceGameObject = GameObject.Find ("UI/AudioSource");
			if (!audioSourceGameObject) {
				Debug.Log ("Whops. Please add the UI/AudioSource prefab inside your UI/ directory in the scene!");
				return;
			}
			audioSource = audioSourceGameObject.GetComponent<AudioSource> ();

			// Menu sounds
			GameObject menuSoundsGameObject = GameObject.Find ("Shared/MenuSounds");
			if (!menuSoundsGameObject) {
				Debug.Log ("Whops. Please add the Shared/MenuSounds prefab inside your Shared/ directory in the scene!");
				return;
			}
			menuSounds = menuSoundsGameObject.GetComponent<SharedMenuSounds> ();
		}

		public void OnPointerEnter (PointerEventData eventData) {
			buttonImage.color = backgroundColorHover;
			buttonText.color = textColorHover;

			if (
				audioSource &&
				menuSounds &&
				menuSounds.buttonSoundEnter
			) {
				audioSource.PlayOneShot (
					menuSounds.buttonSoundEnter
				);
			}
		}

		public void OnPointerExit (PointerEventData eventData) {
			buttonImage.color = backgroundColorDefault;
			buttonText.color = textColorDefault;

			if (
				audioSource &&
				menuSounds &&
				menuSounds.buttonSoundEnter
			) {
				audioSource.PlayOneShot (
					menuSounds.buttonSoundExit
				);
			}
		}

		public void OnPointerClick (PointerEventData eventData) {
			if (
				audioSource &&
				menuSounds &&
				menuSounds.buttonSoundClick
			) {
				audioSource.PlayOneShot (
					menuSounds.buttonSoundClick
				);
			}
		}
	}
}