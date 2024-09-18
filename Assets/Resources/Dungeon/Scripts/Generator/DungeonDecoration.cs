using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonDecoration : MonoBehaviour
{
    [Header("Spawn Data")]
    [SerializeField] private GameObject spritePrefab; // Prefab del sprite a spawnnear
    [SerializeField] private Tilemap targetTilemap; // Tilemap donde se spawnnear�n los sprites
    [Range(1, 50f)]
    [SerializeField] private int minSpritesToSpawn = 10; // N�mero m�nimo de sprites a spawnnear
    [Range(1, 50f)]
    [SerializeField] private int maxSpritesToSpawn = 20;  // N�mero m�ximo de sprites a spawnnear

    private List<Vector3> spawnPositions = new List<Vector3>(); // Lista para almacenar posiciones v�lidas de spawn

    private void OnValidate()
    {
        //Asegurar que maxSprites es mayor que minSprites
        if (maxSpritesToSpawn < minSpritesToSpawn)
        {
            maxSpritesToSpawn = minSpritesToSpawn;
        }
    }

    public void Decoration()
    {
        // Obtener todas las posiciones donde hay tiles
        GetTilePositions();

        // Obtener un n�mero aleatorio de sprites a spawnnear entre los valores m�nimos y m�ximos
        int numberOfSpritesToSpawn = Random.Range(minSpritesToSpawn, maxSpritesToSpawn + 1);

        // Spawnnear los sprites
        SpawnSprites(numberOfSpritesToSpawn);
    }

    void GetTilePositions()
    {
        // Iterar a trav�s de todas las posiciones dentro del �rea de celdas del Tilemap
        foreach (var pos in targetTilemap.cellBounds.allPositionsWithin)
        {
            if (targetTilemap.HasTile(pos))
            {
                // Convertir la posici�n de la celda a coordenadas del mundo y a�adirla a la lista
                Vector3 worldPosition = targetTilemap.CellToWorld(pos) + targetTilemap.cellSize / 2;
                spawnPositions.Add(worldPosition);
            }
        }
    }

    void SpawnSprites(int numberOfSpritesToSpawn)
    {
        int spritesSpawned = 0;

        // Spawnnear sprites mientras no se haya alcanzado el n�mero deseado y haya posiciones disponibles
        while (spritesSpawned < numberOfSpritesToSpawn && spawnPositions.Count > 0)
        {
            // Escoger una posici�n aleatoria de la lista
            int randomIndex = Random.Range(0, spawnPositions.Count);
            Vector3 spawnPosition = spawnPositions[randomIndex];

            // Instanciar el prefab del sprite en la posici�n seleccionada
            GameObject instance = Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

            instance.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);

            // Remover la posici�n usada para evitar spawnnear m�ltiples sprites en el mismo lugar
            spawnPositions.RemoveAt(randomIndex);

            spritesSpawned++;
        }
    }
}
