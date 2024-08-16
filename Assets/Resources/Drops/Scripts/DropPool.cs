using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DropAmount))]
public class DropPool : GeneralPool
{
    [Header("Drops Types")]
    [SerializeField] SODrop[] drops;

    [Header("Spread Range")]
    [Range(0.0f, 2.0f)]
    [SerializeField] private float maxRange = 0.5f;

    private DropAmount amount;

    int[] dropNumber;

    int currentDrop = -1;

    int minNum = 0;

    int maxNum = 0;

    private void OnEnable()
    {
        EnemyStats.EventTriggerDeathEnemy += Drop;
    }

    private void OnDisable()
    {
        EnemyStats.EventTriggerDeathEnemy -= Drop;
    }

    protected override void Start()
    {
        poolSize = 0;

        amount = GetComponent<DropAmount>();

        dropNumber = new int[drops.Length];


        foreach (var drop in drops)
        {
            poolSize += drop.dropAmount;
        }

        typesInstances = new GameObject[poolSize];

        int type = -1;
        int j = 0;

        foreach (var drop in drops)
        {
            type++;

            for (int i = 0; i < drop.dropAmount; i++)
            {
                typesInstances[j] = Instantiate(types[type], new Vector2(0, 0f), Quaternion.identity);
                typesInstances[j].transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);
                j++;
            }

        }
    }

    void Drop(Vector3 position)
    {
        foreach (var drop in drops)
        {
            int chance = amount.GetDropNumber(drop.minDrop, drop.maxDrop, drop.probabilityMaxDrop);

            if (currentDrop < dropNumber.Length - 1)
            {
                currentDrop++;

                minNum = maxNum - 1;
            }

            else
            {
                currentDrop = 0;

                minNum = 0;

                maxNum = 0;
            }

            maxNum += drop.dropAmount;

            if (dropNumber[currentDrop] == 0)
            {
                dropNumber[currentDrop] = minNum;
            }

            for (int i = 0; i < chance; i++)
            {
                dropNumber[currentDrop]++;

                if (dropNumber[currentDrop] >= maxNum)
                {
                    dropNumber[currentDrop] = minNum;
                }

                typesInstances[dropNumber[currentDrop]].transform.position = GetRandomPositionAroundPoint(position, maxRange);

                typesInstances[dropNumber[currentDrop]].SetActive(true);
            }
        }

    }

    Vector2 GetRandomPositionAroundPoint(Vector2 center, float maxDistance)
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);

        float distance = Random.Range(0f, maxDistance);

        float x = center.x + distance * Mathf.Cos(angle);
        float y = center.y + distance * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}
