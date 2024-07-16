using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveDrop : MonoBehaviour
{
    //public static event Action EventGetMoney;

    public bool canRecieve = false;

    [Range(0.1f , 5f)]
    [SerializeField] private float time = 1;


    private void Update()
    {
        if (canRecieve)
        {
            transform.DOMove(PlayerStats.instance.transform.position, time).OnComplete(() => AnimDone());
        }
    }

    void AnimDone()
    {
        //EventGetMoney();

        gameObject.SetActive(false);
    }

}
