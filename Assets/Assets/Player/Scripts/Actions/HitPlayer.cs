using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [SerializeField] GameObject hitSprite;

    [SerializeField] float hitDuration, hitCD;

    bool hit = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !hit)
        {
            StartCoroutine(HitAnim());
        }
    }

    IEnumerator HitAnim()
    {
        hit = true;
        hitSprite.SetActive(true);

        yield return new WaitForSeconds(hitDuration);

        hitSprite.SetActive(false);

        yield return new WaitForSeconds(hitCD);

        hit = false;

    }
}
