using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneLoader : MonoBehaviour {
	private bool sceneStartedLoading = false;
	private Text loadingText;

	void Start () {
		loadingText = GameObject.Find ("LoadingText").GetComponent<Text>();
	}

	void Update () {
		if (sceneStartedLoading == true) {
			loadingText.color = new Color (
				loadingText.color.r,
				loadingText.color.g,
				loadingText.color.b,
				Mathf.PingPong (Time.time, 1)
			);
		} else {
			StartCoroutine(LoadMainMenuScene());

			sceneStartedLoading = true;
		}
	}
	
	IEnumerator LoadMainMenuScene () {
		yield return new WaitForSeconds(5);
		
		AsyncOperation async = SceneManager.LoadSceneAsync("10_MainMenu");
		
		while (!async.isDone) {
			yield return null;
		}
	}
}
