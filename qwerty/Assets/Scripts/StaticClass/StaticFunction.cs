using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.StaticFunction
{
    public static class StaticFunction
    {
        public static Vector3 SetPositionOnTheCenterTile(Tilemap tilemap, Vector3 currentPosition)
        {
            Vector3Int startPosition = tilemap.WorldToCell(currentPosition);

            return tilemap.CellToLocal(startPosition);
        }

        public static Vector3Int GetRandomTileLeft(int width, int height) =>
            new Vector3Int(Random.Range(0, width), Random.Range(0, height));

        public static Vector3Int GetRandomTileRight(int width, int height) =>
            new Vector3Int(Random.Range(width / 2 + GenerateLevel.Instance.RandomShiftRightWater, width), Random.Range(0, height));
    }
}
