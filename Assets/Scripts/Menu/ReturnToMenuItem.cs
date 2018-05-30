using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnToMenuItem : MonoBehaviour, IPointerClickHandler {

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>().ResetGame();
		SceneManager.LoadScene("Main Menu");
		SceneManager.UnloadSceneAsync("Game Scene");
	}
}
