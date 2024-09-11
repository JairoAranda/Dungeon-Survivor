using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShoot : BaseRangedAbility, IAbility
{
    [SerializeField] int shoots = 4;

    [SerializeField] float delayShoot = 0.25f;

    public void Ability()
    {
        StartCoroutine(DelayShoot());
    }

    IEnumerator DelayShoot()
    {
        for (int i = 0; i < shoots; i++)
        {
            AddPool();

            Bullet();

            SetAtributes();

            AddForce(0);

            yield return new WaitForSeconds(delayShoot);
        }
    }
}
