using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPool : MonoBehaviour
{
    [Header("Pool Data")]
    [Space]
    [Range(1, 50)]
    [Tooltip("N�mero total de instancias del objeto en el pool.")]
    public int poolSize = 20;
    [Tooltip("Tipos de objetos que se instanciar�n en el pool.")]
    [SerializeField] protected GameObject[] types;

    [HideInInspector]
    public GameObject[] typesInstances; // Instancias de los objetos en el pool.

    protected virtual void Start()
    {
        typesInstances = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            // Instancia un objeto aleatorio del array de tipos y lo coloca en la posici�n (0, 0).
            typesInstances[i] = Instantiate(types[Random.Range(0, types.Length)], new Vector2(0, 0f), Quaternion.identity);
            // Ajusta el �ndice del transform del objeto para mantener el orden en la jerarqu�a.
            typesInstances[i].transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);
        }
    }
}
