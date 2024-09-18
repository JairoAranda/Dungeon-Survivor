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
    [SerializeField] float animFlicker = 0.1f; // Tiempo de parpadeo en segundos

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
        // Obtiene referencias a componentes necesarios
        sOFinderPlayer = GetComponent<SOFinderPlayer>().sOPlayerInfo;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void StartIframes(GameObject go)
    {
        // Inicia las corutinas para manejar el tiempo de invulnerabilidad y la animación de parpadeo
        StartCoroutine(iFrameTime());
        StartCoroutine(iFrameAnim());
    }
    private IEnumerator iFrameTime()
    {
        // Desactiva la capacidad de ser golpeado (invulnerabilidad)
        PlayerStats.instance.canBeHit = false;

        yield return new WaitForSeconds(sOFinderPlayer.iFrames);

        // Reactiva la capacidad de ser golpeado
        PlayerStats.instance.canBeHit = true;
        spriteRenderer.enabled = true; // Asegura que el sprite sea visible al final del período de invulnerabilidad
    }

    private IEnumerator iFrameAnim()
    {
        // Mientras el jugador no pueda ser golpeado, alterna la visibilidad del sprite
        while (!PlayerStats.instance.canBeHit)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(animFlicker);
        }


    }
}
