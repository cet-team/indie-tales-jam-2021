using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GasController : MonoBehaviour {

    [SerializeField] GameObject prefabSmoke;
    [SerializeField] Tilemap smokeMap;    
    [SerializeField] TileBase smokeGroundTile;
    [SerializeField] WaitForSeconds timeBetweenSpreading = new WaitForSeconds(1f);
    Vector3 origin;
    Vector3Int originAsInt;

    private void Start() {
        Invoke("StartLeaking", 5f);
        origin = smokeMap.LocalToCellInterpolated(transform.position);
        originAsInt = smokeMap.LocalToCell(transform.position);
    }

    private void StartLeaking() {
        Instantiate(prefabSmoke, origin , Quaternion.identity);
        smokeMap.SetTile(originAsInt, smokeGroundTile);
    }       
}
