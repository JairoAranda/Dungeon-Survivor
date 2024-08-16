using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAsign : MonoBehaviour
{
    IAbility ability;

    private void Start()
    {
        ability = gameObject.GetComponent<IAbility>();


        if (AbilitiesManager.instance.qAbility == null)
        {
            AbilitiesManager.instance.qAbility = ability;
        }
        else
        {
            AbilitiesManager.instance.eAbility = ability;
        }

    }

    private void OnDestroy()
    {
        if (AbilitiesManager.instance.qAbility == ability)
        {
            AbilitiesManager.instance.qAbility = null;
        }

        else
        {
            AbilitiesManager.instance.eAbility = null;
        }
    }
}
