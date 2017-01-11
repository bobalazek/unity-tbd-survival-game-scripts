using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.Character;

/**
 * Should be thrown in directly into the root of the scene (after the actual camera)!
 * 
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI.InGame {
	public class HUD : MonoBehaviour {
		private CharacterObject characterObject;

		// Health
		private Image healthBar;
		private Text healthText;

		// Stamina
		private Image staminaBar;
		private Text staminaText;

		// Food
		private Image foodBar;
		private Text foodText;

		// Water
		private Image waterBar;
		private Text waterText;

		// Warmth
		private Image warmthBar;
		private Text warmthText;

		// Awake
		private Image awakeBar;
		private Text awakeText;

		// Oxygen
		private Image oxygenBar;
		private Text oxygenText;

		void Start () {
			GameObject playerGameObject = GameObject.Find ("Player");
			if (!playerGameObject) {
				gameObject.SetActive (false);
				Debug.Log ("No player chaarcter found. HUD will be disabed.");
				return;
			}

			characterObject = playerGameObject.GetComponent<CharacterObject> ();

			string prefix = "HUD/Content/";
			string barSuffix = "/BarWrapper/Bar";
			string textSuffix = "/BarWrapper/Bar/Text";

			// Health
			healthBar = GameObject.Find (prefix + "Health" + barSuffix).GetComponent<Image> ();
			healthText = GameObject.Find (prefix + "Health" + textSuffix).GetComponent<Text> ();

			// Stamina
			staminaBar = GameObject.Find (prefix + "Stamina" + barSuffix).GetComponent<Image> ();
			staminaText = GameObject.Find (prefix + "Stamina" + textSuffix).GetComponent<Text> ();

			// Food
			foodBar = GameObject.Find (prefix + "Food" + barSuffix).GetComponent<Image> ();
			foodText = GameObject.Find (prefix + "Food" + textSuffix).GetComponent<Text> ();

			// Water
			waterBar = GameObject.Find (prefix + "Water" + barSuffix).GetComponent<Image> ();
			waterText = GameObject.Find (prefix + "Water" + textSuffix).GetComponent<Text> ();

			// Warmth
			warmthBar = GameObject.Find (prefix + "Warmth" + barSuffix).GetComponent<Image> ();
			warmthText = GameObject.Find (prefix + "Warmth" + textSuffix).GetComponent<Text> ();

			// Awake
			awakeBar = GameObject.Find (prefix + "Awake" + barSuffix).GetComponent<Image> ();
			awakeText = GameObject.Find (prefix + "Awake" + textSuffix).GetComponent<Text> ();

			// Oxygen
			oxygenBar = GameObject.Find (prefix + "Oxygen" + barSuffix).GetComponent<Image> ();
			oxygenText = GameObject.Find (prefix + "Oxygen" + textSuffix).GetComponent<Text> ();
		}

		void FixedUpdate () {
			if (!characterObject) {
				// No player chaarcter found? Nothing to do
				return;
			}

			// Health
			healthBar.fillAmount = characterObject.characterState.healthPercentage / 100;
			healthText.text = numberFormat (characterObject.characterState.healthPercentage);

			// Stamina
			staminaBar.fillAmount = characterObject.characterState.staminaPercentage / 100;
			staminaText.text = numberFormat (characterObject.characterState.staminaPercentage);

			// Food
			foodBar.fillAmount = characterObject.characterState.foodPercentage / 100;
			foodText.text = numberFormat (characterObject.characterState.foodPercentage);

			// Water
			waterBar.fillAmount = characterObject.characterState.waterPercentage / 100;
			waterText.text = numberFormat (characterObject.characterState.waterPercentage);

			// Warmth
			warmthBar.fillAmount = characterObject.characterState.warmthPercentage / 100;
			warmthText.text = numberFormat (characterObject.characterState.warmthPercentage);

			// Awake
			awakeBar.fillAmount = characterObject.characterState.awakePercentage / 100;
			awakeText.text = numberFormat (characterObject.characterState.awakePercentage);

			// Oxygen
			oxygenBar.fillAmount = characterObject.characterState.oxygenPercentage / 100;
			oxygenText.text = numberFormat (characterObject.characterState.oxygenPercentage);
		}

		private string numberFormat (float number) {
			return ((int) number).ToString();
		}
	}
}