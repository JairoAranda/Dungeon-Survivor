using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireEffect : MonoBehaviour, IEffectType
{
    [Range(0.1f, 5f)]
    [SerializeField] float fireDmg = 1, burnTime = 1; // Da�o y tiempo total del fuego aplicado al objetivo.

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
            // Aplica da�o por fuego al jugador y sigue el objetivo con part�culas de fuego
            StartCoroutine(PlayerFireDamage(PlayerStats.instance, fireDmg, burnTime));
            StartCoroutine(FireParticlePool.instance.FireFollow(target, burnTime));
        }
        else if (target.tag == "Enemy")
        {
            // Aplica da�o por fuego al enemigo y sigue el objetivo con part�culas de fuego
            var enemyStats = target.GetComponent<EnemyStats>();
            StartCoroutine(PlayerFireDamage(enemyStats, fireDmg, burnTime));
            StartCoroutine(FireParticlePool.instance.FireFollow(target, burnTime));
        }

    }

    /// <summary>
    /// Aplica da�o por fuego a un objetivo durante un per�odo de tiempo.
    /// </summary>
    /// <param name="targetStats">Las estad�sticas del objetivo que recibir� el da�o.</param>
    /// <param name="totalDamage">El da�o total que se aplicar� durante el tiempo de combusti�n.</param>
    /// <param name="duration">La duraci�n durante la cual se aplica el da�o.</param>
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
