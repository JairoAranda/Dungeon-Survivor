using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralReciveDrop : MonoBehaviour
{
    public bool canRecieve = false;

    public EnumDropType type;

    [Range(0.1f , 5f)]
    [SerializeField] private float time = 1;

    protected virtual void Update()
    {
        if (canRecieve)
        {
            transform.DOMove(PlayerStats.instance.transform.position, time).OnComplete(() => AnimDone());
        }
    }

    protected virtual void AnimDone()
    {
        canRecieve = false;
        gameObject.SetActive(false);
    }

}
