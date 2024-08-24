using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundTimerManager : MonoBehaviour
{
    [Header("Timer Options")]
    [Space]
    [Range(0f, 180f)]
    public int roundTime;

    private void Start()
    {
        StartCoroutine(Countdown(roundTime));
    }

    private IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }

        SceneManager.LoadScene(0);
    }
}
