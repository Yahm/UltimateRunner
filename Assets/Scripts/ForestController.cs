using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour {

    [SerializeField]
    private float speed;

    //gestion du deplacement de la forêt (vers la gauche)
    private void Update() {
        transform.position = new Vector3(transform.position.x - speed * 0.05f, transform.position.y, transform.position.z);
    }
}
