using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class ProjectilePool : GeneralPool
{
    public static ProjectilePool instance;

    private Vector3 startPosition, target; // Posiciones de inicio y objetivo del proyectil.

    [HideInInspector]
    public int shootNumber = -1; // Índice del proyectil a disparar.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Dispara un proyectil desde el pool.
    /// </summary>
    /// <param name="shootPosition">Posición de disparo del proyectil.</param>
    /// <param name="m_speed">Velocidad del proyectil.</param>
    /// <param name="m_dmg">Daño que inflige el proyectil.</param>
    /// <param name="m_range">Alcance máximo del proyectil.</param>
    /// <param name="m_target">Objetivo del proyectil.</param>
    /// <param name="m_startPosition">Posición inicial del proyectil.</param>
    /// <param name="m_Layer">Capas a las que puede colisionar el proyectil.</param>
    /// <param name="_owner">Objeto que dispara el proyectil.</param>
    /// <param name="_color">Color del proyectil.</param>
    public void ShootBullet(Vector2 shootPosition, float m_speed, float m_dmg, float m_range ,Vector3 m_target, Vector3 m_startPosition ,LayerMask m_Layer , GameObject _owner, Color _color)
    {
        // Incrementa el índice del proyectil a disparar.
        shootNumber++;

        // Si el índice supera el tamaño del pool, reinícialo al principio.
        if (shootNumber > poolSize - 1)
        {
            shootNumber = 0;
        }

        // Obtiene el proyectil de la lista de instancias.
        GameObject bulletToShoot = typesInstances[shootNumber];

        // Obtiene el componente Projectile del proyectil.
        Projectile projectileComponent = bulletToShoot.GetComponent<Projectile>();

        // Obtiene el componente Rigidbody2D para aplicar fuerza.
        Rigidbody2D rb = bulletToShoot.GetComponent<Rigidbody2D>();

        // Configurar los parámetros del proyectil
        projectileComponent.dmg = m_dmg; // Daño que inflige.
        projectileComponent.range = m_range; // Alcance máximo.
        projectileComponent.hitLayer = m_Layer; // Capas que puede impactar.
        projectileComponent.owner = _owner; // Asigna el dueño del proyectil.

        // Definir posiciones inicial y de destino
        target = m_target; // Objetivo del proyectil.
        startPosition = m_startPosition; // Posición inicial.

        // Modificar color del proyectil
        bulletToShoot.GetComponent<SpriteRenderer>().material.color = _color;

        // Establece la posición inicial del proyectil.
        bulletToShoot.transform.position = shootPosition;

        // Activa el proyectil antes de lanzarlo.
        bulletToShoot.SetActive(true);

        // Reinicia la velocidad antes de agregar fuerza
        rb.velocity = Vector2.zero;

        // Aplica la fuerza al proyectil en la dirección calculada con la velocidad indicada.
        rb.AddForce(Direction() * m_speed, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Calcula la dirección normalizada del proyectil desde su posición inicial hacia el objetivo.
    /// </summary>
    /// <returns>Vector2 con la dirección del disparo.</returns>
    private Vector2 Direction()
    {
        Vector2 direction = (target - startPosition).normalized; // Dirección hacia el objetivo.

        return direction;
    }
}
