using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera m_Camera;

    GameObject player;

    private void Start()
    {
        m_Camera = GetComponent<CinemachineVirtualCamera>();

        player = PlayerStats.instance.gameObject;

        m_Camera.Follow = player.transform;

        m_Camera.LookAt = player.transform;
    }
}
