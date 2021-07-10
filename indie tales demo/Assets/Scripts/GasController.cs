using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GasController : MonoBehaviour {

    [SerializeField] GameObject prefabSmoke;
    [SerializeField] Tilemap smokeMap;

    private void Start() {
        Invoke("StartLeaking", 1f);
    }

    private void StartLeaking() {
        GameObject smokeObject = Instantiate(prefabSmoke, smokeMap.LocalToCellInterpolated(transform.position), Quaternion.identity);
        /*
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(soundThrow);*/
    }

    private void LeakInAllPossibledirections() {

    }
}
