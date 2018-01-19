using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObj : MonoBehaviour {

    public float speed = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
    //Deplacement d'un objet vers la gauche
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
	}
}
