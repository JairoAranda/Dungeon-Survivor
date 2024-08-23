using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    // RuleTile para muros
    [SerializeField] RuleTile wallTile;

    // Arrays de tiles para suelos
    [SerializeField] Tile[] floorTiles;

    // Lista de pesos para los tiles de suelo
    [SerializeField] int[] floorTileWeights;

    [SerializeField] int roomMinSize = 5;
    [SerializeField] int roomMaxSize = 10;

    private void OnValidate()
    {
        if (floorTileWeights == null || floorTileWeights.Length != floorTiles.Length)
        {
            System.Array.Resize(ref floorTileWeights, floorTiles.Length);
            for (int i = 0; i < floorTileWeights.Length; i++)
            {
                if (floorTileWeights[i] == 0)
                {
                    floorTileWeights[i] = 1;
                }
            }
        }
    }

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        // Genera el tamaño de la sala aleatoriamente
        int roomWidth = Random.Range(roomMinSize, roomMaxSize);
        int roomHeight = Random.Range(roomMinSize, roomMaxSize);

        // Calcula la esquina inferior izquierda de la sala para que su centro esté en (0, 0)
        int roomX = -roomWidth / 2;
        int roomY = -roomHeight / 2;

        Rect room = new Rect(roomX, roomY, roomWidth, roomHeight);
        CreateRoom(room);
    }

    void CreateRoom(Rect room)
    {
        // Primero genera los tiles de suelo dentro de la forma ovalada
        for (int x = Mathf.FloorToInt(room.x); x < Mathf.CeilToInt(room.xMax); x++)
        {
            for (int y = Mathf.FloorToInt(room.y); y < Mathf.CeilToInt(room.yMax); y++)
            {
                if (IsInsideIrregularShape(x, y, room))
                {
                    Tile floorTile = GetRandomWeightedTile(floorTiles, floorTileWeights);
                    tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
            }
        }

        // Luego genera los tiles de muro alrededor de la forma ovalada
        for (int x = Mathf.FloorToInt(room.x) - 1; x <= Mathf.CeilToInt(room.xMax); x++)
        {
            for (int y = Mathf.FloorToInt(room.y) - 1; y <= Mathf.CeilToInt(room.yMax); y++)
            {
                if (IsBorderOfIrregularShape(x, y, room))
                {
                    // Establece el RuleTile para los muros
                    tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                }
            }
        }
    }

    bool IsInsideIrregularShape(int x, int y, Rect room)
    {
        float centerX = room.x + room.width / 2;
        float centerY = room.y + room.height / 2;
        float radiusX = room.width / 2;
        float radiusY = room.height / 2;

        float dx = (x - centerX) / radiusX;
        float dy = (y - centerY) / radiusY;

        return (dx * dx + dy * dy) <= 1.0f;
    }

    bool IsBorderOfIrregularShape(int x, int y, Rect room)
    {
        float centerX = room.x + room.width / 2;
        float centerY = room.y + room.height / 2;
        float radiusX = room.width / 2;
        float radiusY = room.height / 2;

        float dx = (x - centerX) / radiusX;
        float dy = (y - centerY) / radiusY;

        // Calcula si el tile está en el borde de la forma ovalada
        float distance = (dx * dx + dy * dy);
        bool isInShape = distance <= 1.0f;
        bool isOnBorder = Mathf.Abs(distance - 1.0f) < 0.1f; // Ajusta el margen para el borde

        return isInShape && isOnBorder;
    }

    Tile GetRandomWeightedTile(Tile[] tiles, int[] weights)
    {
        // Calcula la suma total de los pesos
        int totalWeight = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];
        }

        // Selecciona un número aleatorio en ese rango de pesos
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        // Encuentra el tile que corresponde al peso seleccionado
        for (int i = 0; i < weights.Length; i++)
        {
            currentWeight += weights[i];
            if (randomWeight < currentWeight)
            {
                return tiles[i];
            }
        }

        return tiles[0]; // Fallback (por si acaso)
    }
}
