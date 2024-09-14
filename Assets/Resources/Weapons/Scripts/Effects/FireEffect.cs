using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireEffect : MonoBehaviour, IEffectType
{
    [Range(0.1f, 5f)]
    [SerializeField] float fireDmg = 1, burnTime = 1;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            StopAllCoroutines();
        }
    }

    public void Effect(GameObject target)
    {
        if (target.tag == "Player")
        {
            StartCoroutine(PlayerFireDamage(PlayerStats.instance, fireDmg, burnTime));

            StartCoroutine(FireParticlePool.instance.FireFollow(target, burnTime));
        }
        else if (target.tag == "Enemy")
        {
            var enemyStats = target.GetComponent<EnemyStats>();

            StartCoroutine(PlayerFireDamage(enemyStats, fireDmg, burnTime));

            StartCoroutine(FireParticlePool.instance.FireFollow(target, burnTime));
        }

    }

    IEnumerator PlayerFireDamage(IStats targetStats, float totalDamage, float duration)
    {
        float elapsedTime = 0;
        float damagePerSecond = totalDamage / duration;

        while (elapsedTime < duration)
        {
            if (targetStats != null)
            {
                if (targetStats.isDead) yield break;

                targetStats.life -= damagePerSecond * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            else
            {
                yield break;
            }
        }
    }


}
