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

    private GeneralReciveDrop[] dropTypes;

    private DropAmount amount;

    private int dropNumber = -1;

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
        amount = GetComponent<DropAmount>();

        base.Start();
    }

    void Drop(Vector3 position)
    {
        foreach (var drop in drops)
        {
            int chance = amount.GetDropNumber(drop.minDrop, drop.maxDrop);

            for (int i = 0; i < chance; i++)
            {
                dropNumber++;

                if (dropNumber > poolSize - 1)
                {
                    dropNumber = 0;
                }

                typesInstances[dropNumber].transform.position = GetRandomPositionAroundPoint(position, maxRange);

                dropTypes = typesInstances[dropNumber].GetComponents<GeneralReciveDrop>();

                foreach (GeneralReciveDrop dropType in dropTypes)
                {
                    if (dropType.type == drop.types)
                    {
                        dropType.enabled = true;
                    }

                    else
                    {
                        dropType.enabled = false;
                    }
                }

                typesInstances[dropNumber].SetActive(true);
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
