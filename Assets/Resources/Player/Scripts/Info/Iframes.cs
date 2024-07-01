using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SOFinderPlayer))]
public class Iframes : MonoBehaviour
{

    private BoxCollider2D boxCollider2D;
    private SOPlayerInfo sOFinderPlayer;
    private void OnEnable()
    {
        PlayerStats.EventTriggerHit += startIframes;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHit -= startIframes;
    }

    private void Start()
    {
        sOFinderPlayer = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void startIframes()
    {
        StartCoroutine(iFrameTime());
    }
    private IEnumerator iFrameTime()
    {
        boxCollider2D.enabled = false;

        yield return new WaitForSeconds(sOFinderPlayer.iFrames);

        boxCollider2D.enabled = true;
    }
}
