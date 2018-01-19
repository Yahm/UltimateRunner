using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemys;

    private float next;
    private float rate;

    private int rand;

    private void Start() {
        next = 0f;
        rate = 2.0f;
    }


    //Spawning des ennemis avec un temps d'apparition aléatoire
    private void Update() {
        rate = Random.Range(2, 3);
        if (Time.time > next) {
            next = Time.time + rate;
            rand = Random.Range(0, enemys.Count);
            Instantiate(enemys[rand], transform.position, transform.rotation);
        }
    }
}
