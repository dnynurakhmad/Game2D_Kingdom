using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct UnitPoint
{
    public float CurrentPoint;
    public float MaximumPoint;

    public float CurrentProp
    {
        get
        {
            return CurrentPoint;
        }
        set
        {
            CurrentPoint = value;
            if (CurrentPoint > MaximumPoint)
            {
                CurrentPoint = MaximumPoint;
            }
        }
    }
}

public class PlayerActive : MonoBehaviour
{
    public UnitState State;
    public bool IsAlive;
    public bool IsGuard;
    public float[] CooldownSkill;

    public UnitPoint HealthPoint;
    public UnitPoint MagicPoint;
    public UnitPoint StaminaPoint;
    public float MoveSpeed;
    public float AtkSpeed;

    public float[] SpellTime { get; set; }

    private Animator playerAnim;
    private AudioSource playerAudio;
    private AudioSource attackAudio;
    private AudioSource skill_1Audio;
    private Vector2 directPos;
    private float AtkTime;

    private void Player_Attack()
    {
        if (AtkTime == 0 && State == UnitState.Idle)
        {
            State = UnitState.Attack;
            playerAnim.SetTrigger("Attacking");            
            attackAudio.Play();
            AtkTime = AtkSpeed;
        }
    }

    private IEnumerator Player_Attack(SkillSet skill, Action action = null)
    {
        if (SpellTime[(int)skill - 1] == 0 && State == UnitState.Idle)
        {
            State = UnitState.Spell;
            playerAnim.SetTrigger(skill.ToString());
            SpellTime[(int)skill - 1] = CooldownSkill[(int)skill - 1];
            switch (skill)
            {
                case SkillSet.NinjaAttack:
                    skill_1Audio.Play();
                    StaminaPoint.CurrentPoint -= 20;
                    break;
                case SkillSet.DemonShield:
                    MagicPoint.CurrentPoint -= 50;
                    break;
            }
            yield return new WaitForSeconds(1f);
            action?.Invoke();
        }
    }

    private void Player_Movement(Vector2 moving)
    {
        moving.Normalize();
        playerAnim.SetFloat("AxisX", directPos.x);
        playerAnim.SetFloat("AxisY", directPos.y);
        if (moving.x !=0 || moving.y !=0)
        {
            directPos = moving;
            var xAndy = Mathf.Sqrt(Mathf.Pow(moving.x, 2) +
                                        Mathf.Pow(moving.y, 2));
            var pos_x = moving.x * MoveSpeed * Time.fixedDeltaTime / xAndy;
            var pos_y = moving.y * MoveSpeed * Time.fixedDeltaTime / xAndy;
            var pos_z = transform.position.z;
            transform.Translate(pos_x, pos_y, pos_z, Space.Self);
            playerAnim.SetBool("IsMoving", true);
            if (!playerAudio.isPlaying)
            {
                playerAudio.Play();
            }
            State = UnitState.Move;
        }
        else
        {
            playerAnim.SetBool("IsMoving", false);
            playerAudio.Stop();
            State = UnitState.Idle;
        }
    }

    private void Player_Death()
    {
        if (HealthPoint.CurrentPoint <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Player_Movement(Vector2.zero);
            HealthPoint.CurrentPoint = 0;
            playerAnim.SetTrigger("Dead");
            State = UnitState.Dead;
            IsAlive = false;
        }
    }

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        skill_1Audio = transform.Find("abilities").Find("ninjaAttack").GetComponent<AudioSource>();
        attackAudio = transform.Find("class").Find("swordman").GetComponent<AudioSource>();
        SpellTime = new float[] { 0f, 0f, 0f };
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            var moveaway = Vector2.zero;
            moveaway.x = Input.GetAxis("Horizontal");
            moveaway.y = Input.GetAxis("Vertical");
            Player_Movement(moveaway);

            var gm = GameManager.Instance;
            if (Input.GetButtonDown("Jump"))
            {
                Player_Attack();
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (StaminaPoint.CurrentPoint >= 20)
                {
                    StartCoroutine(Player_Attack(SkillSet.NinjaAttack));
                }               
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (gm.Item_Scroll > 0 && MagicPoint.CurrentPoint >= 50)
                {
                    StartCoroutine(Player_Attack(SkillSet.DemonShield, () => {
                        var shield = Instantiate(gm.Origin_demonShield, transform);
                        gm.Item_Scroll--;
                        Destroy(shield, 10f);
                    }));
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (gm.Item_Elixir > 0 && HealthPoint.CurrentPoint < HealthPoint.MaximumPoint && SpellTime[2] == 0)
                {
                    var effect = Instantiate(gm.Origin_lightSpell, transform);
                    Destroy(effect, 1f);

                    HealthPoint.CurrentProp += 100;
                    gm.Item_Elixir--;
                    DamageActive.PopUpDamage(gm.Origin_DamagePopUp, transform.position, 100, DamageState.Healing);
                    if (HealthPoint.CurrentPoint > HealthPoint.MaximumPoint)
                    {
                        HealthPoint.CurrentPoint = HealthPoint.MaximumPoint;
                    }
                    SpellTime[2] = CooldownSkill[2];
                }
            }

            Player_Death();
        }

        for (int i = 0; i < SpellTime.Length; i++)
        {
            if (SpellTime[i] > 0f)
            {
                SpellTime[i] -= Time.fixedDeltaTime;
            }

            if (SpellTime[i] < 0f)
            {
                SpellTime[i] = 0f;
                State = UnitState.Idle;
            }
        }

        if (AtkTime > 0f)
        {
            AtkTime -= Time.fixedDeltaTime;
        }

        if (AtkTime < 0f)
        {
            AtkTime = 0f;
            State = UnitState.Idle;
        }

        IsGuard = transform.Find("demonShield(Clone)");
    }
}