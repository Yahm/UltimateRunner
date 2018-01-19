using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;
    

    //Gestion du déplacement des ennemis (vers la gauche)
    private void Update() {
        //Debug.Log(speed);
        transform.position = new Vector3(transform.position.x - speed * 0.05f, transform.position.y, transform.position.z);
    }
}
