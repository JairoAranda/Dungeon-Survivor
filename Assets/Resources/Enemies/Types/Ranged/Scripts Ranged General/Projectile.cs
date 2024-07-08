using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float speed, dmg;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public string hitTag;

    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(Direction() * speed, ForceMode2D.Force);
    }
    private void OnBecameInvisible()
    {
        EndProjectile();
    }

    private Vector2 Direction()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == hitTag)
        {
            if (hitTag == "Enemy")
            {
                other.GetComponent<EnemyStats>().GetHit(dmg);
            }
            
            else if (hitTag == "Player")
            {
                PlayerStats.instance.Hit(dmg);

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
