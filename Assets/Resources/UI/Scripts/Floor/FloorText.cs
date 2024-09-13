using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floor, timer;

    void Start()
    {
        floor.text = "Floor : " + RoundTimerManager.instance.currentFloor;
    }

    void Update()
    {
        timer.text = RoundTimerManager.instance.floorTimeRemaining.ToString();
    }
}
