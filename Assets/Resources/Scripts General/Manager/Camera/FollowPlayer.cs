using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera m_Camera; // La cámara virtual de Cinemachine que se ajustará para seguir al jugador.

    GameObject player; // El objeto del jugador que la cámara seguirá.

    private void Start()
    {
        // Obtiene el componente CinemachineVirtualCamera adjunto a este objeto.
        m_Camera = GetComponent<CinemachineVirtualCamera>();

        // Obtiene el objeto del jugador desde PlayerStats.
        player = PlayerStats.instance.gameObject;

        // Configura la cámara para que siga y mire al jugador.
        m_Camera.Follow = player.transform;
        m_Camera.LookAt = player.transform;
    }
}
