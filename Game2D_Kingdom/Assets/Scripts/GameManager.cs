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
    public GameObject Origin_DamagePopUp => Resources.Load<GameObject>("Prefabs/Stuffs/DamagePopUp");
    public GameObject Origin_Item_Elixir => Resources.Load<GameObject>("Prefabs/PowerUps/Item_Elixir");
    public GameObject Origin_Item_Scroll => Resources.Load<GameObject>("Prefabs/PowerUps/Item_Scroll");
    public GameObject Origin_Power_Green => Resources.Load<GameObject>("Prefabs/PowerUps/Power_Green");
    public GameObject Origin_Power_Red => Resources.Load<GameObject>("Prefabs/PowerUps/Power_Red");
    public GameObject Origin_Power_Blue => Resources.Load<GameObject>("Prefabs/PowerUps/Power_Blue");

    public int GamePoint;
    public int KillPoint;
    public int Item_Elixir
    {
        get
        {
            return itemElixir;
        }
        set
        {
            itemElixir = value;
            if (itemElixir > 99)
            {
                itemElixir = 99;
            }
        }
    }

    public int Item_Scroll
    {
        get
        {
            return itemScroll;
        }
        set
        {
            itemScroll = value;
            if (itemScroll > 99)
            {
                itemScroll = 99;
            }
        }
    }

    public static GameManager Instance { get; private set; }

    [SerializeField] private int itemElixir;
    [SerializeField] private int itemScroll;

    private void Start()
    {
        Instance = this;
    }
}



public enum UnitState
{
    Idle, Move, Attack, Spell, Dead,
}

public enum DamageState
{
    Default,
    PlayerPhs, PlayerMgc,
    EnemyPhs, Healing,
}

public enum EnemySet
{
    Default, Normal, Damaged, Armored, Skeleton, Witch,
}

public enum SkillSet
{
    Default, NinjaAttack, DemonShield,
}

public enum ItemSet
{
    Default,
    Item_Elixir, Item_Scroll,
    Power_Green, Power_Red, Power_Blue,
}