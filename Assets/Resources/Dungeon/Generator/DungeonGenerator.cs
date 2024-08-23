using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    [SerializeField] Tile[] floorTiles;
    [SerializeField] Tile[] wallTiles;

    [SerializeField] int dungeonWidth = 50;
    [SerializeField] int dungeonHeight = 50;
    [SerializeField] int roomMinSize = 5;
    [SerializeField] int roomMaxSize = 10;

    void Start()
    {
        GenerateRoom();
        AddWalls();
    }

    void GenerateRoom()
    {
        int roomWidth = Random.Range(roomMinSize, roomMaxSize);
        int roomHeight = Random.Range(roomMinSize, roomMaxSize);
        int roomX = (dungeonWidth - roomWidth) / 2; 
        int roomY = (dungeonHeight - roomHeight) / 2;

        Rect room = new Rect(roomX, roomY, roomWidth, roomHeight);
        CreateRoom(room);
    }

    void CreateRoom(Rect room)
    {
        Tile floorTile = floorTiles[Random.Range(0, floorTiles.Length)];

        for (int x = Mathf.FloorToInt(room.x); x < Mathf.CeilToInt(room.xMax); x++)
        {
            for (int y = Mathf.FloorToInt(room.y); y < Mathf.CeilToInt(room.yMax); y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }
    }

    void AddWalls()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(bounds.xMin + x, bounds.yMin + y, 0);

                if (tilemap.HasTile(tilePos))
                {
                    continue;
                }

                if (HasAdjacentFloorTile(tilePos))
                {
                    Tile wallTile = wallTiles[Random.Range(0, wallTiles.Length)];
                    tilemap.SetTile(tilePos, wallTile);
                }
            }
        }
    }

    bool HasAdjacentFloorTile(Vector3Int position)
    {
        Vector3Int[] directions = {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0)
        };

        foreach (var direction in directions)
        {
            if (tilemap.HasTile(position + direction))
            {
                return true;
            }
        }

        return false;
    }
}
