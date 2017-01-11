using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Should be thrown in directly into the root of the scene (after the actual camera)!
 * 
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.UI.InGame {
	public class HUDMiniMap : MonoBehaviour {
		private GameObject targetGameObject;

		void Start () {
			targetGameObject = GameObject.Find ("Player");
		}

		void LateUpdate () {
			if (!targetGameObject) {
				return;
			}

			transform.position = new Vector3 (
				targetGameObject.transform.position.x,
				transform.position.y,
				targetGameObject.transform.position.z
			);
		}
	}
}