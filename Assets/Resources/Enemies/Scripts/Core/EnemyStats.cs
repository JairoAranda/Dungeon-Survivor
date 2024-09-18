using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyStats : MonoBehaviour, IStats
{
    // Eventos estáticos que pueden ser suscritos por otros componentes
    public static event Action<GameObject> EventTriggerHitEnemy, EventTriggerDeathEnemy;

    private SOEnemyInfo enemyInfoSO; // Información del enemigo desde el ScriptableObject

    private bool _isDead = false; // Estado de vida del enemigo
    public bool isDead
    {
        get => _isDead;
    }

    private float _life; // Vida actual del enemigo
    public float life
    {
        get => _life;
        set 
        {
            _life = value;

            if (_life <= 0)
            {
                Death();
            }
        }
    }


    private void OnEnable()
    {
        // Asegura que enemyInfoSO esté inicializado
        if (enemyInfoSO == null)
        {
            enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;
        }

        // Resetea el estado de muerte y establece la vida del enemigo
        _isDead = false;
        life = enemyInfoSO.health;
    }

    public void GetHit(float dmg)
    {
        if (_isDead) return; // No hace nada si el enemigo ya está muerto

        life -= dmg; // Reduce la vida del enemigo por el daño recibido

        if (life > 0)
        {
            // Si el enemigo aún tiene vida, invoca el evento de que el enemigo fue golpeado
            EventTriggerHitEnemy?.Invoke(gameObject);
        }
    }

    public void Death()
    {
        if (!_isDead)
        {
            _isDead = true; // Marca al enemigo como muerto
            EventTriggerDeathEnemy?.Invoke(gameObject); // Invoca el evento de muerte
            gameObject.SetActive(false); // Desactiva el GameObject
        }
        
    }
}
