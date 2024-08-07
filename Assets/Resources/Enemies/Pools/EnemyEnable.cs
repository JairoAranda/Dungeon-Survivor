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

    // Start is called before the first frame update
    public void LateStart()
    {
        m_enemys = GetComponent<EnemyPoolManager>().typesInstances;

        StartCoroutine(RandomSpawn());
    }

    IEnumerator RandomSpawn()
    {
        while (count < m_enemys.Length)
        {

            m_enemys[count].transform.position = RandomPos();

            m_enemys[count].SetActive(true);

            count++;

            yield return new WaitForSeconds(delay);

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
