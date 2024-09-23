using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer; // Capas que identifican las paredes (obst�culos que el proyectil puede colisionar).

    private IStats statsType; // Interfaz para manejar estad�sticas del objeto que colisiona.

    [HideInInspector]
    public GameObject owner; // El objeto que dispar� este proyectil.

    [HideInInspector]
    public float dmg, range; // Da�o y alcance del proyectil.
    [HideInInspector]
    public LayerMask hitLayer; // Capas a las que el proyectil puede da�ar.

    private IEffectType effectType; // Interfaz para manejar efectos al colisionar.

    private Vector2 startPosition; // Posici�n inicial del proyectil.

    private void OnEnable()
    {
        startPosition = transform.position; // Guarda la posici�n inicial cuando el proyectil se activa.

    }

    private void Update()
    {
        // Calcula la distancia recorrida por el proyectil.
        float distanceTravelled = Vector2.Distance(startPosition, transform.position);

        // Si el proyectil alcanza su rango m�ximo.
        if (distanceTravelled >= range )
        {
            // Verifica si el due�o tiene un componente que aplica efectos al colisionar.
            effectType = owner.GetComponentInChildren<IEffectType>();

            if (effectType != null )
            {
                // Aplica el efecto al proyectil.
                effectType.Effect(gameObject);
            }

            EndProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el proyectil colisiona con algo en la capa que puede recibir da�o.
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            statsType = other.GetComponent<IStats>();// Obtiene las estad�sticas del objeto impactado.
            effectType = owner.GetComponentInChildren<IEffectType>(); // Obtiene el tipo de efecto del due�o.

            if (statsType != null && effectType != null)
            {
                statsType.GetHit(dmg); // Aplica da�o al objeto impactado.

                effectType.Effect(other.gameObject); // Aplica efectos al objeto impactado.

            }

        }

        // Verifica si colisiona con una pared.
        if (((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            WallHit(); // Maneja la colisi�n con la pared.
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Si el proyectil sigue colisionando con una pared mientras permanece activo.
        if (((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            WallHit(); // Maneja el impacto continuo con la pared.
        }
    }

    void WallHit()
    {
        // Aplica el efecto si el due�o tiene un componente de tipo IEffectType.
        effectType = owner.GetComponentInChildren<IEffectType>();

        if (effectType != null)
        {
            effectType.Effect(gameObject); // Aplica el efecto al proyectil.

        }

        EndProjectile(); // Termina la vida �til del proyectil tras colisionar con una pared.
    }

    void EndProjectile()
    {
        // Reinicia la posici�n del proyectil y lo desactiva.
        gameObject.transform.position = new Vector3(0, 0);
        gameObject.SetActive(false);
    }
}
