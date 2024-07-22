using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAsign : MonoBehaviour
{
    IAbility ability;

    private void Start()
    {
        ability = gameObject.GetComponent<IAbility>();


        if (AbilitiesManager.Instance.qAbility == null)
        {
            AbilitiesManager.Instance.qAbility = ability;
        }
        else
        {
            AbilitiesManager.Instance.eAbility = ability;
        }

    }

    private void OnDestroy()
    {
        if (AbilitiesManager.Instance.qAbility == ability)
        {
            AbilitiesManager.Instance.qAbility = null;
        }

        else
        {
            AbilitiesManager.Instance.eAbility = null;
        }
    }
}
