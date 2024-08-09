using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralReciveDrop : MonoBehaviour
{
    [Header("Drop type")]
    public EnumDropType type;

    [HideInInspector]
    public bool isPlaying;

    [Header("Collect time")]
    [Range(0.1f , 5f)]
    [SerializeField] private float time = 1;

    Tweener tweener;

    float currentTime;

    protected SOPlayerInfo sOPlayerInfo;

    private void Start()
    {
        sOPlayerInfo = PlayerStats.instance.GetComponent<SOFinderPlayer>().sOPlayerInfo;
    }

    public void StartAnim()
    {
        currentTime = 0;

        tweener = transform.DOMove(PlayerStats.instance.transform.position, time)
            .SetEase(Ease.InBack)
            .OnUpdate(() =>
            {
                currentTime += Time.deltaTime;

                if (currentTime >= time/2 && currentTime <= time)
                {
                    tweener.SetEase(Ease.Linear);

                    tweener.ChangeEndValue(PlayerStats.instance.transform.position, time - currentTime ,true);

                }


            })
            .OnComplete(() => AnimDone());

    }

    protected virtual void AnimDone()
    {

        isPlaying = false;

        gameObject.SetActive(false);

    }

}
