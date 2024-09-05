using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarManager : MonoBehaviour
{
    [SerializeField] Image XPBar;

    float maxXP;

    private void OnEnable()
    {
        PlayerStats.EventTriggerLevelUp += UpdateMaxXP;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerLevelUp -= UpdateMaxXP;
    }

    private void Start()
    {
        UpdateMaxXP();
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        UpdateMaxXP();
    }

    void UpdateMaxXP()
    {
        maxXP = PlayerStats.instance.xpMax;
    }

    private void FixedUpdate()
    {
        if (PlayerStats.instance != null)
        {
            XPBar.fillAmount = PlayerStats.instance.xp / maxXP;
        }       
    }
}
