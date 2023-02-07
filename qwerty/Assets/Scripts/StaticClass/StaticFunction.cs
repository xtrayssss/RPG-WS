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

    }
}
