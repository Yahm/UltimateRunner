using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] obj;
    public float spawn_time_min = 1f;
    public float spawn_time_max = 2f;

    public float valeur;
    int check = 0;

    int speedCheck = 100;

    // Use this for initialization
    void Start () {
        Spawn();
        valeur = 10;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //spawner des ennemis
    //il prend une liste d'ennemis et choisis un aléatoirement,à une durée aléatoire
    //avec une vitesse qui augmente avec les points
    void Spawn()
    {
        int rand = Random.Range(0, obj.Length);

        
        if((int)PlayerPrefs.GetFloat("points") >= speedCheck)
        {
            valeur += 3;
            speedCheck += 100;
        }

		//GameObject obj_instance
		Instantiate(obj[rand], transform.position, Quaternion.identity);
        //obj_instance.GetComponent<EnemyController>().speed = valeur;

        float randSpawn = Random.Range(spawn_time_min, spawn_time_max);

        Debug.Log("valeur = " + valeur);

		Debug.Log("speed = " + obj[rand].GetComponent<EnemyController>().speed);

        Invoke("Spawn", randSpawn);
        
    }
    //pour aller plus loin
    //differencier la vitesse de deplacement des ennemis
}
