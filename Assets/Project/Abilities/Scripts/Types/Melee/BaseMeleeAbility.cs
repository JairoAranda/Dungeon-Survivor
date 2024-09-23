using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseMeleeAbility : BaseAbility
{
    // Referencia a la posición del brazo
    private Transform _armPosition;
    public Transform armPosition
    {
        get => _armPosition;
        set => _armPosition = value;
    }

    // Referencia a los atributos de daño cuerpo a cuerpo
    private MeleeDmg _meleeDmg;
    public MeleeDmg meleeDmg
    {
        get => _meleeDmg;
        set => _meleeDmg = value;
    }

    // Referencia al movimiento de la mano
    private HandMovement _handMovement;
    public HandMovement handMovement
    {
        get => _handMovement;
        set => _handMovement = value;
    }

    protected virtual void OnEnable()
    {
        // Obtiene las referencias a la posición del brazo y al movimiento de la mano desde MeleeHitController
        armPosition = MeleeHitController.instance.armPosition.transform;
        handMovement = MeleeHitController.instance.handMovement;

    }

    protected virtual void Atributes()
    {
        // Obtiene la referencia a los atributos de daño cuerpo a cuerpo desde MeleeHitController
        meleeDmg = MeleeHitController.instance.meleeDmg;

        // Obtiene el componente BoxCollider2D del objeto de daño cuerpo a cuerpo
        BoxCollider2D boxCollider2D = meleeDmg.gameObject.GetComponent<BoxCollider2D>();

        // Desactiva y luego reactiva el BoxCollider2D para actualizar su estado
        boxCollider2D.enabled = false;
        boxCollider2D.enabled = true;

        // Desactiva el movimiento de la mano
        handMovement.enabled = false;

        // Configura los atributos de daño cuerpo a cuerpo
        meleeDmg.canDmg = true;
        meleeDmg.effectType = MeleeHitController.instance.effect;
        meleeDmg.dmg = PlayerStats.instance.dmg;

        // Desactiva el flag de "puede golpear" en MeleeHitController
        MeleeHitController.instance.candHit = false;
    }
}
