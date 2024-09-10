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
    [SerializeField] int floorTime;

    private EnemyPoolManager enemyPoolManager;

    private GameObject endMenu;

    public int floorTimeRemaining;

    public int currentFloor = 0;

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



    private IEnumerator Countdown(int time)
    {
        floorTimeRemaining = time;

        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            floorTimeRemaining = time;
        }

        enemyPoolManager = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<EnemyPoolManager>();

        foreach (var enemy in enemyPoolManager.typesInstances)
        {
            enemy.gameObject.SetActive(false);
        }

        endMenu = GameObject.FindGameObjectWithTag("EndMenu");

        endMenu.transform.GetChild(0).gameObject.SetActive(true);
        
        enemyPoolManager.gameObject.SetActive(false);
    }
}
