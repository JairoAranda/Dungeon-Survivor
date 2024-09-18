using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactDmg : MonoBehaviour
{
    private SOEnemyInfo enemyInfoSO; // Información sobre el enemigo

    void Start()
    {
        // Obtener el componente SOFinderEnemy del objeto padre y su información sobre el enemigo
        enemyInfoSO = GetComponentInParent<SOFinderEnemy>().enemyInfoSO;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Comprobar si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.gameObject.tag == "Player")
        {
            // Aplicar daño al jugador
            PlayerStats.instance.GetHit(enemyInfoSO.damage);
        }
    }

}
