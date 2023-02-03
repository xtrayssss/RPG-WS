using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLand : MonoBehaviour
{
    [SerializeField] private float weight;
    [SerializeField] private float height;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap bridgeTileMap;

    [SerializeField] Tile[] tile;
    [SerializeField] Tile water;
    [SerializeField] Tile bridge;

    [SerializeField] Dictionary<Tile, Transform> dictionaryTile;
    int count = 0;
    int bridgePosY = 10;
    int localX;
    private void Start()
    {
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < weight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y), tile[Random.Range(0, tile.Length)]);
                
                if (x > 9 && x < 16)
                {
                    tilemap.SetTile(new Vector3Int(x, y),water);
                }

                if (y == bridgePosY && x >= 10 && x <= 15)
                {

                    bridgeTileMap.SetTile(new Vector3Int(x, y, 1), bridge);

                    //count++;
                    //Debug.Log(count);

                    //if (count == 6)
                    //{
                    //    bridgePosY = Random.Range(0, 20);
                    //    count = 0;
                    //}
                }
                
            }
        }
    }
}
