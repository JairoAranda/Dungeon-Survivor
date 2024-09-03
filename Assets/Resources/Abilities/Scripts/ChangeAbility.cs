using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAbility : MonoBehaviour
{
    public void ChangeQ()
    {
        Destroy(AbilitiesManager.instance.qAbility.go);

        gameObject.SetActive(false);
    }

    public void ChangeE()
    {
        Destroy(AbilitiesManager.instance.eAbility.go);

        gameObject.SetActive(false);
    }
}
