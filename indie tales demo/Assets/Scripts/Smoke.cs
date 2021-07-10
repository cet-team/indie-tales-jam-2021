using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void Awake() {
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision!= null) {
            if (collision.gameObject.CompareTag("Player")){
                GameManager.PlayerDied();
            }
        }
    }
}
