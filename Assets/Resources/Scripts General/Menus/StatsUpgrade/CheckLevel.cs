using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevel : MonoBehaviour
{
    [Header("Stat config")]
    [Space]
    [SerializeField] GameObject[] levels;

    [SerializeField] string stat;
    void Start()
    {
        int i = 2;

        foreach (GameObject level in levels)
        {
            if (PlayerPrefs.GetInt(stat, 1) >= i)
            {
                level.GetComponent<Image>().color = Color.yellow;
                i++;
            }
        }
    }

}
