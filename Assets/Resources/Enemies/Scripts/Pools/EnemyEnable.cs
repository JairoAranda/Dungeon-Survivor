using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPoolManager))]
public class EnemyEnable : MonoBehaviour
{
    [Header("Spawn Data")]
    [Range(0.1f, 10f)]
    [SerializeField] float delay;
    [Range(1f, 10f)]
    [Tooltip("Number of enemy spawn at once")]
    [SerializeField] int enemyRound;
    [Range(1f, 100f)]
    [SerializeField] float minDistance, maxDistance;

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
        m_enemys = GetComponent<EnemyPoolManager>().typesInstances;

        StartCoroutine(RandomSpawn());
    }

    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(delay);

        while (count < m_enemys.Length)
        {
            if (!m_enemys[count].activeSelf)
            {
                m_enemys[count].transform.position = RandomPos();

                m_enemys[count].SetActive(true);

                yield return new WaitForSeconds(delay);
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
}
