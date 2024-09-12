using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedHit : BaseMeleeAbility, IAbility
{
    [Header("Swing Config")]
    [Space]
    [Range(1f, 89f)]
    [SerializeField] float swingAngle;
    [Range(0.1f, 1f)]
    [SerializeField] float duration;

    [Header("Charge Config")]
    [Space]
    [ColorUsage(true, true)]
    [SerializeField] Color swordColor;
    [Range(0.1f, 3f)]
    [SerializeField] float chargeTime = 1.5f;
    [Range(1f, 4f)]
    [SerializeField] float chargeMultiplier = 2f;

    SpriteRenderer spriteRenderer;

    Color initialColor;

    private Tween rotationTween;

    public void Ability()
    {
        Atributes();

        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        meleeDmg.canDmg = false;

        spriteRenderer = meleeDmg.GetComponent<SpriteRenderer>();

        initialColor = spriteRenderer.material.color;

        float endRotation = armPosition.transform.localEulerAngles.z + swingAngle;

        meleeDmg.dmg = PlayerStats.instance.dmg;

        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        float elapsedTime = 0f;

        while (elapsedTime < chargeTime)
        {
            spriteRenderer.material.color = Color.Lerp(initialColor, swordColor, elapsedTime / chargeTime);


            elapsedTime += Time.deltaTime;


            yield return null;
        }

        meleeDmg.dmg = meleeDmg.dmg * chargeMultiplier;

        meleeDmg.canDmg = true;

        rotationTween = armPosition.transform.DOLocalRotate(new Vector3(0, 0, endRotation), duration).SetEase(Ease.InOutCirc).OnComplete(() => SwingDone());
    }

    protected override void Update()
    {
        base.Update();

        if (rotationTween != null)
        {
            if (Time.timeScale == 0 && rotationTween.IsPlaying())
            {
                // Pausa la animación si Time.timeScale es 0 y la animación está en marcha
                rotationTween.Pause();
            }

            else if (Time.timeScale == 1 && !rotationTween.IsPlaying())
            {
                // Reanuda la animación si Time.timeScale es 1 y la animación está pausada
                rotationTween.Play();
            }
        }

    }


    void SwingDone()
    {
        meleeDmg.dmg = meleeDmg.dmg / chargeMultiplier;

        meleeDmg.canDmg = false;

        handMovement.enabled = true;

        spriteRenderer.material.color = initialColor;

        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        MeleeHitController.instance.candHit = true;
    }
}
