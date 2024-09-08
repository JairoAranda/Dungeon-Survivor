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

    public int floorTimeRemaining;

    public int currentFloor = 1;

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
            currentFloor = 1;

            StopAllCoroutines();
        }

        else if (scene.buildIndex == 1)
        {
            StartCoroutine(Countdown(floorTime));
        }
    }


    private void Start()
    {
        StartCoroutine(Countdown(floorTime));
    }

    private IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
            floorTimeRemaining = time;
            yield return new WaitForSeconds(1);
            time--;
        }

        currentFloor++;

        SceneManager.LoadScene(1);
    }
}
