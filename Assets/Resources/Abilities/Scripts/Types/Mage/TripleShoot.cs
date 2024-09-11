using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AbilityAsign))]
public class TripleShoot : BaseRangedAbility, IAbility
{
    [Header("Aperture Angle")]
    [Space]
    [Range(.1f, 40f)]
    [SerializeField] float shotAngle;


    public void Ability()
    {
        for (float i = -shotAngle; i <= shotAngle; i+=shotAngle) 
        {
            AddPool();

            Bullet();

            SetAtributes();

            AddForce(i);
        }
        
    }

}
