using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UpdateMeleeWeapon))]
public class MeleeDmg : MonoBehaviour
{
    public static MeleeDmg instance;

    [HideInInspector]
    public IEffectType effectType; // Tipo de efecto asociado al da�o

    [SerializeField] LayerMask enemyLayer; // Capa de enemigos para detectar colisiones

    [HideInInspector]
    public float dmg; // Da�o causado por el ataque

    [HideInInspector]
    public bool canDmg; // Indica si el ataque puede causar da�o


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el collider pertenece a la capa de enemigos y si el ataque puede causar da�o
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0 && canDmg)
        {
            // Obtiene el componente EnemyStats del enemigo
            EnemyStats enemy = collision.GetComponentInParent<EnemyStats>();

            // Aplica el da�o al enemigo
            if (enemy != null)
            {
                enemy.GetHit(dmg);
            }

            // Aplica el efecto asociado al ataque
            effectType.Effect(collision.gameObject);
        }
    }
}
