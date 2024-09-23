using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyEnable : MonoBehaviour
{
    [Header("Spawn Data")]
    [Space]
    [Range(0.1f, 10f)]
    [SerializeField] float delay; // Retraso entre oleadas de aparici�n de enemigos

    [Tooltip("Number of enemy spawn at once")]
    [Space]
    [Range(1f, 10f)]
    [SerializeField] int enemyRound; // N�mero de enemigos que aparecen a la vez
    [Range(1f, 100f)]
    [SerializeField] float minDistance, maxDistance; // Rango de distancia desde el jugador para el spawn

    [Header("TileMap")]
    [Space]
    [SerializeField] private Tilemap spawnTilemap; // Tilemap donde se permite el spawn de enemigos

    private GameObject[] m_enemys; // Array de enemigos en la piscina

    private int count = 0; // Contador de enemigos que han sido activados



    private void OnValidate()
    {
        // Asegurarse de que maxDistance no sea menor que minDistance
        if (maxDistance < minDistance)
        {
            maxDistance = minDistance;
        }
    }

    public void LateStart()
    {
        // Ajustar la cantidad de enemigos y el retraso en funci�n del piso actual
        enemyRound += Mathf.FloorToInt(RoundTimerManager.instance.currentFloor / 10f + 0.1f);
        delay = delay / (RoundTimerManager.instance.currentFloor / 10f + 1 - 0.1f);

        // Obtener los enemigos desde el EnemyPoolManager
        m_enemys = GetComponent<EnemyPoolManager>().typesInstances;

        // Iniciar la corrutina para el spawn aleatorio de enemigos
        StartCoroutine(RandomSpawn());
    }

    IEnumerator RandomSpawn()
    {
        int i = 0;

        // Esperar un retraso antes de comenzar a spawn
        yield return new WaitForSeconds(delay);

        // Mientras queden enemigos en la piscina
        while (count < m_enemys.Length)
        {
            // Verificar si el enemigo en la posici�n actual no est� activo
            if (!m_enemys[count].activeSelf)
            {
                i++;

                // Obtener una posici�n aleatoria para el spawn
                Vector2 spawnPosition = RandomPos();

                // Asegurarse de que la posici�n est� en un tile del tilemap
                while (!IsPositionOnTilemap(spawnPosition))
                {
                    spawnPosition = RandomPos();
                }

                // Colocar el enemigo en la posici�n y activarlo
                m_enemys[count].transform.position = spawnPosition;
                m_enemys[count].SetActive(true);

                // Esperar un retraso despu�s de spawn varios enemigos
                if (i >= enemyRound)
                {
                    yield return new WaitForSeconds(delay);

                    i = 0;
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }

            // Ciclar a trav�s de los enemigos en la piscina
            if (count == m_enemys.Length - 1)
            {
                count = 0;
            }
            else
            {
                count++;
            }
        }
    }

    Vector2 RandomPos()
    {
        // Obtener la posici�n del jugador
        Vector2 playerPosition = PlayerStats.instance.transform.position;
        // Calcular un �ngulo aleatorio y una distancia aleatoria
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(minDistance, maxDistance);
        // Calcular la posici�n de spawn en funci�n del �ngulo y distancia
        Vector2 spawnPosition = playerPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

        return spawnPosition;
    }

    bool IsPositionOnTilemap(Vector2 position)
    {
        // Convertir la posici�n en coordenadas de celda del tilemap
        Vector3Int cellPosition = spawnTilemap.WorldToCell(position);
        TileBase tile = spawnTilemap.GetTile(cellPosition);

        // Retorna true si hay un tile en esa posici�n, false si no lo hay
        return tile != null;
    }
}
