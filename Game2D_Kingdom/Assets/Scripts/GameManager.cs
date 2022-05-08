using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Origin_skelCreep => Resources.Load<GameObject>("Prefabs/Enemies/skelCreep");
    public GameObject Origin_lightSpell => Resources.Load<GameObject>("Prefabs/Stuffs/lightSpell");
    
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