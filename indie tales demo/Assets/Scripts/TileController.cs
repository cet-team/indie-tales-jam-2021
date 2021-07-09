using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour {
    
  
    [SerializeField]Tilemap groundTileMap, wallTileMap;
    TilemapCollider2D tilemapcollider;

    private void Start() {
        groundTileMap.CompressBounds();
        wallTileMap.CompressBounds();
    }

    private void Awake() {
        BoundsInt bounds = wallTileMap.cellBounds;
        TileBase[] allWallTiles = wallTileMap.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allWallTiles[x + y * bounds.size.x];


                //tilemapcollider.
            }
        }
    }

    public void AttackAtPosition(Vector3 position) {
        Vector3Int currentCell = wallTileMap.WorldToCell(position);
        if (currentCell != null) {
            if (IsTiledestroyable(wallTileMap.GetTile(currentCell))){
                wallTileMap.SetTile(currentCell, null);
            }
        }
    }

    private bool IsTiledestroyable(TileBase tile) {
        if (tile.name.Contains("Walls1")) {
            return true;
        }
        else {
            return false; 
        }
    }
}