using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour, IBulletType
{

    [SerializeField] float fireDmg = 10, burnTime = 1;

    public void Effect(GameObject target, bool isPlayer)
    {
        if (isPlayer)
        {

            StartCoroutine(PlayerFireDamage(PlayerStats.instance, fireDmg, burnTime));
        }
        else
        {
            var enemyStats = target.GetComponent<EnemyStats>();

            StartCoroutine(PlayerFireDamage(enemyStats, fireDmg, burnTime));
        }
    }

    IEnumerator PlayerFireDamage(IStats targetStats, float totalDamage, float duration)
    {
        float elapsedTime = 0;
        float damagePerSecond = totalDamage / duration;
        while (elapsedTime < duration)
        {
            targetStats.life -= damagePerSecond * Time.deltaTime;
            Debug.Log(targetStats.life);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
