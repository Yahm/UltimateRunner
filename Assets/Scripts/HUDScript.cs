using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour {

    //Gestion du score du joueur et affichage à l'écran
    
    float playerScore = 0;
	// Use this for initialization
	void Awake () {

        if (!PlayerPrefs.HasKey("points"))
        {
            PlayerPrefs.SetFloat("points", 0);
        }
        if (!PlayerPrefs.HasKey("best_points"))
        {
            PlayerPrefs.SetFloat("best_points", 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        playerScore += Time.deltaTime;
        if (PlayerPrefs.HasKey("points")) {
            //stocke le resultat dans player prefs
            PlayerPrefs.SetFloat("points", playerScore * 10);
        }
    }
    public void IncreaseScore(int amount)
    {
        playerScore += amount;
    }

    void OnDisable()
    {
        if (PlayerPrefs.HasKey("points"))
        {
            PlayerPrefs.SetFloat("points", playerScore * 10);
            if (PlayerPrefs.GetFloat("points") > PlayerPrefs.GetFloat("best_points"))
            {
                //stock le meilleur résultat
                PlayerPrefs.SetFloat("best_points", PlayerPrefs.GetFloat("points"));
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "Score : " + (int)(playerScore * 10));
    }
}
