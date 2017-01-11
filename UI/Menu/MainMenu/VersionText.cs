using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu.MainMenu {
	public class VersionText : MonoBehaviour {
		void Start () {
			transform.GetComponent<Text> ().text = Game.Core.Config.VERSION;
		}
	}
}