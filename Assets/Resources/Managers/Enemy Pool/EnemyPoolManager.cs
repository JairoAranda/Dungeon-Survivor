using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyEnable))]
public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager instance;

    [Header("Enemy Pool Data")]
    [Range(1, 50)]
    public int enemyPoolSize = 20;
    [SerializeField] GameObject[] enemysTypes;

    [HideInInspector]
    public GameObject[] enemys;

    private EnemyEnable enemyEnable;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        enemyEnable = GetComponent<EnemyEnable>();

        enemys = new GameObject[enemyPoolSize];

        for (int i = 0; i < enemyPoolSize; i++)
        {
            enemys[i] = Instantiate(enemysTypes[Random.Range(0, enemysTypes.Length)], new Vector2(0, 0f), Quaternion.identity);
            enemys[i].transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);
        }

        enemyEnable.LateStart();
    }

}
