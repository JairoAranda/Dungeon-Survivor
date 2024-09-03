using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Playables;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance;

    [HideInInspector]
    public IAbility qAbility, eAbility;

    public Image qCdImg, eCdImg, qImg, eImg;

    public TextMeshProUGUI qCdText, eCdText;

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
        Ability(qAbility);

        Ability(eAbility);
    }

    void Ability(IAbility ability)
    {
        if (ability != null)
        {
            AbilityUse(ability);

            ImgCd(ability);

            CD(ability);
        }
        
    }

    void AbilityUse(IAbility ability)
    {
        if (Input.GetKeyDown(ability.keycode))
        {
            if (ability.currentCD <= 0)
            {
                ability.currentCD = ability.cd / PlayerStats.instance.coolDownReduction;

                ability.Ability();
            }

        }
    }

    void ImgCd(IAbility ability)
    {
        ability.CDimg.fillAmount = ability.currentCD / (ability.cd / PlayerStats.instance.coolDownReduction);
    }

    void CD(IAbility ability)
    {
        if (ability.CDimg == qCdImg)
        {
            if (!qCdText.enabled)
            {
                qCdText.enabled = true;
            }
            
            qCdText.text = Math.Ceiling(ability.currentCD).ToString();

            if (qCdText.text == "0")
            {
                qCdText.enabled = false;
            }
        }
        else
        {
            if (!eCdText.enabled)
            {
                eCdText.enabled = true;
            }

            eCdText.text = Math.Ceiling(ability.currentCD).ToString();

            if (eCdText.text == "0")
            {
                eCdText.enabled = false;
            }
        }
    }

}
