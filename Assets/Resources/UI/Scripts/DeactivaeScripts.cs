using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivaeScripts : MonoBehaviour
{
    [SerializeField] List <MonoBehaviour> scripts;

    [SerializeField] bool deactivePlayer;

    MeleeHitController meleeHitController;

    HandMovement handMovement;

    private void Awake()
    {
        if (deactivePlayer)
        {
            GameObject player = PlayerStats.instance.gameObject;

            scripts.Add(player.GetComponent<ShootController>());

            meleeHitController = player.GetComponent<MeleeHitController>();

            handMovement = player.GetComponentInChildren<HandMovement>();

            scripts.Add(player.GetComponent<PlayerAnimation>());
        }

    }

    private void OnEnable()
    {
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        if (meleeHitController != null)
        {
            meleeHitController.candHit = false;
        }

        if (handMovement != null)
        {
            handMovement.canAim = false;
        }
    }

    private void OnDisable()
    {
        foreach(var script in scripts)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }

        if (meleeHitController != null)
        {
            meleeHitController.candHit = true;

        }

        if (handMovement != null)
        {
            handMovement.canAim = true;
        }
    }
}
