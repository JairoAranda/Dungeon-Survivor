using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPool : MonoBehaviour
{
    [Header("Pool Data")]
    [Range(1, 50)]
    public int poolSize = 20;
    [SerializeField] protected GameObject[] types;

    [HideInInspector]
    public GameObject[] typesInstances;

    protected virtual void Start()
    {
        typesInstances = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            typesInstances[i] = Instantiate(types[Random.Range(0, types.Length)], new Vector2(0, 0f), Quaternion.identity);
            typesInstances[i].transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + 1);
        }
    }
}
