using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivaeScripts : MonoBehaviour
{
    [SerializeField] List <MonoBehaviour> scripts;

    [SerializeField] bool deactivePlayer;

    private void Awake()
    {
        if (deactivePlayer)
        {
            GameObject player = PlayerStats.instance.gameObject;

            scripts.Add(player.GetComponent<ShootController>());

            scripts.Add(player.GetComponent<MeleeHitController>());

            scripts.Add(player.GetComponentInChildren<HandMovement>());

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
    }
}
