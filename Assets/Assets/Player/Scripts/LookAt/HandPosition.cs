using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] Transform handPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = handPosition.position;
    }
}
