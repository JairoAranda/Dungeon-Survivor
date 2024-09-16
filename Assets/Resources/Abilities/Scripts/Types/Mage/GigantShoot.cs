using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class GigantShoot : BaseRangedAbility, IAbility
{
    [SerializeField] Vector3 targetScale = new Vector3 (2, 2, 2);

    [SerializeField] float targetDmgMultiplier = 2f;

    protected override void OnDisable()
    {
        base.OnDisable();

        StopAllCoroutines();
    }

    public void Ability()
    {
        AddPool();

        Bullet();

        SetAtributes();

        StartCoroutine(ScaleOverTime());

        AddForce(0);
    }


    IEnumerator ScaleOverTime()
    {
        float duration = sOPlayerInfo.range / sOPlayerInfo.projectSpeed;

        float elapsedTime = 0f;

        Vector3 initialScale = bulletToShoot.transform.localScale;

        Projectile projectile = bulletToShoot.GetComponent<Projectile>();

        float initialDmg = projectile.dmg;

        float targetDmg = initialDmg * targetDmgMultiplier;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            bulletToShoot.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);

            projectile.dmg = Mathf.Lerp(initialDmg, targetDmg, elapsedTime / duration);

            yield return null;
        }

        bulletToShoot.transform.localScale = initialScale;
    }

}
