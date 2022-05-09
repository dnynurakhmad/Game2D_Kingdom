using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GenerateActive : MonoBehaviour
{
    public GameObject[] Creeps;
    public float DelayTime;
        
    public Action<string> GenerateAction { get; set; }

    public bool IsReadySpawn
    {
        set
        {
            if (value)
            {
                if (spawnTime > 0)
                {
                    spawnTime -= Time.deltaTime;
                }

                if (spawnTime < 0)
                {
                    spawnTime = 1f;
                    GenerateAction?.Invoke(transform.tag);
                }
            }
        }
    }

    public int Stock
    {
        get
        {
            return stock;
        }
        set
        {
            stock = value;
            Creeps = new GameObject[stock];
        }
    }

    private int stock;
    private float castTime;
    private float spawnTime;

    private void GeneratorTier_1(string tag)
    {
        for (int i = 0; i < Creeps.Length; i++)
        {
            if (Creeps[i] == null)
            {
                var gm = GameManager.Instance;
                GameObject creep = null;
                if (tag == "Nest")
                {
                    creep = gm.Origin_normalCreep;
                }
                else if (tag == "Hole")
                {
                    creep = gm.Origin_damagedCreep;
                }

                creep.GetComponent<EnemyActive>().Spawner = gameObject;
                creep.GetComponent<EnemyActive>().SlotNum = i;
                var generator = GetComponent<GenerateActive>();
                generator.Creeps[i] = Instantiate(creep, transform.position, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void GeneratorTier_2(string tag)
    {
        for (int i = 0; i < Creeps.Length; i++)
        {
            if (Creeps[i] == null)
            {
                var gm = GameManager.Instance;
                GameObject creep = null;
                if (tag == "Nest")
                {
                    creep = (i < 4) ? gm.Origin_normalCreep : gm.Origin_armoredCreep;
                }
                else if (tag == "Hole")
                {
                    creep = (i < 3) ? gm.Origin_damagedCreep : gm.Origin_witchCreep;
                }

                creep.GetComponent<EnemyActive>().Spawner = gameObject;
                creep.GetComponent<EnemyActive>().SlotNum = i;
                var generator = GetComponent<GenerateActive>();
                generator.Creeps[i] = Instantiate(creep, transform.position, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void GeneratorTier_3(string tag)
    {
        for (int i = 0; i < Creeps.Length; i++)
        {
            if (Creeps[i] == null)
            {
                var gm = GameManager.Instance;
                GameObject creep = null;
                if (tag == "Nest")
                {
                    creep = (i < 5) ? gm.Origin_normalCreep : gm.Origin_armoredCreep;
                }
                else if (tag == "Hole")
                {
                    creep = (i < 3) ? gm.Origin_normalCreep : gm.Origin_witchCreep;
                }

                creep.GetComponent<EnemyActive>().Spawner = gameObject;
                creep.GetComponent<EnemyActive>().SlotNum = i;
                var generator = GetComponent<GenerateActive>();
                generator.Creeps[i] = Instantiate(creep, transform.position, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void Start()
    {
        castTime = DelayTime;
        Stock = 5;
        GenerateAction = GeneratorTier_2;
    }

    private void Update()
    {
        if (castTime > 0)
        {
            castTime -= Time.deltaTime;
        }

        if (castTime < 0)
        {
            castTime = 0;
            spawnTime = 1f;
        }

        IsReadySpawn = transform.childCount < Stock;
    }
}
