using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SOFinderPlayer))]
public class ShootController : EnemyDetector
{
    public static event Action<GameObject> EventTriggerShoot; // Evento que se dispara cuando se realiza un disparo

    [Header("Bullet Color")]
    [Space]
    [ColorUsage(true, true)]
    [SerializeField] Color bulletColor; // Color de las balas

    [Header("Inputs")]
    [SerializeField] InputActionAsset inputActions;

    [SerializeField] InputActionEnum action;
    private InputAction shootAction;

    private void OnEnable()
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");

        shootAction = gameplayMap.FindAction(action.ToString());

        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
    }

    void Update()
    {
        lastShootTime += Time.deltaTime;

        // Determina el tipo de disparo basado en la opción automática o el clic del mouse
        if (OptionManager.instance.isAuto)
        {
            AutoShot();
        }
        else if (shootAction.ReadValue<float>() > 0)
        {
            TargetShot();
        }
    }

    void AutoShot()
    {
        // Detecta el enemigo más cercano
        Transform closestEnemy = DetectClosestEnemy();

        // Calcula el tiempo de vida de la bala basado en la distancia y la velocidad del proyectil
        float lifeTime = detectionRange / (sOPlayerInfo.projectSpeed / 50);

        // Verifica si hay un enemigo cercano y si ha pasado el tiempo de enfriamiento
        if (closestEnemy != null && lastShootTime > shootCooldown)
        {
            // Dispara el evento de disparo
            EventTriggerShoot(gameObject);

            // Llama al método para disparar una bala desde el pool de proyectiles
            ProjectilePool.instance.ShootBullet(
                handPosition.position,
                sOPlayerInfo.projectSpeed,
                PlayerStats.instance.dmg,
                detectionRange, closestEnemy.position,
                handPosition.position,
                detectionLayer,
                gameObject,
                bulletColor
            );
            lastShootTime = 0; // Reinicia el temporizador del último disparo
        }
    }

    void TargetShot()
    {
        // Obtiene la posición del ratón en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegura que la posición en Z sea 0

        // Calcula el tiempo de vida de la bala basado en la distancia y la velocidad del proyectil
        float lifeTime = detectionRange / (sOPlayerInfo.projectSpeed / 50);

        // Verifica si ha pasado el tiempo de enfriamiento
        if (lastShootTime > shootCooldown)
        {
            // Dispara el evento de disparo
            EventTriggerShoot(gameObject);

            // Llama al método para disparar una bala desde el pool de proyectiles
            ProjectilePool.instance.ShootBullet(
                handPosition.position,
                sOPlayerInfo.projectSpeed,
                PlayerStats.instance.dmg,
                detectionRange,
                mousePosition,
                handPosition.position,
                detectionLayer,
                gameObject,
                bulletColor
            );
            lastShootTime = 0; // Reinicia el temporizador del último disparo
        }
    }

}
