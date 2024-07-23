using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralReciveDrop : MonoBehaviour
{
    public EnumDropType type;

    public bool isPlaying;

    [Range(0.1f , 5f)]
    [SerializeField] private float time = 1;

    Tweener tweener;

    public void StartAnim()
    {
        tweener = transform.DOMove(PlayerStats.instance.transform.position, time).OnComplete(() => AnimDone());

        CheckLastPost();
    }

    IEnumerator CheckLastPost()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (transform.position != PlayerStats.instance.transform.position)
            {
                tweener.ChangeEndValue(PlayerStats.instance.transform.position, true);
            }
        }
    }

    //protected virtual void Update()
    //{
    //    if (canRecieve)
    //    {
    //        transform.DOMove(PlayerStats.instance.transform.position, time).OnComplete(() => AnimDone());
    //    }
    //}


    protected virtual void AnimDone()
    {
        StopAllCoroutines();

        isPlaying = false;

        gameObject.SetActive(false);
    }

}
