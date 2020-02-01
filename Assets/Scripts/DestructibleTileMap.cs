using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DestructibleTileMap : MonoBehaviour
{
    #region Properties
    private Tilemap tileMap;
    private GridLayout grid;
    private Vector3Int tilePosition;
    #endregion
    #region Unity callbacks
    public void Start()
    {
        tileMap = GetComponent<Tilemap>();
        grid = GetComponentInParent<GridLayout>();
        foreach (var position in tileMap.cellBounds.allPositionsWithin)
        {
            var t = tileMap.GetTile(position);
            if (t is DestructibleTile)
            {
                var dt = Instantiate(t) as DestructibleTile;
                dt.StartUp(position, dt.tileMap, dt.gameObject);
                tileMap.SetTile(position, dt);
            }
        }

    }
    #endregion
    public void Damage(Projectile projectile, Vector3 pContactPoint)
    {
        var tileToDamage = tileMap.GetTile(grid.WorldToCell(pContactPoint));
        if (tileToDamage is DestructibleTile tile) // 
        {
            tile.ApplyDamage(10);
            tileMap.RefreshTile(tile.tilePosition);
        }
    } 
}