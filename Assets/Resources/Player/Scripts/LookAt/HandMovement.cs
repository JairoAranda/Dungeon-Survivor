using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandMovement : EnemyDetector
{
    protected override void Start()
    {
        detectionRange = float.PositiveInfinity;
    }

    void Update()
    {
        if (OptionManager.instance.isAuto)
        {
            AutoAim();
        }
        else
        {
            MouseAim();
        }
    }

    void AutoAim()
    {
        Transform closestEnemy = DetectClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
       
    }

    void MouseAim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
