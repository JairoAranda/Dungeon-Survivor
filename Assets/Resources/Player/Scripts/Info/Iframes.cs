using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SOFinderPlayer))]
public class Iframes : MonoBehaviour
{
    [Tooltip("Flicker time on seconds")]
    [Space]
    [Range(0f, 1f)]
    [SerializeField] float animFlicker = 0.1f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
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

        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void StartIframes()
    {
        StartCoroutine(iFrameTime());
        StartCoroutine(iFrameAnim());
    }
    private IEnumerator iFrameTime()
    {
        boxCollider2D.enabled = false;

        yield return new WaitForSeconds(sOFinderPlayer.iFrames);

        boxCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }

    private IEnumerator iFrameAnim()
    {
        while (!boxCollider2D.enabled)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(animFlicker);
        }


    }
}
