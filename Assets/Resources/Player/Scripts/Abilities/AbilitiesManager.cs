using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance;

    public IAbility qAbility, eAbility;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && qAbility != null)
        {
            if (qAbility.currentCD <= 0)
            {
                qAbility.currentCD = qAbility.cd;

                qAbility.Ability();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.E) && eAbility != null)
        {
            if (eAbility.currentCD <= 0)
            {
                eAbility.currentCD = eAbility.cd;

                eAbility.Ability();
            }
            
        }
    }
}
