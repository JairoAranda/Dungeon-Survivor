using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderPlayer))]
public class MeleeHitController : MonoBehaviour
{
    public static MeleeHitController Instance;

    public bool isAuto;

    [SerializeField] GameObject armPosition;

    [SerializeField] HandMovement handMovement;

    [Range(1f, 180f)]
    [SerializeField] float swingAngle;
    [Range(0.1f, 1f)]
    [SerializeField] float duration;

    private float currentCD;

    private IBulletType effect;

    SOPlayerInfo playerInfo;

    MeleeDmg meleeDmg;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        effect = GetComponent<IBulletType>();
    }

    private void Update()
    {
        Timer();

        if (!isAuto)
        {
            ManualHit();
        }
    }

    void Timer()
    {
        if(currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }

    public void AutoHit()
    {
        if (currentCD <= 0)
        {
            Swing();

            currentCD = playerInfo.cooldown;
        }
    }

    void ManualHit()
    {
        if (currentCD <= 0 && Input.GetMouseButtonDown(0))
        {
            Swing();

            currentCD = playerInfo.cooldown;
        }
    }

    void Swing()
    {
        if (meleeDmg == null)
        {
            meleeDmg = GetComponentInChildren<MeleeDmg>();
        }
        
        float endRotation = armPosition.transform.localEulerAngles.z + swingAngle;

        handMovement.enabled = false;

        meleeDmg.canDmg = true;

        meleeDmg.effectType = effect;

        meleeDmg.dmg = playerInfo.damage;

        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        armPosition.transform.DOLocalRotate(new Vector3(0, 0, endRotation), duration).SetEase(Ease.InOutCirc).OnComplete(() => SwingDone());
    }

    void SwingDone()
    {
        meleeDmg.canDmg = false;

        handMovement.enabled = true;

        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);
    }
}
