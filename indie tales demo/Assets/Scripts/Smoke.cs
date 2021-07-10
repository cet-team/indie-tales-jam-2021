using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    private int depth = 8;
    void Start() {
        name = "Smoke " + depth;

        if (depth <= 1) {
            return;
        }
        StartCoroutine(CreateAfterASecond());        
    }

    IEnumerator CreateAfterASecond() {
        yield return new WaitForSeconds(1.5f);
        Smoke childA = CreateChild(Vector3.up, Quaternion.identity);
        Smoke childB = CreateChild(Vector3.right, Quaternion.Euler(0f, 0f, -90f));
        Smoke childC = CreateChild(Vector3.left, Quaternion.Euler(0f, 0f, 90f));
        Smoke childD = CreateChild(Vector3.down, Quaternion.identity);

        childA.transform.SetParent(transform, false);
        childB.transform.SetParent(transform, false);
        childC.transform.SetParent(transform, false);
        childD.transform.SetParent(transform, false);
    }

    Smoke CreateChild(Vector3 direction, Quaternion rotation) {
        Smoke child = Instantiate(this);
        child.depth = depth - 1;
        child.transform.localPosition = direction;
        child.transform.localRotation = rotation;
        return child;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision!= null) {
            if (collision.gameObject.CompareTag("Player")){
                GameManager.PlayerDied();
            }
            if (collision.gameObject.CompareTag("Smoke")) {
                Destroy(gameObject);
            }
        }
    }
}

/*
 * private IEnumerator LeakInAllPossibledirections(Vector3Int newOrginAsInt) {
        Vector3Int Up    = newOrginAsInt;
        Vector3Int Down  = newOrginAsInt;
        Vector3Int Left  = newOrginAsInt;
        Vector3Int Right = newOrginAsInt;

        while (true) {
            yield return timeBetweenSpreading;
            Up      += Vector3Int.up;
            Down    += Vector3Int.down;
            Left    += Vector3Int.left;
            Right   += Vector3Int.right;

            if (!smokeMap.HasTile(Up)) {
                TileBase tile = Instantiate(smokeGroundTile);
                smokeMap.SetTile(Up, tile);
                Instantiate(prefabSmoke, Up, Quaternion.identity);
            }
            if (!smokeMap.HasTile(Down)) {
                TileBase tile = Instantiate(smokeGroundTile);
                smokeMap.SetTile(Down, tile);
                Instantiate(prefabSmoke, Down, Quaternion.identity);
            }
            if (!smokeMap.HasTile(Left)) {
                TileBase tile = Instantiate(smokeGroundTile);
                smokeMap.SetTile(Left, tile);
                Instantiate(prefabSmoke, Left, Quaternion.identity);
            }
            if (!smokeMap.HasTile(Right)) {
                TileBase tile = Instantiate(smokeGroundTile);
                smokeMap.SetTile(Right, tile);
                Instantiate(prefabSmoke, Right, Quaternion.identity);
            }
        }
    }*/
