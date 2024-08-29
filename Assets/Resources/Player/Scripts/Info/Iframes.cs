using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SOFinderPlayer))]
public class Iframes : MonoBehaviour
{
    [Tooltip("Flicker time on seconds")]
    [Space]
    [Range(0f, 1f)]
    [SerializeField] float animFlicker = 0.1f;

    private SpriteRenderer spriteRenderer;
    private SOPlayerInfo sOFinderPlayer;
    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += StartIframes;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= StartIframes;
    }

    private void Start()
    {
        sOFinderPlayer = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void StartIframes(GameObject go)
    {
        StartCoroutine(iFrameTime());
        StartCoroutine(iFrameAnim());
    }
    private IEnumerator iFrameTime()
    {
        PlayerStats.instance.canBeHit = false;

        yield return new WaitForSeconds(sOFinderPlayer.iFrames);

        PlayerStats.instance.canBeHit = true;
        spriteRenderer.enabled = true;
    }

    private IEnumerator iFrameAnim()
    {
        while (!PlayerStats.instance.canBeHit)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(animFlicker);
        }


    }
}
