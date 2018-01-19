using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Controlle du joueur
    //Ce dernier ne fait que "courir" avec une animation de course toujours à true
    //son seul déplacement est le saut
    [SerializeField]
    private float jumpHeight;

    Rigidbody2D playerBody;
    Animator playerAnim;

    private bool grounded;
    private bool jump;

    private void Start() {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        //si la variable saut est vrai,effectuer un saut s'il est sur le sol
        if (jump) {
            jump = false;
            if (grounded) {
                grounded = false;
                playerAnim.SetBool("grounded", false);
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpHeight);
            }
        }
        //if (Input.GetKeyDown("space")) {
        //    jump = false;
        //    if (grounded) {
        //        grounded = false;
        //        playerAnim.SetBool("grounded", false);
        //        playerBody.velocity = new Vector2(playerBody.velocity.x, jumpHeight);
        //    }
        //}
    }
    
    //Collision avec le sol
    //Collision avec les ennemis
    //Chargement de la fin de partie
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            playerAnim.SetBool("grounded", true);
            grounded = true;
            jump = false;
        }else if(collision.gameObject.tag == "Enemy") {
            //Destroy(gameObject);
            SceneManager.LoadScene("EndScene");
        }
    }

    public void setJump() {
        if (grounded) {
            jump = true;
        }
    }
}
