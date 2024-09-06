using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UpdateMeleeWeapon))]
public class MeleeDmg : MonoBehaviour
{
    public static MeleeDmg instance;

    [HideInInspector]
    public IEffectType effectType;

    [SerializeField] LayerMask enemyLayer;

    [HideInInspector]
    public float dmg;

    [HideInInspector]
    public bool canDmg;


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
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0 && canDmg)
        {

            Debug.Log("test");

            EnemyStats enemy = collision.GetComponentInParent<EnemyStats>();

            enemy.GetHit(dmg);

            effectType.Effect(collision.gameObject);

            
        }
    }

}
