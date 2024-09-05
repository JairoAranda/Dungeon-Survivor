using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDmg : MonoBehaviour
{
    public static MeleeDmg instance;

    [HideInInspector]
    public IBulletType effectType;

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
            collision.GetComponent<EnemyStats>().GetHit(dmg);

            effectType.Effect(collision.gameObject);
        }
    }

}
