using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DropAmount))]
public class DropPool : GeneralPool
{

    [Header("Drops Types")]
    [Space]
    [SerializeField] SODrop[] drops; // Array de tipos de drops configurados en ScriptableObjects

    [Header("Spread Range")]
    [Space]
    [Range(0.0f, 2.0f)]
    [SerializeField] private float maxRange = 0.5f; // Rango máximo para dispersar los objetos de drop

    private DropAmount amount; // Componente para obtener la cantidad de drop basada en la suerte

    int[] dropNumber; // Array para llevar el control de los números de drop

    int currentDrop = -1; // Índice actual del drop

    int minNum = 0; // Número mínimo de drops
    int maxNum = 0; // Número máximo de drops

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
        poolSize = 0; // Inicializar el tamaño del pool

        amount = GetComponent<DropAmount>(); // Obtener el componente DropAmount

        dropNumber = new int[drops.Length]; // Inicializar el array de números de drop

        // Calcular el tamaño total del pool en base a la cantidad de cada tipo de drop
        foreach (var drop in drops)
        {
            poolSize += drop.dropAmount;
        }

        // Inicializar el array de instancias de los tipos de drops
        typesInstances = new GameObject[poolSize];

        int type = -1;
        int j = 0;

        // Instanciar los objetos de drop y almacenarlos en el array typesInstances
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

    void Drop(GameObject gameobject)
    {
        // Lógica para determinar y activar los drops cuando un enemigo muere
        foreach (var drop in drops)
        {
            int chance = amount.GetDropNumber(drop.minDrop, drop.maxDrop, drop.minProbabilityMaxDrop ,drop.maxProbabilityMaxDrop);

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

                // Establecer la posición aleatoria para el drop y activarlo
                typesInstances[dropNumber[currentDrop]].transform.position = GetRandomPositionAroundPoint(gameobject.transform.position, maxRange);
                typesInstances[dropNumber[currentDrop]].SetActive(true);
            }
        }

    }

    Vector2 GetRandomPositionAroundPoint(Vector2 center, float maxDistance)
    {
        // Obtener una posición aleatoria alrededor de un punto central dentro de una distancia máxima
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float distance = Random.Range(0f, maxDistance);
        float x = center.x + distance * Mathf.Cos(angle);
        float y = center.y + distance * Mathf.Sin(angle);

        return new Vector2(x, y);
    }
}
