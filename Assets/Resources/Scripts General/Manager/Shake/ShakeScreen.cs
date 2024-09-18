using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    CinemachineImpulseSource m_Source; // Fuente de impulso para la sacudida.

    [Header("Shake Config")]
    [Space]
    [Range(0.1f, 10f)]
    [SerializeField] float intesity = 1f; // Intensidad del efecto de sacudida.

    private void Awake()
    {
        // Obtiene el componente CinemachineImpulseSource.
        m_Source = GetComponent<CinemachineImpulseSource>();
    }

    // Activa el efecto de sacudida en la pantalla con la intensidad especificada.
    protected void TriggerShake(GameObject go)
    {
        // Genera el impulso de sacudida con la intensidad configurada.
        m_Source.GenerateImpulseWithForce(intesity);
    }
}
