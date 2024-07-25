using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWeaponRange : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0 && MeleeHitController.Instance.isAuto)
        {
            MeleeHitController.Instance.AutoHit();
        }
    }

}
