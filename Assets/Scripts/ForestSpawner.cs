using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject forest;

    private float next;
    private float rate;

    //Creation de la forêt
    private void Start() {
        next = 0f;
        rate = 18f;
        Instantiate(forest, transform.position, transform.rotation);
    }

    //et instantiation d'une nouvelle forêt pour creer l'effet de background infini
    private void Update() {
        //délai de spawn
        //rate constitue le temps de parcours de la sprite
        if(Time.time > next) {
            next = Time.time + rate;
            Instantiate(forest, transform.position, transform.rotation);
        }
    }
}
