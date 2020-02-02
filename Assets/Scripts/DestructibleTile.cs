using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTile : Tile
{
    #region Properties
    [Space(20)]
    [Header("Destructible Tile")]
    public float life;
    private float StartLife;
    public Sprite brokenSprite;
    public ITilemap tileMap;
    public Vector3Int tilePosition;
    #endregion
    #region Tile Overriding
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        StartLife = life;
        //Store some data
        tileMap = tilemap;
        tilePosition = position;
        return base.StartUp(position, tilemap, go);
    }
    
    
    #endregion
    #region Implementation
    public void ApplyDamage(float pDamage)
    {
        life -= pDamage;
        if (life < StartLife / 2 && sprite != brokenSprite)
            sprite = brokenSprite;
        if (life < 0)
            sprite = null;
    }
    #endregion
    #region Asset DataBase
    [MenuItem("Assets/MoorLighter/DestructibleTile")]
    public static void CreateDestructibleTile()
    {
        var path = EditorUtility.SaveFilePanelInProject("Save Destructible Tile", "DestructibleTile_", "Asset", "Save Destructible Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(CreateInstance<DestructibleTile>(), path);
    }
    #endregion
}