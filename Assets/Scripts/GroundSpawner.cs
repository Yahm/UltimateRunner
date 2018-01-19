using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject groundFly;

    private float next;
    private float rate;

    private void Start() {
        next = 0f;
        rate = 1.9f;
    }

    //Instantiation d'une nouvelle sprite sol pour créer l'effet de sol infini
    private void Update() {
        //délai de spawn
        //rate constitue le temps de parcours de la sprite
        if (Time.time > next) {
            next = next + rate;
            Instantiate(groundFly, transform.position, transform.rotation);
        }
    }
}
