using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevel : MonoBehaviour
{
    #region Variables

    [SerializeField] public Tile[] forceOfLightTiles;

    [SerializeField] public Tile[] forceOfDarknessTiles;

    public static GenerateLevel Instance;

    [SerializeField] public int width;
    
    [SerializeField] public int height;

    [SerializeField] private Tilemap bridgeTileMap;

    [SerializeField] private Tilemap waterTileMap;
    
    [SerializeField] public Tile water;
    
    [SerializeField] public Tile bridge;

    [SerializeField] private Tilemap forceOfLight;
    
    [SerializeField] private Tilemap forceOfDarkness;
    
    private int TotalAmountTiles { get => width * height;}
    private int partLight { get => TotalAmountTiles / 2;}

    private int count = 0;

    private int darknessX;
    
    public int RandomShiftLeftWater;

    public int RandomShiftRightWater;

    private int bridgeStartBuildX;

    private int bridgeLastBuildX;

    private int randomBridgeY;
    [field: SerializeField] public int AmountBridge { get; private set; }
    #endregion

    #region Awake
    private void Awake()
    {
        Instance = this;
        RandomShiftLeftWater = GetRandomSize(1, 5);
        RandomShiftRightWater = GetRandomSize(1,5);
        randomBridgeY = GetRandomSize(1, height);
    }
    #endregion

    private void Start()
    {
        PartOfLight();
        PartOfDarkness();
        BridgeSpawn();
    }

    #region GetRandomSize
    public int GetRandomSize(int min, int max) => Random.Range(min, max);
    #endregion
   
    #region BridgeSpawn
    public void BridgeSpawn()
    {
        int countBridge = 0;

        while (AmountBridge > countBridge)
        {
            for (int x = bridgeStartBuildX; x <= bridgeLastBuildX; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (randomBridgeY == y)
                    {
                        bridgeTileMap.SetTile(new Vector3Int(x, y, 1), bridge);
                    }
                }
            }

            countBridge++;

            randomBridgeY = GetRandomSize(1, height);
        }
    }
    #endregion

    #region PartOfDarkness
    private void PartOfDarkness()
    {
        count = 0;

        for (int darkX = width / 2; darkX < width; darkX++)
        {
            for (int darkY = 0; darkY < height; darkY++)
            {
                if (darkX < (width / 2 + RandomShiftRightWater))
                {
                    forceOfDarkness.SetTile(new Vector3Int(darkX, darkY, 1), null);

                    waterTileMap.SetTile(new Vector3Int(darkX, darkY, 0), water);
                    
                    if (bridgeStartBuildX == 0)
                    {
                        bridgeStartBuildX = darkX;
                    }
                    if (darkX + 1 >= (width / 2 + RandomShiftRightWater))
                    {
                        bridgeLastBuildX = darkX;
                    }
                }
                else
                {
                    forceOfDarkness.SetTile(new Vector3Int(darkX, darkY, 1), forceOfDarknessTiles[Random.Range(0, forceOfDarknessTiles.Length)]);
                }
                count++;
            }
        }
    }
    #endregion

    #region PartOfLight
    private void PartOfLight()
    {
        for (int x = 0; x < width / 2; x++)
        {
            darknessX = x;

            for (int y = 0; y < height; y++)
            {
                count++;

                forceOfLight.SetTile(new Vector3Int(x, y, 1), forceOfLightTiles[Random.Range(0, forceOfLightTiles.Length)]);
                
                if (x > width / 2 - RandomShiftLeftWater && x <= width / 2)
                {
                    forceOfLight.SetTile(new Vector3Int(x, y, 1), null);

                    waterTileMap.SetTile(new Vector3Int(x, y, 0), water);

                    if (bridgeStartBuildX == 0)
                    {
                        bridgeStartBuildX = x;
                    }
                }
            }
        }
    }
    #endregion
}
