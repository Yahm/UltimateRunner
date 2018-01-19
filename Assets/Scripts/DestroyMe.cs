using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    [SerializeField]
    private float delay;


    //Destruction de l'objet apres un certains temps
    private void Start() {
        Destroy(gameObject, delay);
    }
}
