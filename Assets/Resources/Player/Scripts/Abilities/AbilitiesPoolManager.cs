using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesPoolManager : GeneralPool
{
    public static AbilitiesPoolManager Instance;

    [HideInInspector]
    public int bulletNumber = -1;

    public IAbility qAbility, eAbility;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && qAbility.currentCD == 0)
        {
            if (qAbility != null)
            {
                qAbility.Ability();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.E) && eAbility.currentCD == 0)
        {
            if (eAbility != null)
            {
                eAbility.Ability();
            }
            
        }
    }
}
