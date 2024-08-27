using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonDecoration : MonoBehaviour
{
    [Header("Spawn Data")]
    [SerializeField] private GameObject spritePrefab;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private int minSpritesToSpawn = 10;
    [SerializeField] private int maxSpritesToSpawn = 20;

    private List<Vector3> spawnPositions = new List<Vector3>();

    public void Decoration()
    {
        // Obtener todas las posiciones donde hay tiles
        GetTilePositions();

        // Obtener un número aleatorio de sprites a spawnnear entre los valores mínimos y máximos
        int numberOfSpritesToSpawn = Random.Range(minSpritesToSpawn, maxSpritesToSpawn + 1);

        // Spawnnear los sprites
        SpawnSprites(numberOfSpritesToSpawn);
    }

    void GetTilePositions()
    {
        foreach (var pos in targetTilemap.cellBounds.allPositionsWithin)
        {
            if (targetTilemap.HasTile(pos))
            {
                // Convertir la posición de la celda a coordenadas del mundo y añadirla a la lista
                Vector3 worldPosition = targetTilemap.CellToWorld(pos) + targetTilemap.cellSize / 2;
                spawnPositions.Add(worldPosition);
            }
        }
    }

    void SpawnSprites(int numberOfSpritesToSpawn)
    {
        int spritesSpawned = 0;

        while (spritesSpawned < numberOfSpritesToSpawn && spawnPositions.Count > 0)
        {
            // Escoger una posición aleatoria de la lista
            int randomIndex = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPosition = spawnPositions[randomIndex];

            GameObject instance = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

            instance.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);

            // Remover la posición usada para evitar spawnnear múltiples sprites en el mismo lugar
            spawnPositions.RemoveAt(randomIndex);

            spritesSpawned++;
        }
    }
}
