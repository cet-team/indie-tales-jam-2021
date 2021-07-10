using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GasController : MonoBehaviour {

    [SerializeField] GameObject prefabSmoke;
    [SerializeField] Tilemap smokeMap;    
    [SerializeField] TileBase smokeGroundTile;
    Vector3 origin;
    Vector3Int originAsInt;

    private void Start() {
        Invoke("StartLeaking", 1f);
        origin = smokeMap.LocalToCellInterpolated(transform.position);
        originAsInt = smokeMap.LocalToCell(transform.position);
    }

    private void StartLeaking() {
        GameObject smokeObject = Instantiate(prefabSmoke, origin , Quaternion.identity);
        smokeMap.SetTile(originAsInt, smokeGroundTile);
        //smokeMap.In
        /*
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(soundThrow);*/
    }    

    private void LeakInAllPossibledirections() {
        Vector3 Up = origin + Vector3.up;
        Vector3 Down = origin + Vector3.down;
        Vector3 Left = origin + Vector3.left;
        Vector3 Right = origin + Vector3.right;


    }
}
