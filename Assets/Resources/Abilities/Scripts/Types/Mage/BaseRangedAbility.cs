using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseRangedAbility : BaseAbility
{
    // Evento que se activa cuando se dispara una habilidad a distancia
    public static event Action<GameObject> EventTriggerAbilityShoot;

    // Referencias a las posiciones de la mano y el brazo
    private Transform handPoint;
    private Transform armPosition;

    // Componente Rigidbody2D de la bala
    Rigidbody2D rb;

    // Referencia al objeto bala que se disparará
    private GameObject _bulletToShoot;
    public GameObject bulletToShoot
    {
        get => _bulletToShoot;
        set => _bulletToShoot = value;
    }

    // Pool de proyectiles
    private ProjectilePool _projectilePool;
    public ProjectilePool projectilePool
    {
        get => _projectilePool;
        set => _projectilePool = value;
    }

    [Header("Bullet Color")]
    [Space]
    [ColorUsage(true, true)]
    [SerializeField] Color bulletColor; // Color de la bala

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Busca las posiciones de la mano y el brazo
        handPoint = GameObject.FindGameObjectWithTag("Hand").transform;
        armPosition = GameObject.FindGameObjectWithTag("Arm").transform;

    }

    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se ejecuta cuando una nueva escena es cargada
    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si la escena cargada es la de índice 1, inicializa el pool de proyectiles
        if (scene.buildIndex == 1)
        {
            ProjectPool();
        }
    }

    protected override void Start()
    {
        base.Start();

        // Inicializa el pool de proyectiles
        ProjectPool();
    }


    // Inicializa la instancia del pool de proyectiles
    protected virtual void ProjectPool()
    {

        _projectilePool = ProjectilePool.instance;
    }

    // Asigna la bala que se va a disparar
    protected virtual void Bullet()
    {
        bulletToShoot = projectilePool.typesInstances[projectilePool.shootNumber];
    }

    // Actualiza el contador del pool de proyectiles para disparar la siguiente bala
    protected virtual void AddPool()
    {
        projectilePool.shootNumber++;

        // Si el contador supera el tamaño del pool, lo reinicia
        if (projectilePool.shootNumber > projectilePool.poolSize - 1)
        {
            projectilePool.shootNumber = 0;
        }
    }

    // Configura los atributos del proyectil antes de dispararlo
    protected virtual void SetAtributes()
    {
        Projectile projectile = bulletToShoot.GetComponent<Projectile>();

        // Configura las propiedades del proyectil como el daño, rango, capa objetivo y dueño
        projectile.hitLayer = enemyLayer;
        projectile.dmg = PlayerStats.instance.dmg;
        projectile.range = sOPlayerInfo.range;
        projectile.owner = PlayerStats.instance.gameObject;

        // Asigna el color del proyectil
        projectile.GetComponent<SpriteRenderer>().material.color = bulletColor;
    }

    // Añade fuerza al proyectil para dispararlo en la dirección correcta
    protected virtual void AddForce(float zAngle)
    {
        // Posiciona la bala en el punto de la mano
        bulletToShoot.transform.position = handPoint.position;

        bulletToShoot.SetActive(true);

        // Calcula el ángulo final combinando el ángulo Z del brazo con el parámetro zAngle
        float endAngle = zAngle + armPosition.eulerAngles.z;

        // Establece la rotación del proyectil
        bulletToShoot.transform.eulerAngles = new Vector3(0, 0, endAngle);

        // Obtiene el componente Rigidbody2D del proyectil
        rb = bulletToShoot.GetComponent<Rigidbody2D>();

        // Calcula la dirección en la que se disparará el proyectil
        Vector2 direction = new Vector2(Mathf.Cos(endAngle * Mathf.Deg2Rad), Mathf.Sin(endAngle * Mathf.Deg2Rad));

        // Añade la fuerza al proyectil para dispararlo
        rb.AddForce(direction * sOPlayerInfo.projectSpeed, ForceMode2D.Impulse);

        // Dispara el evento indicando que la habilidad se ha usado
        EventTriggerAbilityShoot(gameObject);
    }
}
