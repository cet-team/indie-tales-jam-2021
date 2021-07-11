using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Smoke : MonoBehaviour {

    GameObject Grid;
    Tilemap smokeTileMap, wallTileMap;
    public Tile smokeGroundTile;

    private int depth = 6;
    private Vector3 origin;
    private Vector3Int originAsInt;

    private void Start() {
        name = "Smoke " + depth;

        if (depth <= 1) {
            return;
        }    
        
        Grid = GameObject.FindGameObjectWithTag("Grid");
        smokeTileMap = Grid.transform.Find("SmokeTilemap").GetComponent<Tilemap>();
        wallTileMap = Grid.transform.Find("WallTileMap").GetComponent<Tilemap>();
        

        origin = smokeTileMap.LocalToCellInterpolated(transform.position);
        originAsInt = smokeTileMap.LocalToCell(transform.position);

        TileBase tile = Instantiate(smokeGroundTile);
        smokeTileMap.SetTile(originAsInt, tile);

        StartCoroutine(CreateAfterASecond());        
    }

    IEnumerator CreateAfterASecond() {
        yield return new WaitForSeconds(1.5f);

        if (!smokeTileMap.HasTile(originAsInt + Vector3Int.up) && !wallTileMap.HasTile(originAsInt + Vector3Int.up)) {
            Smoke childA = CreateChild(Vector3.up);
            childA.transform.SetParent(transform, false);
        }
        if (!smokeTileMap.HasTile(originAsInt + Vector3Int.right) && !wallTileMap.HasTile(originAsInt + Vector3Int.right)) {
            Smoke childB = CreateChild(Vector3.right);
            childB.transform.SetParent(transform, false);
        }        
        if (!smokeTileMap.HasTile(originAsInt + Vector3Int.left) && !wallTileMap.HasTile(originAsInt + Vector3Int.left)) {
            Smoke childC = CreateChild(Vector3.left);
            childC.transform.SetParent(transform, false);
        }
        if (!smokeTileMap.HasTile(originAsInt + Vector3Int.down) && !wallTileMap.HasTile(originAsInt + Vector3Int.down)) {
            Smoke childD = CreateChild(Vector3.down);
            childD.transform.SetParent(transform, false);
        }
    }

    Smoke CreateChild(Vector3 direction) {
        Smoke child = Instantiate(this);
        child.depth = depth - 1;
        child.transform.localPosition = direction;
        return child;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision!= null) {
            if (collision.gameObject.CompareTag("Player")){
                GameManager.PlayerDied();
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
