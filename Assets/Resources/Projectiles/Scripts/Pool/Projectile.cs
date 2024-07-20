using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask playerLayer;

    [Header("Bullet Effects")]
    private IBulletType effectType;

    [HideInInspector]
    public float speed, dmg, timeToDie;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public LayerMask hitLayer;
    [HideInInspector]
    public bool lifeTime;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(Direction() * speed, ForceMode2D.Force);

        if (lifeTime)
        {
            StartCoroutine(LifeTime());
        }
        
    }
    private void OnBecameInvisible()
    {
        EndProjectile();
    }

    private Vector2 Direction()
    {
        Vector2 direction = (target - transform.position).normalized;

        return direction;
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(timeToDie);

        EndProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitLayer) != 0)
        {
            if (hitLayer == enemyLayer)
            {
                effectType = PlayerStats.instance.GetComponent<IBulletType>();

                other.GetComponent<EnemyStats>().GetHit(dmg);

                effectType.Effect(other.gameObject, false);
            }
            
            else if (hitLayer == playerLayer)
            {
                effectType = other.GetComponent<IBulletType>();

                PlayerStats.instance.Hit(dmg);

                effectType.Effect(other.gameObject, true);
            }
            EndProjectile();
        }
    }

    void EndProjectile()
    {
        gameObject.transform.position = new Vector3(0, 0);
        gameObject.SetActive(false);
    }


}
