using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.UI {
	public delegate void Callback();

	public class ModalPanel : MonoBehaviour {
		public string title;
		public string content;

		public Callback onConfirmCallback;
		public Callback onCancelCallback;

		public void init (string newTitle, string newContent, Callback newOnConfirmCallback, Callback newOnCancelCallback) {
			title = newTitle;
			content = newContent;
			onConfirmCallback += newOnConfirmCallback;
			onCancelCallback += newOnCancelCallback;

			show ();
		}

		public void show () {
			gameObject.transform.Find ("Content/Header/Text").GetComponent<Text>().text = title;
			gameObject.transform.Find ("Content/Body/Text").GetComponent<Text>().text = content;

			gameObject.SetActive (true);
		}

		public void hide () {
			gameObject.SetActive (false);
		}

		public void onConfirmButtonClicked () {
			if (onConfirmCallback != null) {
				onConfirmCallback ();

				onConfirmCallback = null;
			}
		}

		public void onCancelButtonClicked () {
			if (onCancelCallback != null) {
				onCancelCallback ();

				onCancelCallback = null;
			}
		}
	}
}