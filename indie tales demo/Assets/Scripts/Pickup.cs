using UnityEngine;

public class Pickup : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if(gameObject.tag == "Crowbar") {
                collision.gameObject.GetComponent<PlayerController>().SetWeapon(Weapons.crowbar);
            }
            else if (gameObject.tag == "Sledgehammer") {
                collision.gameObject.GetComponent<PlayerController>().SetWeapon(Weapons.sledgehammer);
            }
            else { Debug.Log("Weapon not implemented"); 
            }
            Destroy(gameObject);
        }       
    }


    private void Update() {
        transform.Rotate(Vector3.back * Time.deltaTime * 100);
    }
}