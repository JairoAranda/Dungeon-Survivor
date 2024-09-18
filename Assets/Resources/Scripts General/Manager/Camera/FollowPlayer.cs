using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera m_Camera; // La c�mara virtual de Cinemachine que se ajustar� para seguir al jugador.

    GameObject player; // El objeto del jugador que la c�mara seguir�.

    private void Start()
    {
        // Obtiene el componente CinemachineVirtualCamera adjunto a este objeto.
        m_Camera = GetComponent<CinemachineVirtualCamera>();

        // Obtiene el objeto del jugador desde PlayerStats.
        player = PlayerStats.instance.gameObject;

        // Configura la c�mara para que siga y mire al jugador.
        m_Camera.Follow = player.transform;
        m_Camera.LookAt = player.transform;
    }
}
