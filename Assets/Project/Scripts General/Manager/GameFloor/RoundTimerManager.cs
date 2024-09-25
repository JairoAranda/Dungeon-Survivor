using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundTimerManager : MonoBehaviour
{
    public static RoundTimerManager instance;

    [Header("Timer Options")]
    [Space]
    [Range(0f, 180f)]
    [SerializeField] int floorTime; // Tiempo del piso en segundos.

    private EnemyPoolManager enemyPoolManager; // Referencia al gestor de la piscina de enemigos.

    private ProjectilePool projectilePool; // Referencia al gestor de la piscina de proyectiles.

    private GameObject endMenu; // Referencia al menú de fin de ronda.

    public int floorTimeRemaining; // Tiempo restante del piso en segundos.

    public int currentFloor = 0; // Piso actual.

    private void OnEnable()
    {
        SceneManager.sceneLoaded += RestartLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= RestartLevel;
    }

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

        DontDestroyOnLoad(gameObject);
    }

    void RestartLevel(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            currentFloor = 0;

            StopAllCoroutines();
        }

        else if (scene.buildIndex == 1)
        {
            StartCoroutine(Countdown(floorTime));

            currentFloor++;
        }
    }


    // Realiza una cuenta regresiva durante el tiempo especificado.
    private IEnumerator Countdown(int time)
    {
        floorTimeRemaining = time;

        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            floorTimeRemaining = time;
        }

        // Desactiva todos los enemigos/projectiles y muestra el menú de fin de ronda.
        enemyPoolManager = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();

        projectilePool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ProjectilePool>();

        foreach (var enemy in enemyPoolManager.typesInstances)
        {
            enemy.gameObject.SetActive(false);
        }

        foreach (var projectile in projectilePool.typesInstances)
        {
            projectile.gameObject.SetActive(false);
        }

        endMenu = GameObject.FindGameObjectWithTag("EndMenu");

        endMenu.transform.GetChild(0).gameObject.SetActive(true);
        
        enemyPoolManager.gameObject.SetActive(false);
    }
}
