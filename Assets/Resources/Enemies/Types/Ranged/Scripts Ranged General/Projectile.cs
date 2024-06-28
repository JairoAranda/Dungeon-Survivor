using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    private void OnEnable()
    {
        Debug.Log(Direction());

        GetComponent<Rigidbody2D>().AddForce(Direction() * speed, ForceMode2D.Force);
    }
    private void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector3(0, 0);
        gameObject.SetActive(false);
    }

    private Vector2 Direction()
    {
        Vector2 direction = (PlayerInfo.instance.player.transform.position - transform.position).normalized;

        return direction;
    }
}
