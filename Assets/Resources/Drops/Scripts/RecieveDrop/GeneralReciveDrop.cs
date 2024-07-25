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

    [SerializeField] AnimationCurve animCurve;

    public void StartAnim()
    {
        StartCoroutine(PickUpAnim());
    }

    IEnumerator PickUpAnim()
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;

        while (timeElapsed < time)
        {
            Vector3 endPosition = PlayerStats.instance.transform.position;

            float t = animCurve.Evaluate(timeElapsed / time);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        AnimDone();
    }


    protected virtual void AnimDone()
    {

        StopAllCoroutines();

        isPlaying = false;

        gameObject.SetActive(false);

    }

}
