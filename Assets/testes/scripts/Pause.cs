using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject menu;

	void start(){
		menu.GetComponent<MeshRenderer>().enabled = false;
		menu.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			menu.SetActive (true);
			Time.timeScale = 0f;
		}
	
	}
	public void voltar(){
		Time.timeScale = 1f;
		menu.SetActive (false);
	}
}
