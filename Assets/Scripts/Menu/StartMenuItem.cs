using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenuItem : MonoBehaviour, IPointerClickHandler {
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
		SceneManager.LoadScene("Game Scene");
		SceneManager.UnloadSceneAsync("Main Menu");
	}
}
