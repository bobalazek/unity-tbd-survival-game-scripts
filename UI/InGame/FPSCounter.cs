using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.InGame {
	public class FPSCounter : MonoBehaviour {
		private Text text;
		private string textString = "{0} FPS";
		private float measurePeriod = 0.5f;
		private float measurePeriodNext = 0.5f;
		private float fps = 0f;
		private int accumulator = 0;
		
		void Start () {
			text = transform.Find ("Text").GetComponent<Text> ();
		}

		void Update () {
			accumulator++;
			if (Time.realtimeSinceStartup > measurePeriodNext) {
				fps = (int) (accumulator / measurePeriod);
				accumulator = 0;
				measurePeriodNext += measurePeriod;
				text.text = string.Format(textString, fps);
			}
		}
	}
}