using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Borut Balazek <bobalazek124@gmail.com> 
 */
namespace Game.Manager {
	public class EnvironmentManager : MonoBehaviour {
		public static float temperature = 0f;
		private DateTime dateTime = DateTime.Now;
	}
}