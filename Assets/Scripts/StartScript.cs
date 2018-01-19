using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Affichage du bouton pour lancer la partie
    /*void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 50, 250, 1000, 50), "ULTIMATE RUNNER");

        if (GUI.Button(new Rect(Screen.width / 2 - 30, 350, 60, 30), "Play"))
        {
            SceneManager.LoadScene("Level0");
        }
    }*/

    //Load Game
    public void LoadGame()
    {
        SceneManager.LoadScene("Level0");
    }
}
