using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private int height;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap bridgeTileMap;

    [SerializeField] Tilemap waterTileMap;
    [SerializeField] Tile water;
    [SerializeField] Tile bridge;

    [SerializeField] Tilemap forceOfLight;
    [SerializeField] Tilemap forceOfDarkness;
    
    [SerializeField] Tile[] forceOfLightTiles;

    [SerializeField] Tile[] forceOfDarknessTiles;

    public static Dictionary<Vector3Int, Vector3Int> dictionaryTile = new Dictionary<Vector3Int, Vector3Int>();


    public int TotalAmountTiles { get => weight * height; private set => TotalAmountTiles = value; }
    public int partLight { get => TotalAmountTiles / 2; private set => partLight = value; }
    public int partDarkness { get => TotalAmountTiles / 2; private set => partDarkness = value; }

    int count = 0;
    int bridgePosY = 10;
    int localX;

    private int darknessX;
    private int darknessY;
    public int GetRandomSize(int min, int max)
    {
        return Random.Range(min, max);
    }
    private void Awake()
    {
        RandomShiftLeftWater = GetRandomSize(1, 5);
        RandomShiftRightWater = GetRandomSize(1,5);
    }
    public int RandomShiftLeftWater;

    public int RandomShiftRightWater;

    private void Start()
    {
        for (int x = 0; x < height && count < partLight; x++)
        {
            darknessX = x;

            for (int y = 0; y < weight && count < partLight; y++)
            {
                count++;

                forceOfLight.SetTile(new Vector3Int(x, y, 1), forceOfLightTiles[Random.Range(0, forceOfLightTiles.Length)]);
                
                if (x > weight / 2 - RandomShiftLeftWater && x <= weight / 2)
                {
                   forceOfLight.SetTile(new Vector3Int(x, y, 1), null);
                   
                   waterTileMap.SetTile(new Vector3Int(x, y, 0), water);
                }

                //if (y == bridgePosY && x >= 10 && x <= 15)
                //{
                //    bridgeTileMap.SetTile(new Vector3Int(x, y, 1), bridge);
                //}
                darknessY = y;
            }
        }
        count = 0;

        for (int darkX = darknessX; count < 1250; darkX++)
        {
            for (int darkY = 0; darkY < weight; darkY++)
            {
                if (darkX < (weight / 2 + RandomShiftRightWater))
                {
                    forceOfDarkness.SetTile(new Vector3Int(darkX, darkY, 1), null);

                    waterTileMap.SetTile(new Vector3Int(darkX, darkY, 0), water);
                }
                else
                {
                    forceOfDarkness.SetTile(new Vector3Int(darkX, darkY, 1), forceOfDarknessTiles[Random.Range(0, forceOfDarknessTiles.Length)]);
                }
                count++;
            }
        }
    }
}
