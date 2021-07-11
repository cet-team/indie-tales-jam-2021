using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour {
    
  
    [SerializeField]Tilemap groundTileMap, wallTileMap;
    TilemapCollider2D tilemapcollider;
    TileBase[] allWallTiles;
    int[] hitpoints;
    BoundsInt bounds;

    private void Awake() {
        groundTileMap.CompressBounds();
        wallTileMap.CompressBounds();
    }

    private void Start() {
        bounds = wallTileMap.cellBounds;
        allWallTiles = wallTileMap.GetTilesBlock(bounds);
        hitpoints = new int[allWallTiles.Length];
        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                if (allWallTiles[x + y * bounds.size.x] != null) {
                    if (allWallTiles[x + y * bounds.size.x].name.Contains("Walls1")) {
                        hitpoints[x + y * bounds.size.x] = 100;
                    }
                }
            }
        }
    }


    public void AttackAtPosition(Vector3 position, int damage) {
        Vector3Int currentCell = wallTileMap.WorldToCell(position);
        
        if (currentCell != null) {            
            if (wallTileMap.GetTile(currentCell).name.Contains("Walls1")){
                
                TileBase tile = wallTileMap.GetTile(currentCell);
                for (int i = 0; i < allWallTiles.Length; i++) {
                    if (allWallTiles[i] == tile) {
                        hitpoints[i] -= damage;
                        if (hitpoints[i] <= 0) {
                            wallTileMap.SetTile(currentCell, null);
                            GameManager.Instance.PlayWallDawn1Sound();
                        }
                        else {
                            GameManager.Instance.PlayWallAlmostsDownSound();
                        }
                        return;
                    }
                }
            }
        }
    }
}
