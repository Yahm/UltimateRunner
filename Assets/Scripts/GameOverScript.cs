using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    //Gestion de la partie de fin
    int score;
    int best_score;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("points") && PlayerPrefs.HasKey("best_points"))
        {
            score = (int)PlayerPrefs.GetFloat("points");
            best_score = (int)PlayerPrefs.GetFloat("best_points");
        }

        PlayerPrefs.DeleteKey("points");
    }


    //Affichage du texte des points et du bouton pour rejouer.
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 40, 50, 80, 30), "GAME OVER");
        GUI.Label(new Rect(Screen.width / 2 - 40, 300, 100, 30), "Your Score : " + score);
        GUI.Label(new Rect(Screen.width / 2 - 40, 320, 100, 30), "Best Score : " + best_score);

        /*if(GUI.Button(new Rect(Screen.width/2 - 40,350,80,30),"Play Again"))
        {
            SceneManager.LoadScene("StartScene");
        }*/
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadGame()
    {
        SceneManager.LoadScene("Level0");
    }
}
