using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireEffect : MonoBehaviour, IEffectType
{
    [Range(0.1f, 5f)]
    [SerializeField] float fireDmg = 1, burnTime = 1; // Daño y tiempo total del fuego aplicado al objetivo.

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

    // Aplica el efecto de fuego al objetivo especificado.
    public void Effect(GameObject target)
    {
        if (target.tag == "Player")
        {
            // Aplica daño por fuego al jugador y sigue el objetivo con partículas de fuego
            StartCoroutine(PlayerFireDamage(PlayerStats.instance, fireDmg, burnTime));
            StartCoroutine(FireParticlePool.instance.FireFollow(target, burnTime));
        }
        else if (target.tag == "Enemy")
        {
            // Aplica daño por fuego al enemigo y sigue el objetivo con partículas de fuego
            var enemyStats = target.GetComponent<EnemyStats>();
            StartCoroutine(PlayerFireDamage(enemyStats, fireDmg, burnTime));
            StartCoroutine(FireParticlePool.instance.FireFollow(target, burnTime));
        }

    }

    /// <summary>
    /// Aplica daño por fuego a un objetivo durante un período de tiempo.
    /// </summary>
    /// <param name="targetStats">Las estadísticas del objetivo que recibirá el daño.</param>
    /// <param name="totalDamage">El daño total que se aplicará durante el tiempo de combustión.</param>
    /// <param name="duration">La duración durante la cual se aplica el daño.</param>
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
