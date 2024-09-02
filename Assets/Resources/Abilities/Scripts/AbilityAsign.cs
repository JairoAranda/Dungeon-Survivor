using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityAsign : MonoBehaviour
{
    IAbility ability;

    private void Start()
    {
        ability = gameObject.GetComponent<IAbility>();


        if (AbilitiesManager.instance.qAbility == null)
        {
            ability.keycode = KeyCode.Q;

            ability.CDimg = AbilitiesManager.instance.qCdImg;

            AbilitiesManager.instance.qImg.sprite = ability.img;

            AbilitiesManager.instance.qAbility = ability;
        }
        else
        {

            ability.keycode = KeyCode.E;

            ability.CDimg = AbilitiesManager.instance.eCdImg;

            AbilitiesManager.instance.eImg.sprite = ability.img;

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
