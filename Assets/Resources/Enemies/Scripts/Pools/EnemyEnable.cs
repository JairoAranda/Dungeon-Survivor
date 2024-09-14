using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyEnable : MonoBehaviour
{
    [Header("Spawn Data")]
    [Space]
    [Range(0.1f, 10f)]
    [SerializeField] float delay;

    [Tooltip("Number of enemy spawn at once")]
    [Space]
    [Range(1f, 10f)]
    [SerializeField] int enemyRound;
    [Range(1f, 100f)]
    [SerializeField] float minDistance, maxDistance;

    [Header("TileMap")]
    [Space]
    [SerializeField] private Tilemap spawnTilemap;

    private GameObject[] m_enemys;

    private int count = 0;



    private void OnValidate()
    {
        if (maxDistance < minDistance)
        {
            maxDistance = minDistance;
        }
    }

    public void LateStart()
    {
        enemyRound += Mathf.FloorToInt(RoundTimerManager.instance.currentFloor / 10f + 0.1f);

        delay = delay / (RoundTimerManager.instance.currentFloor / 10f + 1 - 0.1f);

        m_enemys = GetComponent<EnemyPoolManager>().typesInstances;

        StartCoroutine(RandomSpawn());
    }

    IEnumerator RandomSpawn()
    {
        int i = 0;

        yield return new WaitForSeconds(delay);

        while (count < m_enemys.Length)
        {
            if (!m_enemys[count].activeSelf)
            {
                i++;

                Vector2 spawnPosition = RandomPos();

                while (!IsPositionOnTilemap(spawnPosition))
                {
                    spawnPosition = RandomPos();
                }

                m_enemys[count].transform.position = spawnPosition;
                m_enemys[count].SetActive(true);

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
        Vector2 playerPosition = PlayerStats.instance.transform.position;
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(minDistance, maxDistance);
        Vector2 spawnPosition = playerPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

        return spawnPosition;
    }

    // Método para verificar si la posición tiene un tile en el tilemap
    bool IsPositionOnTilemap(Vector2 position)
    {
        // Convertir la posición en coordenadas de celda del tilemap
        Vector3Int cellPosition = spawnTilemap.WorldToCell(position);
        TileBase tile = spawnTilemap.GetTile(cellPosition);

        // Retorna true si hay un tile en esa posición, false si no lo hay
        return tile != null;
    }
}
