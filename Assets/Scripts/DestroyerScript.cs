using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {


    //Script de destruction des objets lors de la collision

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //verification du joueur
        //si ce dernier tombe == fin de partie
        if(transform.tag != "Player")
        {
            if (col.tag == "Player")
            {
                SceneManager.LoadScene("EndScene");
            }

            //Destruction de l'objet et de son parent.
            if (col.gameObject.transform.parent)
            {
                Destroy(col.gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
        
    }

    //Gestion de la collision entre le joueur et un obstacle
    //Chargement de la scène fin de partie
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (transform.tag == "Player" && col.transform.tag == "Obstacle")
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
