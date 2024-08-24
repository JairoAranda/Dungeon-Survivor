using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] Transform handPosition;

    void Update()
    {
        transform.position = handPosition.position;
    }
}
