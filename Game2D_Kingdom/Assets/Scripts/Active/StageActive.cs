using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StageActive : MonoBehaviour
{
    public GameObject[] NestSpawner;
    public GameObject[] HoleSpawner;

    private GameManager manager;

    private void GeneratorTier_1(Transform spawner, string tag)
    {
        var generator = spawner.GetComponent<GenerateActive>();

        for (int i = 0; i < generator.Creeps.Length; i++)
        {
            if (generator.Creeps[i] == null)
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

                creep.GetComponent<EnemyActive>().Spawner = spawner.gameObject;
                creep.GetComponent<EnemyActive>().SlotNum = i;
                generator.Creeps[i] = Instantiate(creep, spawner.position, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void GeneratorTier_2(Transform spawner, string tag)
    {
        var generator = spawner.GetComponent<GenerateActive>();

        for (int i = 0; i < generator.Creeps.Length; i++)
        {
            if (generator.Creeps[i] == null)
            {
                var gm = GameManager.Instance;
                GameObject creep = null;
                if (tag == "Nest")
                {
                    creep = (i < 4) ? gm.Origin_normalCreep : gm.Origin_armoredCreep;
                }
                else if (tag == "Hole")
                {
                    creep = (i < 1) ? gm.Origin_damagedCreep : gm.Origin_witchCreep;
                }

                creep.GetComponent<EnemyActive>().Spawner = spawner.gameObject;
                creep.GetComponent<EnemyActive>().SlotNum = i;
                generator.Creeps[i] = Instantiate(creep, spawner.position, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void GeneratorTier_3(Transform spawner, string tag)
    {
        var generator = spawner.GetComponent<GenerateActive>();

        for (int i = 0; i < generator.Creeps.Length; i++)
        {
            if (generator.Creeps[i] == null)
            {
                var gm = GameManager.Instance;
                GameObject creep = null;
                if (tag == "Nest")
                {
                    creep = (i < 5) ? gm.Origin_normalCreep : gm.Origin_armoredCreep;
                }
                else if (tag == "Hole")
                {
                    creep = (i < 2) ? gm.Origin_normalCreep : gm.Origin_witchCreep;
                }

                creep.GetComponent<EnemyActive>().Spawner = spawner.gameObject;
                creep.GetComponent<EnemyActive>().SlotNum = i;
                generator.Creeps[i] = Instantiate(creep, spawner.position, Quaternion.identity, transform);
                break;
            }
        }
    }

    private void StageSetup(LevelSet level, Action<Transform, string> action, params int[] stock)
    {
        manager.LevelPoint = level;
        foreach (var nest in NestSpawner)
        {
            var generator = nest.GetComponent<GenerateActive>();
            nest.SetActive(true);
            generator.Stock = stock[0];
            generator.GenerateAction = action;
        }
        foreach (var hole in HoleSpawner)
        {
            var generator = hole.GetComponent<GenerateActive>();
            hole.SetActive(true);
            generator.Stock = stock[1];
            generator.GenerateAction = action;
        }
    }

    private void LateUpdate()
    {
        if (manager == null)
        {
            manager = GameManager.Instance;
        }

        var level = manager.LevelPoint;
        if (manager.KillPoint > 99 && level != LevelSet.Hard)
        {
            StageSetup(LevelSet.Hard, GeneratorTier_3, 7, 5);
        }
        else if (manager.KillPoint > 30 && manager.KillPoint <= 99 && level != LevelSet.Normal)
        {
            StageSetup(LevelSet.Normal, GeneratorTier_2, 5, 3);
        }
        else if (manager.KillPoint <= 30 && level != LevelSet.Easy)
        {
            StageSetup(LevelSet.Easy, GeneratorTier_1, 3, 1);
        }
    }
}
