using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralReciveDrop : MonoBehaviour
{
    [Header("Drop type")]
    public EnumDropType type;

    [Header("Stat Upgrade")]
    [SerializeField] private PlayerUpgradeEnum upgrade;

    [SerializeField] private string playerPrefs;

    [HideInInspector]
    public bool isPlaying;

    [Header("Stat Options")]
    [Range(0.1f , 5f)]
    [SerializeField] private float gatherTime = 1;

    [Range(0f, 10f)]
    [SerializeField] private float baseValue = 5;

    [Range(2f, 10f)]
    [SerializeField] private int multiplier = 5;

    protected float totalValue;

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

        tweener = transform.DOMove(PlayerStats.instance.transform.position, gatherTime)
            .SetEase(Ease.InBack)
            .OnUpdate(() =>
            {
                currentTime += Time.deltaTime;

                if (currentTime >= gatherTime/2 && currentTime <= gatherTime)
                {
                    tweener.SetEase(Ease.Linear);

                    tweener.ChangeEndValue(PlayerStats.instance.transform.position, gatherTime - currentTime ,true);

                }


            })
            .OnComplete(() => AnimDone());

    }

    protected virtual void AnimDone()
    {
        totalValue = baseValue* PlayerPrefs.GetInt(playerPrefs, 1) * ScaleMultiplier.ScaleFactor(multiplier, sOPlayerInfo.statUpgrades[upgrade]);

        StartCoroutine(DestoyObject());

    }

    protected virtual IEnumerator DestoyObject()
    {
        yield return new WaitForEndOfFrame();

        isPlaying = false;

        gameObject.SetActive(false);
    }

}
