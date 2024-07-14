using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveDrop : MonoBehaviour
{
    [Range(0.1f , 5f)]
    [SerializeField] private float time = 1;

    private void OnEnable()
    {
        transform.DOMove(PlayerStats.instance.player.transform.position, time);
    }



}
