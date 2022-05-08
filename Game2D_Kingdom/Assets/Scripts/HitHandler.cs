using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public float AttackPoint;
    public bool HitArea;

    private int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.isTrigger)
        {
            var enemy = collision.GetComponent<EnemyActive>();
            if (HitArea)
            {
                enemy.HealthPoint -= AttackPoint;
            }
            else
            {
                if(count == 0)
                {
                    enemy.HealthPoint -= AttackPoint;
                    var player = transform.parent.parent.parent.GetComponent<PlayerActive>(); ;
                    player.StaminaPoint.CurrentPoint += 10;
                }
                count++;
            }
        }
        else if (collision.CompareTag("Player") && collision.isTrigger)
        {
            var player = collision.GetComponent<PlayerActive>();
            if (player.IsGuard)
            {
                player.HealthPoint.CurrentPoint -= AttackPoint;
            }
        }
    }

    private void OnDisable()
    {
        count = 0;
    }
}
