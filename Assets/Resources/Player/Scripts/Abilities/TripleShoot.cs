using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShoot : BaseAbility, IAbility
{
    [SerializeField] Transform handPoint;

    GameObject bulletToShoot;

    Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
    }
    public void Ability()
    {

        for (int i = -30; i <= 30; i+=30) 
        {
            AddPool();

            bulletToShoot = abilitiesManager.typesInstances[abilitiesManager.bulletNumber];

            AddForce(bulletToShoot, i);
        }
        
    }

    void AddPool()
    {
        abilitiesManager.bulletNumber++;

        if (abilitiesManager.bulletNumber > abilitiesManager.poolSize - 1)
        {
            abilitiesManager.bulletNumber = 0;
        }
    }

    void AddForce(GameObject bullet, int zAngle)
    {
        bulletToShoot.transform.position = handPoint.position;

        bulletToShoot.SetActive(true);

        bulletToShoot.transform.eulerAngles = new Vector3(0, 0, zAngle);

        rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(bulletToShoot.transform.forward * sOPlayerInfo.speed, ForceMode2D.Force);
    }
}
