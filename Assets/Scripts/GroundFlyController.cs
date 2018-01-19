using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFlyController : MonoBehaviour {

    [SerializeField]
    private float speed;

    //gestion du deplacement du sol(vers la gauche)
    private void Update() {
        transform.position = new Vector3(transform.position.x - speed * 0.05f, transform.position.y, transform.position.z);
    }
}
