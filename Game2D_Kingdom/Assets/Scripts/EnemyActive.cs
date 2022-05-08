using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    public Transform Target;
    public UnitState State;
    public EnemySet Creep;
    public bool IsAlive;
    public bool IsForward;
    public float HealthPoint;
    public float MoveSpeed;
    public float AtkSpeed;

    private Animator enemyAnim;
    private AudioSource enemyAudio;

    private void Enemy_Stance()
    {
        enemyAnim.SetBool("IsMoving", false);
        State = UnitState.Idle;
    }

    private void Enemy_Movement(bool isForward)
    {
        var movetoward = Vector3.MoveTowards(transform.position, Target.position,
                                             MoveSpeed * Time.deltaTime);
        var towardpos = movetoward - transform.position;
        ChangeDirection(towardpos, isForward);
        if (isForward)
        {
            transform.position += towardpos;
        }
        else
        {
            transform.position -= towardpos;
        }
        enemyAnim.SetBool("IsMoving", true);
        State = UnitState.Move;
    }

    private void Enemy_Death()
    {
        if (HealthPoint <= 0)
        {
            IsAlive = false;
            HealthPoint = 0;
            State = UnitState.Dead;
            if (Creep == EnemySet.Skeleton)
            {
                Destroy(gameObject);
            }
            else
            {
                enemyAnim.SetTrigger("Dead");
                Destroy(gameObject, 3f);
            }
        }
    }

    private void ChangeDirection(Vector2 direction, bool isForward)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            var flag = isForward ? direction.x > 0 : direction.x < 0;
            SetAnimParameter(flag ? Vector2.right : Vector2.left);
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            var flag = isForward ? direction.y > 0 : direction.y < 0;
            SetAnimParameter(flag ? Vector2.up : Vector2.down);
        }
    }

    private void SetAnimParameter(Vector2 vector)
    {
        enemyAnim.SetFloat("AxisX", vector.x);
        enemyAnim.SetFloat("AxisY", vector.y);
    }

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (IsAlive)
        {
            var distance = Vector3.Distance(transform.position, Target.position);
            if (distance < 1.7f && IsForward)
            {
                Enemy_Stance();
            }
            else
            {
                if (IsForward)
                {
                    Enemy_Movement(true);
                }
                else
                {
                    if (distance < 6f)
                    {
                        Enemy_Stance();
                    }
                    if (distance < 5f)
                    {
                        Enemy_Movement(false);
                    }
                    else if (distance > 6f)
                    {
                        Enemy_Movement(true);
                    }
                }
            }

            Enemy_Death();
        }
    }
}

public enum EnemySet
{
    Default, Normal, Damaged, Armored, Skeleton, Witch,
}
