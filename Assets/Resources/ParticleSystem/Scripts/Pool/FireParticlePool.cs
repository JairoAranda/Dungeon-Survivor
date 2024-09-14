using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticlePool : GeneralPool
{
    public static FireParticlePool instance;

    int fireNumber = -1;

    private void Awake()
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


    public IEnumerator FireFollow(GameObject target, float time)
    {
        fireNumber++;

        if (fireNumber > poolSize - 1)
        {
            fireNumber = 0;
        }

        GameObject fire = typesInstances[fireNumber];

        float elapsedTime = 0;

        while(elapsedTime < time)
        {
            fire.transform.position = target.transform.position;
            fire.SetActive(true);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fire.SetActive(false);
    }
}
