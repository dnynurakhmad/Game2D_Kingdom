using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Origin_normalCreep => Resources.Load<GameObject>("Prefabs/Enemies/normalCreep");
    public GameObject Origin_damagedCreep => Resources.Load<GameObject>("Prefabs/Enemies/damagedCreep");
    public GameObject Origin_armoredCreep => Resources.Load<GameObject>("Prefabs/Enemies/armoredCreep");
    public GameObject Origin_witchCreep => Resources.Load<GameObject>("Prefabs/Enemies/witchCreep");
    public GameObject Origin_skelCreep => Resources.Load<GameObject>("Prefabs/Enemies/skelCreep");
    public GameObject Origin_lightSpell => Resources.Load<GameObject>("Prefabs/Stuffs/lightSpell");
    public GameObject Origin_fireRed => Resources.Load<GameObject>("Prefabs/Stuffs/fireRed");
    public GameObject Origin_fireBlue => Resources.Load<GameObject>("Prefabs/Stuffs/fireBlue");
    public GameObject Origin_demonShield => Resources.Load<GameObject>("Prefabs/Stuffs/demonShield");

    public int GamePoint;
    public int KillPoint;

    public static GameManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }
}


public enum UnitState
{
    Idle, Move, Attack, Spell, Dead,
}

public enum EnemySet
{
    Default, Normal, Damaged, Armored, Skeleton, Witch,
}

public enum SkillSet
{
    Default, NinjaAttack, DemonShield,
}