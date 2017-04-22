using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu : MonoBehaviour {

	public Button startgame;
	public Button exitgame;
	// Use this for initialization
	void Start () {
		Button exit = exitgame.GetComponent<Button> ();
		exit.onClick.AddListener (Exit_game);
		Button btn = startgame.GetComponent<Button> ();
		btn.onClick.AddListener (Start_Game);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
	void Start_Game(){
		Application.LoadLevel ("demo");
	}
	void Exit_game(){
		Application.Quit();
	}
}
