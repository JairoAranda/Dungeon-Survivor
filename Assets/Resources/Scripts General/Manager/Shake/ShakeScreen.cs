using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{

    CinemachineImpulseSource m_Source;

    [Header("Shake Config")]
    [Space]
    [Range(0.1f, 10f)]
    [SerializeField] float intesity = 1f;

    private void Awake()
    {
        m_Source = GetComponent<CinemachineImpulseSource>();
    }

    protected void TriggerShake(GameObject go)
    {
        m_Source.GenerateImpulseWithForce(intesity);
    }


}
