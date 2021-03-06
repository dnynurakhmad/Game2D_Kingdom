using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public float AttackPoint;
    public bool HitArea;

    //private int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gm = GameManager.Instance;
        var position = collision.transform.position;
        if (collision.CompareTag("Enemy") && collision.isTrigger)
        {
            var enemy = collision.GetComponent<EnemyActive>();

            if (HitArea)
            {
                enemy.HealthPoint -= AttackPoint;
                DamageActive.PopUpDamage(gm.Origin_DamagePopUp, position, AttackPoint, DamageState.PlayerMgc);
            }
            else
            {
                //if(count == 0)
                //{
                    enemy.HealthPoint -= AttackPoint;
                    DamageActive.PopUpDamage(gm.Origin_DamagePopUp, position, AttackPoint, DamageState.PlayerPhs);
                    //var player = transform.parent.parent.GetComponent<PlayerActive>();
                    //if (player)
                    //{
                        //player.StaminaPoint.CurrentProp += 10;
                    //}
                //}
                //count++;
            }
        }
        else if (collision.CompareTag("Player") && collision.isTrigger)
        {
            var player = collision.GetComponent<PlayerActive>();
            if (!player.IsGuard)
            {
                player.HealthPoint.CurrentPoint -= AttackPoint;
                DamageActive.PopUpDamage(gm.Origin_DamagePopUp, position, AttackPoint, DamageState.EnemyPhs);
            }
            else
            {
                DamageActive.PopUpDamage(gm.Origin_DamagePopUp, position, 0, DamageState.EnemyPhs);
            }
        }
    }

    //private void OnDisable()
    //{
    //    count = 0;
    //}
}
