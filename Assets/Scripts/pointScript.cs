using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointScript : MonoBehaviour {

    HUDScript hud;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //pour l'implémentation de power ups pour augmenter les points
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            hud = GameObject.Find("Main Camera").GetComponent<HUDScript>();
            hud.IncreaseScore(10);
            Destroy(this.gameObject);   
        }
    }
}
