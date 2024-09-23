using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWeaponRange : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer; // Capa para detectar enemigos en el rango de ataque
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Verifica si el collider pertenece a la capa de enemigos y si el auto ataque está habilitado
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0 && OptionManager.instance.isAuto)
        {
            MeleeHitController.instance.AutoHit();
        }
    }
}
